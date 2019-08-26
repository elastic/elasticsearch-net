module Tests.YamlRunner.TestsReader

open System
open System.Collections.Generic
open System.Text.RegularExpressions
open System.Linq

open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsLocator

type YamlMap = Dictionary<Object,Object>
type YamlValue = YamlDictionary of YamlMap | YamlString of string

let private tryPick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then Some (value :?> 'a) else None
    
let private tryPickList<'a,'b> (map:YamlMap) key parse =
    let found, value =  map.TryGetValue key
    if (found) then
        let value =
            value :?> List<Object>
            |> Seq.map (fun o -> o :?> 'a)
            |> Seq.map parse
            |> Seq.toList<'b>
        Some value
    else None
    
let private pick<'a> (map:YamlMap) key =
    let found, value =  map.TryGetValue key
    if (found) then (value :?> 'a)
    else failwithf "expected to find %s of type %s" key typeof<'a>.Name

let private mapSkip (operation:YamlMap) =
    let version = tryPick<string> operation "version" 
    let reason = tryPick<string> operation "reason"
    let parseFeature s = match s with | ToFeature s -> s
    let features =
        let found, value= operation.TryGetValue "features"
        match (found, value) with
        | (false, _) -> None
        | (_, x) ->
            match x with 
            | :? List<Object> -> tryPickList<string, Feature> operation "features" parseFeature
            | :? String as feature -> Some [parseFeature feature]
            | _ -> None
        
        
    Skip { Version=version; Reason=reason; Features=features }
    
let private mapNumericAssert (operation:YamlMap) =
    operation
        |> Seq.map (fun (kv) ->
            let v =
                match kv.Value with
                | :? int32 as i -> Fixed <| Convert.ToDouble i
                | :? int64 as i -> Fixed <| Convert.ToDouble i
                | :? double as i -> Fixed <| Convert.ToDouble i
                | :? string as i -> StashedId <| StashedId.Create i
                | _ -> failwithf "unsupported %s" (kv.Value.GetType().Name)
            AssertPath (kv.Key :?> string), v
        )
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
        | Some version -> Some <| VersionAndAttributeSelector (version, key, value)
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
        
    let warnings = tryPickList<string, string> operation "warnings" id
        
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
    let yamlValue : YamlValue =
        match kv.Value with
        | :? YamlMap as m -> YamlDictionary m
        | :? string as s -> YamlString s
        | _ -> failwithf "unsupported %s" (kv.Value.GetType().Name)
    
    match key with
    | IsOperation s ->
        match (s, yamlValue) with
        | ("skip", YamlDictionary map) -> mapSkip map
        | ("set", YamlDictionary map) -> Set <| mapSet map
        | ("transform_and_set", YamlDictionary map) -> TransformAndSet <| mapTransformAndSet map
        | ("do", YamlDictionary map) -> mapDo map
        | ("match", YamlDictionary map) ->  Assert <| Match (mapMatch map)
        | ("is_false", YamlDictionary map) -> Assert <| IsFalse (firstValueAsPath map)
        | ("is_true", YamlDictionary map) -> Assert <| IsTrue (firstValueAsPath map)
        | ("is_false", YamlString str) -> Assert <| IsFalse (AssertPath str)
        | ("is_true", YamlString str) -> Assert <| IsTrue (AssertPath str)
        | (IsNumericAssert n, YamlDictionary map) -> Assert <| NumericAssert (n, mapNumericAssert map)
        | (k, v) -> failwithf "%s does not support %s" k (v.GetType().Name)
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

type YamlTestDocument = {
    Setup: Operations option
    Teardown: Operations option
    Tests: YamlTest list
}

let private toDocument (sections:YamlTestSection list) =
    {
        Setup = sections |> List.tryPick (fun s -> match s with | Setup s -> Some s | _ -> None)
        Teardown = sections |> List.tryPick (fun s -> match s with | Teardown s -> Some s | _ -> None)
        Tests = sections |> List.map (fun s -> match s with | YamlTest s -> Some s | _ -> None) |> List.choose id
    }

type ReadResults = { Folder: string; Files: YamlTestDocument list } 

let ReadYamlFile (yamlInfo:YamlFileInfo) = 

    let serializer = SharpYaml.Serialization.Serializer()
    let sections =
        let r e message = raise <| new Exception(message, e)
        Regex.Split(yamlInfo.Yaml, @"---\s*?\r?\n")
        |> Seq.filter (fun s -> not <| String.IsNullOrWhiteSpace s)
        |> Seq.map (fun sectionString ->
            try
                (sectionString, serializer.Deserialize<Dictionary<string, Object>> sectionString)
            with | e -> r e <| sprintf "parseError %s: %s %s %s" yamlInfo.File e.Message Environment.NewLine sectionString
        )
        |> Seq.filter (fun (s, _) -> s <> null)
        |> Seq.map (fun (s, document) ->
            try
                mapDocument document
            with | e -> r e <| sprintf "mapError %s: %O %O %O" yamlInfo.File (e.Message) Environment.NewLine s
        )
        |> Seq.toList
        |> toDocument
        
    sections 

