// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.OperationExecutor

open System
open System.Collections.Specialized
open System.Linq
open System.IO
open System.Text
open System.Text.RegularExpressions
open Elastic.Transport
open Microsoft.FSharp.Reflection
open Tests.YamlRunner.Models
open Tests.YamlRunner.Stashes
open ShellProgressBar
open Elasticsearch.Net
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open System.Collections.Generic

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
            | Some _ ->
                let v = stashes.GetResponseValue progress prop
                stashes.[id] <- v
                Succeeded op
            | None ->
                Failed <| Fail.Create op "Attempted to look up %s but no response was set prior" prop
        
        s |> Map.map v |> ignore 
        Succeeded op

    static member Do op d (lookup:YamlMap -> DoMapper.FastApiInvoke) progress = async {
        let stashes = op.Stashes
        let (api, data) = d.ApiCall
        try
            let invoke = lookup data
            let resolvedData = stashes.Resolve progress data
            let headers =
                let head (h:string) =
                    match h.Contains("$") with
                    | false -> h
                    | true ->
                        Regex.Replace(h, "\$\{?\w+\}?", fun r -> (stashes.ResolveToken progress r.Value).ToString())
                match d.Headers with
                | Some h ->
                    (h.AllKeys |> Seq.map(fun key -> key, head h.[key]))
                    |> Map.ofSeq
                    |> Map.fold
                      (fun (nv:NameValueCollection) k v ->
                        (nv.[k] <- v)
                        nv
                      )
                      (NameValueCollection())
                    |> Option.Some
                | None -> None
                    
            
            let! r = Async.AwaitTask <| invoke.Invoke resolvedData headers
            
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
                        let body = Encoding.UTF8.GetString(r.ApiCall.ResponseBodyInBytes)
                        match Regex.IsMatch(body, regexp) with
                        | true -> Succeeded op
                        | false -> Failed <| Fail.Create op "Catching error failed: %O on server error" d.Catch 
                    | _ -> Succeeded op
            
            //progress.WriteLine <| sprintf "%s %s" (r.ApiCall.HttpMethod.ToString()) (r.Uri.PathAndQuery)
            return result
        with
        | DoMapper.ParamException e ->
            match d.Catch with
            | Some UnknownParameter -> return Succeeded op
            | _ ->
                return Failed <| Fail.Create op "%s %O" e d.Catch
                
    }
    
    static member TransformAndSet op (t:TransformAndSet) (progress:IProgressBar) =
        let call (prop:StashedId) (transform:Transformation) =
            let stashes = op.Stashes
            match transform.Function with
            | "base64EncodeCredentials" ->
                let encoded =
                    transform.Values
                    |> List.map (fun v ->
                        match v with
                        | ResponsePath p -> stashes.GetResponseValue<string> progress p 
                        | _ -> null
                    )
                    |> String.concat ":"
                    |> Encoding.UTF8.GetBytes
                    |> Convert.ToBase64String
                
                stashes.[prop] <- encoded
                
                Succeeded op
            | func ->
                Failed <| Fail.Create op "TransformAndSet unknown function: %s" func
            
        
        t |> Map.map (fun k v -> call k v.Transform) |> ignore 
        Succeeded op

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
            match r.HttpMethod, r.ApiCall.Success, r.Body with
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
        | null -> JValue(t) :> JToken
        | :? JToken as j -> j 
        // yaml test framework often compares ints with doubles, does not validate
        // actual numeric types returned
        | :? int 
        | :? int64 -> JValue(Convert.ToDouble(t)) :> JToken
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
            
    // inspects `actual` is a list of objects and contains an object with the keys and value of `expected`
    static member JTokenContainsSubSet op expected actual =
            let expected = OperationExecutor.ToJToken expected
            let actual = OperationExecutor.ToJToken actual
            match actual.Type with
            | JTokenType.Array ->
                let array = actual :?> JArray
                let setOfKeys (o:JObject) = o.Properties() |> Seq.map(fun p -> p.Name) |> Set.ofSeq
                let expectedObj = expected :?> JObject
                let expectedKeys = setOfKeys expectedObj
                let misMatchedValues (o:JObject) = 
                    expectedObj.Properties()
                    |> Seq.map(fun prop -> (prop, JToken.DeepEquals (prop.Value, o.Property(prop.Name).Value)))
                    // filter all values that differ
                    |> Seq.filter (fun (_, equals) -> not equals)
                    |> Seq.map (fun (prop, _) -> (prop.Name, prop.Value))
                    |> Seq.toList
                let actualValues =
                    array
                    |> Seq.map(fun v -> v :?> JObject)
                    |> Seq.filter(fun o -> o <> null)
                    |> Seq.filter(fun o -> (setOfKeys o).IsSupersetOf(expectedKeys))
                    |> Seq.map(fun o -> misMatchedValues o)
                    |> List.ofSeq
                    
                match actualValues |> List.tryFind(fun (l) -> l.Length = 0) with
                | None ->
                    let a = actual.ToString(Formatting.None) 
                    let e = expected.ToString(Formatting.None)
                    Failed <| Fail.Create op "No object in actual: %s has proper subset of keys and values from expected: %s" a e
                | Some _ ->
                    Succeeded op
            | _ -> 
                Failed <| Fail.Create op "Can not assert contains when actual is not an array: %s" (actual.ToString(Formatting.None))
        
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
                        let dictOrArray = stashes.Response().Body.Count
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
            |> Seq.sortBy (fun ex -> match ex with | Succeeded _ -> 4 | Skipped _ -> 3 | NotSkipped _ -> 2 | Failed _ -> 1)
            |> Seq.toList
            
        asserts |> Seq.head
        
    static member IsMatch op (matchOp:Match) progress =
        let stashes = op.Stashes
        let doMatch assertOn assertValue = 
            let value =
                match (assertOn, assertValue) with
                | (ResponsePath "$body", Value _) -> stashes.Response().Body.ToDictionary() :> Object
                | (ResponsePath path, _) -> stashes.GetResponseValue progress path :> Object
                | (WholeResponse, _) -> stashes.Response().Body.ToDictionary() :> Object
            
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
                    let reg = 
                        let s = re.Regex.ToString()
                        match stashes.ReplaceStaches progress s with
                        | (false, _) -> re.Regex
                        | (true, s) -> Regex(s, RegexOptions.IgnorePatternWhitespace)
                            
                    let matched = reg.IsMatch(body)
                    match matched with
                    | true -> Succeeded op
                    | false -> Failed <| Fail.Create op "regex did not match body %s" body
                    
        let asserts =
            matchOp
            |> Map.toList
            |> Seq.map (fun (k, v) -> doMatch k v)
            |> Seq.sortBy (fun ex -> match ex with | Succeeded _ -> 4 | Skipped _ -> 3 | NotSkipped _ -> 2 | Failed _ -> 1)
            |> Seq.toList
            
        asserts |> Seq.head
        
    static member IsContains op (matchOp:Match) progress =
        let stashes = op.Stashes
        let doMatch assertOn assertValue = 
            let value =
                match (assertOn, assertValue) with
                | (ResponsePath "$body", Value _) -> stashes.Response().Body.ToDictionary() :> Object
                | (ResponsePath path, _) -> stashes.GetResponseValue progress path :> Object
                | (WholeResponse, _) -> stashes.Response().Body.ToDictionary() :> Object
            
            match assertValue with
            | Value o -> OperationExecutor.JTokenContainsSubSet op o value
            | Id id ->
                let found, expected = stashes.TryGetValue id
                match found with
                | true -> OperationExecutor.JTokenContainsSubSet op expected value
                | false -> Failed <| Fail.Create op "%A not stashed at this point" id 
            | RegexAssertion _ ->
                Failed <| Fail.Create op "regex assertion not supported in `contains`"
                
        let asserts =
            matchOp
            |> Map.toList
            |> Seq.map (fun (k, v) -> doMatch k v)
            |> Seq.sortBy (fun ex -> match ex with | Succeeded _ -> 4 | Skipped _ -> 3 | NotSkipped _ -> 2 | Failed _ -> 1)
            |> Seq.toList
            
        asserts |> Seq.head

    member this.Execute (op:ExecutionContext) (progress:IProgressBar) = async {
        match op.Operation with
        | Unknown u -> return Skipped (op, sprintf "Unknown operation: %s" u)
        | Actions (_, a) ->
            match a (client, op.Suite) with
            | None -> return Succeeded op
            | r ->
                op.Stashes.ResponseOption <- r
                return Failed <| Fail.Create op "%s" op.Section
        | Skip s ->
            let skip reason = Skipped (op, s.Reason |> Option.defaultValue reason)
            let versionRangeCheck (versions:SemVer.Range list) =
                let anchoredVersion =
                    let x = SemVer.Version(op.Version)
                    sprintf "%i.%i.%i" x.Major x.Minor x.Patch
                
                let versionInRange =
                    versions
                    |> List.tryFind (fun v -> v.IsSatisfied(op.Version) || v.IsSatisfied(anchoredVersion))
                
                match versionInRange with
                | Some v -> skip (sprintf "version:%s in range:%O" op.Version v)
                | None -> NotSkipped op
            let featureCheck (features:Feature list) =
                let unsupportedFeatures =
                    features
                    |> Seq.filter (fun feature -> not (SupportedFeatures |> List.contains feature))
                    |> Seq.toList
                
                let noXPackButXPack = features.Contains(NoXPack) && op.Suite = XPack
                match (unsupportedFeatures, noXPackButXPack) with
                | ([], false) -> NotSkipped op
                | ([], true) ->
                   skip (sprintf "no_xpack was specified but we are running against an xpack node")
                | (l,_) -> skip (sprintf "feature %O not supported" l)
            
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
        | TransformAndSet t -> return OperationExecutor.TransformAndSet op t progress
        | Assert a ->
            return
                match a with
                | IsTrue t -> OperationExecutor.IsTrue op t progress
                | IsFalse t -> OperationExecutor.IsFalse op t progress
                | Contains m -> OperationExecutor.IsContains op m progress
                | Match m -> OperationExecutor.IsMatch op m progress
                | NumericAssert (a, m) -> OperationExecutor.IsNumericMatch op a m progress
              
    }
