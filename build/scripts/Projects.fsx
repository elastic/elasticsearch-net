#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

open Fake 

type Project =
    | Nest
    | ElasticsearchNet
    static member All = [ElasticsearchNet;  Nest] 

type DotNetProject = 
    | Project of Project
    static member All =
        Seq.concat [
            Project.All |> List.map(fun p -> Project p);
        ]

    member this.Name =
        match this with
        | Project p ->
            match p with
            | Nest -> "Nest"
            | ElasticsearchNet -> "Elasticsearch.Net"
   
    static member TryFindName (name: string) =
        DotNetProject.All
        |> Seq.map(fun p -> p.Name)
        |> Seq.tryFind(fun p -> p.ToLowerInvariant() = name.ToLowerInvariant())
