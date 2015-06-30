#I @"../tools/FAKE/tools"
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
            Some "Elasticsearch.Net - oficial low level elasticsearch client"
        | f when f = "nest" -> 
            Some "NEST - oficial high level elasticsearch client"
        | _ -> None

type DotNetFramework = 
    | NET40 
    static member All = [NET40] 
    member this.Identifier = 
        match this with
        | NET40 -> { MSBuild = "v4.0"; Nuget = "net40"; }
    
type DotNet40Project =
    | Nest
    | ElasticsearchNet
    static member All = [ElasticsearchNet;  Nest] 

type DotNetProject = 
    | DotNet40Project of DotNet40Project
    static member All =
        Seq.concat [
            DotNet40Project.All |> List.map(fun p -> DotNet40Project p);
        ]

    member this.ProjectName =
        match this with
        | DotNet40Project net40 ->
            match net40 with
            | Nest -> ProjectName "Nest"
            | ElasticsearchNet -> ProjectName "Elasticsearch.Net"
   
    static member TryFindName (name: string) =
        DotNetProject.All
        |> Seq.map(fun p -> p.ProjectName)
        |> Seq.tryFind(fun p -> p.Nuget.ToLowerInvariant() = name.ToLowerInvariant())
