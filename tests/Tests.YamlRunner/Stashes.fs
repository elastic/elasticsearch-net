module Tests.YamlRunner.Stashes

open Elasticsearch.Net
open System
open System.Collections.Generic
open System.Drawing
open System.IO
open System.Text
open Tests.YamlRunner.Models
open ShellProgressBar

type Stashes() =
    inherit Dictionary<StashedId, Object>()
    
    member val ResponseOption:DynamicResponse option = None with get, set
    member private this.LazyResponse = lazy(this.ResponseOption.Value)
    member this.Response () = this.LazyResponse.Force()
    
    member this.ResolvePath (progress:IProgressBar) (path:String) =
        let r = this.ResolveToken progress
        let tokens =
            path.Split('.')
            |> Seq.map r
        String.Join('.', tokens)
        
    member this.GetResponseValue (progress:IProgressBar) (path:String) =
        let g = this.Response().Get 
        match path with
        | "$body" -> g "body"
        | _ -> 
            let path = this.ResolvePath progress path
            g path
    
    member this.Resolve (progress:IProgressBar) (object:YamlMap) =
        let rec resolve (o:Object) : Object =
            match o with
            | :? List<Object> as list ->
                let resolved = new List<Object>();
                list |> Seq.iter(fun i -> resolved.Add <| resolve i)
                resolved :> Object
            | :? YamlMap as map ->
                let newDict = YamlMap()
                map
                |> Seq.iter (fun kv -> newDict.[kv.Key] <- resolve kv.Value)
                newDict :> Object
            | :? String as value -> this.ResolveToken progress value 
            | value -> value
        
        let resolved = resolve object :?> YamlMap
        resolved
        
    member this.ResolveToken (progress:IProgressBar) (s:String) : Object =
        match s with
        | s when s.StartsWith "$" ->
            let found, value = this.TryGetValue <| StashedId.Create s
            if not found then
                let s = sprintf "Expected to resolve %s but no such value was stashed at this point" s 
                progress.WriteLine s 
                failwith s
            value 
        | s -> s :> Object
        
        
        
        
