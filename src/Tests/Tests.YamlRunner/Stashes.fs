module Tests.YamlRunner.Stashes

open Elasticsearch.Net
open System
open System.Collections.Generic
open System.IO
open System.Text
open Tests.YamlRunner.Models

type Stashes() =
    inherit Dictionary<StashedId, Object>()
    
    member val Response:StringResponse option = None with get, set
    
    member this.Dictionary = lazy(
        let r = this.Response.Value
        
        use s = new MemoryStream(Encoding.UTF8.GetBytes r.Body)
        r.ConnectionConfiguration.RequestResponseSerializer.Deserialize<DynamicDictionary> s
    )
    
    member this.GetResponseValue path = this.Dictionary.Force().Get path
    
    member this.Resolve (object:YamlMap) =
        let rec resolve (o:Object) : Object =
            match o with
            | :? YamlMap as map ->
                let newDict = YamlMap()
                map
                |> Seq.iter (fun kv -> newDict.[kv.Key] <- resolve kv.Value)
                newDict :> Object
            | :? String as value -> value :> Object
            | value -> value
        
        let resolved = resolve object :?> YamlMap
        resolved
