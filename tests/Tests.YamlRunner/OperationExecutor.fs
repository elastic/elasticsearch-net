module Tests.YamlRunner.OperationExecutor

open System
open System.Linq
open System.IO
open Microsoft.FSharp.Reflection
open Tests.YamlRunner.DoMapper
open Tests.YamlRunner.Models
open Tests.YamlRunner.Stashes
open ShellProgressBar
open Elasticsearch.Net
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open System.Collections.Generic
open System.Runtime.ExceptionServices

type ExecutionContext = {
    Version: string
    Suite: TestSuite
    Folder: DirectoryInfo
    File: FileInfo
    Section: string
    NthOperation: int
    Operation: Operation
    Stashes: Stashes
    Elapsed: int64 ref
}
    with member __.Throw message = failwithf "%s" message

type Fail =
    | SeenException of ExecutionContext * Exception 
    | ValidationFailure of ExecutionContext * string
    with
        member this.Context = match this with | SeenException (c, _) -> c | ValidationFailure (c, _) -> c
        member this.Log () =
            match this with
            | SeenException (_, e) -> sprintf "Exception: %s" e.Message 
            | ValidationFailure (_, r) -> sprintf "Reason: %s" r 
        static member private FormatFailure op fmt = ValidationFailure (op, sprintf "%s" fmt)
        static member Create op fmt = Printf.kprintf (fun x -> Fail.FormatFailure op x) fmt
        
type ExecutionResult =
    | Succeeded of ExecutionContext
    | Skipped of ExecutionContext * string
    | NotSkipped of ExecutionContext
    | Failed of Fail
    with
        member this.Name = match FSharpValue.GetUnionFields(this, this.GetType()) with | (case, _) -> case.Name
        member this.Context =
            match this with | Succeeded c -> c | NotSkipped c -> c | Skipped (c, _) -> c | Failed f -> f.Context
            
type JTokenOrFailure = Token of JToken | Fail of Fail

type OperationExecutor(client:IElasticLowLevelClient) =

    member private this.OpMap = DoMapper.createDoMap client
    
    static member Set op s (progress:IProgressBar) = 
        let v (prop:ResponseProperty) id =
            let stashes = op.Stashes
            let (ResponseProperty prop) = prop
            match stashes.ResponseOption with
            | Some r ->
                let v = stashes.GetResponseValue progress prop
                stashes.[id] <- v
                Succeeded op
            | None ->
                Failed <| Fail.Create op "Attempted to look up %s but no response was set prior" prop
        
        let responses = s |> Map.map v 
        Succeeded op

    static member Do op d (lookup:YamlMap -> FastApiInvoke) progress = async {
        let stashes = op.Stashes
        let (api, data) = d.ApiCall
        try
            let invoke = lookup data
            let resolvedData = stashes.Resolve progress data
            let! r = Async.AwaitTask <| invoke.Invoke resolvedData d.Headers
            
            let responseMimeType = r.ApiCall.ResponseMimeType
            match responseMimeType with
            | RequestData.MimeType -> ignore() //json
            // not json set $body to the response body string
            | _ -> op.Stashes.[StashedId.Body] <- r.Get<String>("body")
            
            op.Stashes.ResponseOption <- Some r
            
            let result =
                let autoFailed = d.AutoFail && not r.Success
                let statusCode = if r.HttpStatusCode.HasValue then Some r.HttpStatusCode.Value else None
                match (autoFailed, d.Catch, statusCode) with
                | true, _, _ -> Failed <| Fail.Create op "AutoFail triggered on api: %s" api
                | false, None, _  -> Succeeded op
                | _, Some catch, statusCode ->
                    match (catch, statusCode) with
                    | BadRequest, Some s when s <> 400 ->
                        Failed <| Fail.Create op "Catch %A: expected 400 received %i" catch s
                    | Unauthorized, Some s when s <> 401 ->
                        Failed <| Fail.Create op "Catch %A: expected 401 received %i" catch s
                    | Forbidden, Some s when s <> 403 ->
                        Failed <| Fail.Create op "Catch %A: expected 403 received %i" catch s
                    | Missing, Some s when s <> 404 ->
                        Failed <| Fail.Create op "Catch %A: expected 403 received %i" catch s
                    | RequestTimeout, Some s when s <> 408 ->
                        Failed <| Fail.Create op "Catch %A: expected 408 received %i" catch s
                    | Conflict, Some s when s <> 409 ->
                        Failed <| Fail.Create op "Catch %A: expected 409 received %i" catch s
                    | Unavailable, Some s when s <> 503 ->
                        Failed <| Fail.Create op "Catch %A: expected 503 received %i" catch s
                    | OtherBadResponse, Some s when s < 400 || s >= 600 ->
                        Failed <| Fail.Create op "Catch %A: expected 4xx-5xx received %i" catch s
                    | (CatchRegex regexp), _ -> 
                        let body = System.Text.Encoding.UTF8.GetString(r.ApiCall.ResponseBodyInBytes)
                        match System.Text.RegularExpressions.Regex.IsMatch(body, regexp) with
                        | true -> Succeeded op
                        | false -> Failed <| Fail.Create op "Catching error failed: %O on server error" d.Catch 
                    | _ -> Succeeded op
            
            //progress.WriteLine <| sprintf "%s %s" (r.ApiCall.HttpMethod.ToString()) (r.Uri.PathAndQuery)
            return result
        with
        | ParamException e ->
            match d.Catch with
            | Some UnknownParameter -> return Succeeded op
            | _ ->
                return Failed <| Fail.Create op "%s %O" e d.Catch
                
    }

    ///<summary>The specified key exists and has a true value (ie not 0, false, undefined, null or the empty string)</summary>
    static member IsTrue op (t:AssertOn) progress =
        let stashes = op.Stashes
        match t with
        | ResponsePath p ->
            let v = stashes.GetResponseValue progress p :> Object
            match v with
            | null -> Failed <| Fail.Create op "resolved to null"
            | :? string as s when String.IsNullOrEmpty s -> Failed <| Fail.Create op "string is null or empty"
            | :? bool as s when not s -> Failed <| Fail.Create op "returned bool is false"
            | :? int as s when s = 0 -> Failed <| Fail.Create op "int equals 0"
            | :? int64 as s when s = 0L -> Failed <| Fail.Create op "long equals 0"
            | _ -> Succeeded op
        | WholeResponse ->
            let r = stashes.Response()
            match r.HttpMethod, r.ApiCall.Success, r.Dictionary  with
            | (HttpMethod.HEAD, true, _) -> Succeeded op
            | (HttpMethod.HEAD, false, _) -> Failed <| Fail.Create op "HEAD request not successful"
            | (_,_, b) when b = null  -> Failed <| Fail.Create op "no body was returned"
            | _ -> Succeeded op
            
    static member IsFalse op (t:AssertOn) progress =
        let isTrue = OperationExecutor.IsTrue op t progress
        match isTrue with
        | Skipped (op, r) -> Skipped (op, r)
        | NotSkipped c -> NotSkipped c
        | Failed f -> Succeeded f.Context
        | Succeeded op ->
            Failed <| Fail.Create op "Expected is_false but got is_true behavior"
    
    
    static member ToJToken (t:Object) =
        match t with
        | null -> new JValue(t) :> JToken
        | :? JToken as j -> j 
        // yaml test framework often compares ints with doubles, does not validate
        // actual numeric types returned
        | :? int 
        | :? int64 -> new JValue(Convert.ToDouble(t)) :> JToken
        | :? Dictionary<Object, Object> as d -> JObject.FromObject(d) :> JToken
        | :? IDictionary<String, Object> as d -> JObject.FromObject(d) :> JToken
        | _ -> JToken.FromObject t
    
    static member JTokenDeepEquals op expected actual =
            let expected = OperationExecutor.ToJToken expected
            let actual = OperationExecutor.ToJToken actual
            match JToken.DeepEquals (expected, actual) with
            | false ->
                let a = actual.ToString(Formatting.None)
                let e = expected.ToString(Formatting.None)
                Failed <| Fail.Create op "expected: %s actual: %s" e a
            | _ -> Succeeded op
        
    static member IsNumericMatch op (assertion:NumericAssert) (m:NumericMatch) progress =
        let stashes = op.Stashes
        let doMatch assertOn assertValue = 
            match assertOn with
            | WholeResponse ->
                Failed <| Fail.Create op "Can not do numeric asserts on whole responses" 
            | ResponsePath path ->
                let expected =
                    match assertValue with
                    | Long l -> Token(JValue(Convert.ToDouble(l)))
                    | Double d -> Token(JValue(d))
                    | NumericId id ->
                        let found, expected = stashes.TryGetValue id
                        match found with
                        | true -> Token(OperationExecutor.ToJToken expected)
                        | false -> Fail(Fail.Create op "%A not stashed at this point" id)
                        
                let expectedValue (value:JToken) =
                    match value with
                    | :? JValue as v -> Some(Convert.ChangeType(v.Value, typeof<double>) :?> double)
                    | :? JArray as a -> Some <| Convert.ToDouble(a.Count)
                    | :? JObject as o -> Some <| Convert.ToDouble(o.Properties().Count())
                    | _ -> None
                        
                let actual =
                    match path with 
                    | "$body" ->
                        let dictOrArray = stashes.Response().Dictionary.Count
                        Some <| Convert.ToDouble(dictOrArray)
                    | _ -> 
                        let a = OperationExecutor.ToJToken <| (stashes.GetResponseValue progress path :> Object)
                        expectedValue a
                
                let numMatch a e s c = 
                    let e = expectedValue e
                    match e with
                    | None -> Failed <| Fail.Create op "Can not get numeric value from expected %O" e
                    | Some e when c a e  -> Succeeded op 
                    | Some e -> Failed <| Fail.Create op "Expected %f %s %f " e s a
                
                match (assertion, actual, expected) with
                | (_, _, Fail f) -> Failed <| f
                | (_, None, _) -> Failed <| Fail.Create op "Can not get numeric value from actual %O" actual
                | (Equal, Some a, Token e) -> OperationExecutor.JTokenDeepEquals op e a 
                | (Length, Some a, Token e) -> numMatch a e "=" <| (fun a e -> a = e)
                | (LowerThan, Some a, Token e) -> numMatch a e "<" <| (fun a e -> a < e)
                | (GreaterThan, Some a, Token e)  -> numMatch a e ">" <| (fun a e -> a > e)
                | (GreaterThanOrEqual, Some a, Token e) -> numMatch a e ">=" <| (fun a e -> a >= e)
                | (LowerThanOrEqual, Some a, Token e)  -> numMatch a e "<=" <| (fun a e -> a <= e)
    
        let asserts =
            m
            |> Map.toList
            |> Seq.map (fun (k, v) -> doMatch k v)
            |> Seq.sortBy (fun ex -> match ex with | Succeeded o -> 4 | Skipped (o, s) -> 3 | NotSkipped s -> 2 | Failed o -> 1)
            |> Seq.toList
            
        asserts |> Seq.head
        
    static member IsMatch op (matchOp:Match) progress =
        let stashes = op.Stashes
        let doMatch assertOn assertValue = 
            let value =
                match (assertOn, assertValue) with
                | (ResponsePath "$body", Value _) -> stashes.Response().Dictionary.ToDictionary() :> Object
                | (ResponsePath path, _) -> stashes.GetResponseValue progress path :> Object
                | (WholeResponse, _) -> stashes.Response().Dictionary.ToDictionary() :> Object
            
            match assertValue with
            | Value o -> OperationExecutor.JTokenDeepEquals op o value
            | Id id ->
                let found, expected = stashes.TryGetValue id
                match found with
                | true -> OperationExecutor.JTokenDeepEquals op expected value
                | false -> Failed <| Fail.Create op "%A not stashed at this point" id 
            | RegexAssertion re ->
                match assertOn with
                | WholeResponse -> 
                    Failed <| Fail.Create op "regex can no t be called on the parsed body ('')"
                | ResponsePath _ -> 
                    let body = value.ToString()
                    let matched = re.Regex.IsMatch(body)
                    match matched with
                    | true -> Succeeded op
                    | false -> Failed <| Fail.Create op "regex did not match body %s" body
                    
        let asserts =
            matchOp
            |> Map.toList
            |> Seq.map (fun (k, v) -> doMatch k v)
            |> Seq.sortBy (fun ex -> match ex with | Succeeded o -> 4 | Skipped (o, s) -> 3 | NotSkipped s -> 2 | Failed o -> 1)
            |> Seq.toList
            
        asserts |> Seq.head

    member this.Execute (op:ExecutionContext) (progress:IProgressBar) = async {
        match op.Operation with
        | Unknown u -> return Skipped (op, sprintf "Unknown operation: %s" u)
        | Actions (s, a) ->
            match a (client, op.Suite) with
            | None -> return Succeeded op
            | r ->
                op.Stashes.ResponseOption <- r
                return Failed <| Fail.Create op "%s" op.Section
        | Skip s ->
            let skip reason = Skipped (op, s.Reason |> Option.defaultValue reason)
            let versionRangeCheck (v:SemVer.Range) =
                let anchoredVersion =
                    let x = SemVer.Version(op.Version)
                    sprintf "%i.%i.%i" x.Major x.Minor x.Patch
                match v.IsSatisfied(op.Version) || v.IsSatisfied(anchoredVersion) with
                | true -> skip (sprintf "version:%s in range:%O" op.Version v)
                | false -> NotSkipped op
            let featureCheck (features:Feature list) =
                let unsupportedFeatures =
                    features
                    |> Seq.filter (fun feature -> not (SupportedFeatures |> List.contains feature))
                    |> Seq.toList
                
                match unsupportedFeatures with
                | [] -> NotSkipped op
                | _ -> skip (sprintf "feature %O not support" features)
            
            let result =
                match (s.Version, s.Features) with
                | (Some v, Some features) ->
                    match (versionRangeCheck v) with
                    | Skipped (op, r) -> Skipped (op, r)
                    | _ ->
                        featureCheck features
                | (Some v, None) -> 
                    versionRangeCheck v
                | (None, Some features) ->
                    featureCheck features
                | _  ->
                    NotSkipped op
    
            return result
        | Do d ->
            let (name, _) = d.ApiCall
            let found, lookup = this.OpMap.TryGetValue name
            if found then 
               return! OperationExecutor.Do op d lookup progress
            else 
                return Failed <| Fail.Create op "Api: %s not found on client" name 
        | Set s -> return OperationExecutor.Set op s progress
        | TransformAndSet ts -> return Skipped (op, "TODO transform_and_set")
        | Assert a ->
            return
                match a with
                | IsTrue t -> OperationExecutor.IsTrue op t progress
                | IsFalse t -> OperationExecutor.IsFalse op t progress
                | Match m -> OperationExecutor.IsMatch op m progress
                | NumericAssert (a, m) -> OperationExecutor.IsNumericMatch op a m progress
              
    }
