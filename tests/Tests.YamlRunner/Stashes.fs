// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.Stashes

open System.Text.RegularExpressions
open Elastic.Transport
open System
open System.Collections.Generic
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
                let resolved = List<Object>();
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
    
    /// Optionally replaces all ${id} references in s, returns whether it actually replaced anything and the string
    member this.ReplaceStaches (progress:IProgressBar) (s:String) =
        match s.Contains("$") with
        | false -> (false, s)
        | true ->
            let re = Regex.Replace(s, "\$\{?\w+\}?", fun r -> (this.ResolveToken progress r.Value).ToString())
            (true, re)
        
    member this.ResolveToken (progress:IProgressBar) (s:String) : Object =
        match s with
        | s when s.StartsWith "$" ->
            let found, value = this.TryGetValue <| StashedId.Create s
            if not found then
                let s = sprintf "Expected to resolve %s but no such value was stashed at this point" s 
                progress.WriteLine s 
                failwith s
            value
        | s when s.Contains "$" ->
            let (_, r) = this.ReplaceStaches progress s
            r :> Object
        | s -> s :> Object
        
        
        
        
