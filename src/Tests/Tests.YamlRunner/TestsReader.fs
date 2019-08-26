module Tests.YamlRunner.TestsReader

open System
open System.Collections.Generic
open System.Text.RegularExpressions
open System.Linq

open Tests.YamlRunner.Models

type YamlMap = Dictionary<Object,Object>

let private tryPick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then Some (value :?> 'a) else None
    
let private pick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then (value :?> 'a)
    else failwithf "expected to find %s of type %s" key typeof<'a>.Name

let private mapSkip (operation:YamlMap) =
    let version = pick<string> operation "version" 
    let reason = tryPick<string> operation "reason"
    Skip { Version=version; Reason=reason }
    
let private mapNumericAssert (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> AssertPath (kv.Key :?> string), kv.Value :?> int64)
        |> Map.ofSeq
        
let private firstValueAsPath (operation:YamlMap) = AssertPath (operation.Values.First() :?> string)

let private mapMatch (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> AssertPath (kv.Key :?> string), kv.Value)
        |> Map.ofSeq
        
let private mapTransformAndSet (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> StashedId.Create  (kv.Key :?> string), SetTransformation.Create (kv.Value :?> string))
        |> Map.ofSeq
        
let private mapSet (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) -> ResponseProperty (kv.Key :?> string), StashedId.Create (kv.Value :?> string))
        |> Map.ofSeq
        
let private mapNodeSelector (operation:YamlMap) =
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
            
    
let private mapDo (operation:YamlMap) =
    
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
    
let private mapOperation (operation:YamlMap) =
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
    
let private mapOperations (operations:YamlMap list) =
    operations |> List.map mapOperation
    
let private mapDocument (document:Dictionary<string, Object>) =
    
    let kv = document.First();
    let key = kv.Key
    let values = kv.Value :?> List<Object>
    
    let operations = values |> Enumerable.Cast<YamlMap> |> Seq.toList |> mapOperations
    
    match key with
    | "setup" -> Setup operations
    | "teardown" -> Teardown operations
    | name -> YamlTest { Name=name; Operations=operations }

let ReadYamlFile yamlString = 

    let serializer = SharpYaml.Serialization.Serializer()
    let sections =
        Regex.Split(yamlString, @"--- ?\r?\n")
        |> Seq.filter (fun s -> not <| String.IsNullOrEmpty s)
        |> Seq.map (fun document -> serializer.Deserialize<Dictionary<string, Object>> document)
        |> Seq.filter (fun s -> s <> null)
        |> Seq.map mapDocument
        |> Seq.toList
        
    sections

