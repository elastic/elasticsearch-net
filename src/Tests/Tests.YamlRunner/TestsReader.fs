module Tests.YamlRunner.TestsReader

open System
open System
open System.Collections.Generic
open System.Text.RegularExpressions
open System.Linq


type Skip = { Version:string; Reason:string option }

type DoCatch =
    | BadRequest // bad_request, 400 response from ES
    | Unauthorized //unauthorized a 401 response from ES
    | Forbidden //forbidden a 403 response from ES
    | Missing //missing a 404 response from ES
    | RequestTimeout //request_timeout a 408 response from ES
    | Conflict //conflict a 409 response from ES
    | Unavailable//unavailable a 503 response from ES
    | UnknownParameter //param a client-side error indicating an unknown parameter has been passed to the method
    | OtherBadResponse //request 4xx-5xx error response from ES, not equal to any named response above
    | Regex // /foo bar/ the text of the error message matches this regular expression
    
let (|IsDoCatch|_|) (s:string) =
    match s with
    | "bad_request" -> Some BadRequest 
    | "unauthorized " -> Some Unauthorized 
    | "forbidden " -> Some Forbidden 
    | "missing " -> Some Missing 
    | "request_timeout " -> Some RequestTimeout 
    | "conflict " -> Some Conflict 
    | "unavailable " -> Some Unavailable
    | "param " -> Some UnknownParameter
    | "request " -> Some OtherBadResponse 
    | "regex" -> Some Regex
    | _ -> None
    
type NodeSelector =
    | NodeVersionSelector of string
    | NodeAttributeSelector of string * string
    | NodeSelector of string * string * string

type ResponseProperty = private ResponseProperty of string
type StashedId = private StashedId of string
type SetTransformation = private SetTransformation of string
type AssertPath = private AssertPath of string

type Set = Map<ResponseProperty, StashedId>
type TransformAndSet = Map<StashedId, SetTransformation>
type Match = Map<AssertPath, Object>
type NumericMatch = Map<AssertPath, int64>
    
type Do = {
    ApiCall: string * Object
    Catch:DoCatch option
    Warnings:option<string list>
    NodeSelector:NodeSelector option
}

type NumericAssert = 
    | LowerThan 
    | GreaterThan 
    | GreaterThanOrEqual 
    | LowerThanOrEqual 
    | Equal 
    | Length

let (|IsNumericAssert|_|) (s:string) =
    match s with
    | "length" -> Some Length
    | "eq" -> Some Equal
    | "gte" -> Some GreaterThanOrEqual
    | "lte" -> Some LowerThanOrEqual
    | "gt" -> Some GreaterThan
    | "lt" -> Some LowerThan
    | _ -> None
    
type Assert = 
    | IsTrue of AssertPath
    | IsFalse of AssertPath
    | Match of Match
    | NumericAssert of NumericAssert * NumericMatch

type Operation =
    | Unknown of string
    | Skip of Skip
    | Do of Do
    | Set of Set
    | TransformAndSet of TransformAndSet
    | Assert of Assert
type Operations = Operation list

type YamlTest = {
    Name:string;
    Actions: Operations;
}
    
type TestType = | Setup of Operations | Teardown of Operations | Suite of YamlTest

type YamlMap = Dictionary<Object,Object>

let tryPick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then Some (value :?> 'a) else None
    
let pick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then (value :?> 'a)
    else failwithf "expected to find %s of type %s" key typeof<'a>.Name

let mapSkip (operation:YamlMap) =
    let version = pick<string> operation "version" 
    let reason = tryPick<string> operation "reason"
    Skip { Version=version; Reason=reason }
    
let mapNumericAssert (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> AssertPath (kv.Key :?> string), kv.Value :?> int64)
        |> Map.ofSeq
        
let firstValueAsPath (operation:YamlMap) = AssertPath (operation.Values.First() :?> string)

let mapMatch (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> AssertPath (kv.Key :?> string), kv.Value)
        |> Map.ofSeq
        
let mapTransformAndSet (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> StashedId (kv.Key :?> string), SetTransformation (kv.Value :?> string))
        |> Map.ofSeq
        
let mapSet (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> ResponseProperty (kv.Key :?> string), StashedId (kv.Value :?> string))
        |> Map.ofSeq
        
let mapNodeSelector (operation:YamlMap) =
    let version = tryPick<string> operation "version" 
    let attribute = tryPick<YamlMap> operation "attribute"
    match (version, attribute) with
    | (version, Some attribute) ->
        let kv = attribute.First()
        let key = kv.Key :?> string
        let value = kv.Value :?> string
        match version with
        | Some version -> Some <| NodeSelector (version, key, value)
        | None -> Some <| NodeAttributeSelector(key, value)
    | (Some version, None) -> Some <| NodeVersionSelector(version)
    | _ -> None
            
    
let mapDo (operation:YamlMap) =
    
    let last = operation.Last()
    let lastKey = last.Key :?> string
    let lastValue = last.Key 
    
    let catch =
        match tryPick<string> operation "catch" with
        | Some s ->
            match s with | IsDoCatch s -> Some s | _ -> None
        | _ -> None
        
    let warnings =
        match tryPick<List<string>> operation "warnings" with
        | Some s -> Some (s |> Seq.toList)
        | _ -> None
        
    let nodeSelector = mapNodeSelector operation
    Do {
        ApiCall = (lastKey, lastValue)
        Catch = catch
        Warnings = warnings
        NodeSelector = nodeSelector
    }
    
let mapOperation (operation:YamlMap) =
    let kv = operation.First();
    let key = kv.Key :?> string
    let yamlMap = kv.Value :?> YamlMap
    
    match key with
    | "skip" -> mapSkip yamlMap
    | "set" -> Set <| mapSet yamlMap
    | "transform_and_set" -> TransformAndSet <| mapTransformAndSet yamlMap
    | "do" -> mapDo yamlMap
    | "match" ->  Assert <| Match (mapMatch yamlMap)
    | "is_false" -> Assert <| IsFalse (firstValueAsPath yamlMap)
    | "is_true" -> Assert <| IsTrue (firstValueAsPath yamlMap)
    | IsNumericAssert n -> Assert <| NumericAssert (n, mapNumericAssert yamlMap)
    | unknownOperation -> Unknown unknownOperation
    
let mapOperations (operations:YamlMap list) =
    operations |> List.map mapOperation
    
let mapDocument (document:Dictionary<string, Object>) =
    
    let kv = document.First();
    
    let list = (kv.Value :?> List<Object>) |> Enumerable.Cast<YamlMap> |> Seq.toList |> mapOperations
    
    document

let test () = 
    let document = "/tmp/elastic/tests-master/cat.aliases/10_basic.yml"

    let x = SharpYaml.Serialization.Serializer()
    let contents = System.IO.File.ReadAllText document
    let tests =
        Regex.Split(contents, @"--- ?\r?\n")
        |> Seq.filter (fun s -> not <| String.IsNullOrEmpty s)
        |> Seq.map (fun document -> x.Deserialize<Dictionary<string, Object>> document)
        |> Seq.filter (fun s -> s <> null)
        |> Seq.map mapDocument
        |> Seq.toList
        
    
    tests

