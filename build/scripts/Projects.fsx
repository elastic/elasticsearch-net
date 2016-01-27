#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake 

type FrameworkName = { MSBuild: string; Nuget: string; }
type ProjectName(msbuild: string) = 
    member this.Nuget = regex_replace @"^.*\\" "" msbuild
    member this.MSBuild = if isMono then this.Nuget else msbuild
    member this.Location = msbuild
    
    member this.NugetDescription = 
        match this.Nuget.ToLowerInvariant() with
        | f when f = "elasticsearch.net" -> 
            Some "Elasticsearch.Net - official low level elasticsearch client"
        | f when f = "nest" -> 
            Some "NEST - official high level elasticsearch client"
        | _ -> None

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

    member this.ProjectName =
        match this with
        | Project p ->
            match p with
            | Nest -> ProjectName "Nest"
            | ElasticsearchNet -> ProjectName "Elasticsearch.Net"
   
    static member TryFindName (name: string) =
        DotNetProject.All
        |> Seq.map(fun p -> p.ProjectName)
        |> Seq.tryFind(fun p -> p.Nuget.ToLowerInvariant() = name.ToLowerInvariant())
