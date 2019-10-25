module Tests.YamlRunner.OperationExecutor

open System
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
open System.Diagnostics

type ExecutionContext = {
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
            | SeenException (_, e) -> sprintf "Exception: %O" e 
            | ValidationFailure (_, r) -> sprintf "Reason: %s" r 
        static member private FormatFailure op fmt = ValidationFailure (op, sprintf "%s" fmt)
        static member Create op fmt = Printf.kprintf (fun x -> Fail.FormatFailure op x) fmt

type ExecutionResult =
    | Succeeded of ExecutionContext
    | Skipped of ExecutionContext 
    | Failed of Fail
    with
        member this.Name = match FSharpValue.GetUnionFields(this, this.GetType()) with | (case, _) -> case.Name
        member this.Context = match this with | Succeeded c -> c | Skipped c -> c | Failed f -> f.Context


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
        let (_, data) = d.ApiCall
        try
            let invoke = lookup data
            let resolvedData = stashes.Resolve progress data
            let! r = Async.AwaitTask <| invoke.Invoke resolvedData
            
            let responseMimeType = r.ApiCall.ResponseMimeType
            match responseMimeType with
            | RequestData.MimeType -> ignore() //json
            // not json set $body to the response body string
            | _ -> op.Stashes.[StashedId.Body] <- r.Get<String>("body")
            
            op.Stashes.ResponseOption <- Some r
            
            //progress.WriteLine <| sprintf "%s %s" (r.ApiCall.HttpMethod.ToString()) (r.Uri.PathAndQuery)
            return Succeeded op
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
        | Skipped op -> Skipped op
        | Failed f -> Succeeded f.Context
        | Succeeded op ->
            Failed <| Fail.Create op "Expected is_false but got is_true behavior"
        
    static member IsMatch op (matchOp:Match) progress =
        let stashes = op.Stashes
        let isMatch expected actual =
            let toJtoken (t:Object) =
                match t with
                | null -> new JValue(t) :> JToken
                // yaml test framework often compares ints with doubles, does not validate
                // actual numeric types returned
                | :? int 
                | :? int64 -> new JValue(Convert.ToDouble(t)) :> JToken
                | :? Dictionary<Object, Object> as d -> JObject.FromObject(d) :> JToken
                | :? IDictionary<String, Object> as d -> JObject.FromObject(d) :> JToken
                | _ -> JToken.FromObject t
                
            let expected = toJtoken expected
            let actual = toJtoken actual
            match JToken.DeepEquals (expected, actual) with
            | false ->
                let a = actual.ToString(Formatting.None)
                let e = expected.ToString(Formatting.None)
                Failed <| Fail.Create op "expected: %s actual: %s" e a
            | _ -> Succeeded op
        
        let doMatch assertOn assertValue = 
            let value =
                match assertOn with
                | ResponsePath path -> stashes.GetResponseValue progress path :> Object
                | WholeResponse -> stashes.Response().Dictionary.ToDictionary() :> Object
            
            match assertValue with
            | Value o -> isMatch o value
            | Id id ->
                let found, expected = stashes.TryGetValue id
                match found with
                | true -> isMatch expected value
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
            |> Seq.sortBy (fun ex -> match ex with | Succeeded o -> 3 | Skipped o -> 2 | Failed o -> 1)
            |> Seq.toList
            
        asserts |> Seq.head

    member this.Execute (op:ExecutionContext) (progress:IProgressBar) = async {
        match op.Operation with
        | Unknown u -> return Skipped op
        | Skip s -> return Skipped op
        | Do d ->
            let (name, _) = d.ApiCall
            let found, lookup = this.OpMap.TryGetValue name
            if found then 
               return! OperationExecutor.Do op d lookup progress
            else 
                return Failed <| Fail.Create op "Api: %s not found on client" name 
        | Set s -> return OperationExecutor.Set op s progress
        | TransformAndSet ts -> return Skipped op
        | Assert a ->
            return
                match a with
                | IsTrue t -> OperationExecutor.IsTrue op t progress
                | IsFalse t -> OperationExecutor.IsFalse op t progress
                | Match m -> OperationExecutor.IsMatch op m progress
                | NumericAssert (a, m) -> Skipped op
              
    }
