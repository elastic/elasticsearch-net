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
            Some "Elasticsearch.Net - oficial low level elasticsearch client"
        | f when f = "nest" -> 
            Some "NEST - oficial high level elasticsearch client"
        | _ -> None

type DotNetFramework = 
    | NET40 
    | NET45 
    static member All = [NET45] 
    member this.Identifier = 
        match this with
        | NET40 -> { MSBuild = "v4.0"; Nuget = "net40"; }
        | NET45 -> { MSBuild = "v4.5"; Nuget = "net45"; }
    
type DotNet45Project =
    | Nest
    | ElasticsearchNet
    static member All = [ElasticsearchNet;  Nest] 

type DotNet40Project =
    | None
    static member All = [] 

type DotNetProject = 
    | DotNet45Project of DotNet45Project
    static member All =
        Seq.concat [
            DotNet45Project.All |> List.map(fun p -> DotNet45Project p);
        ]

    member this.ProjectName =
        match this with
        | DotNet45Project net45 ->
            match net45 with
            | Nest -> ProjectName "Nest"
            | ElasticsearchNet -> ProjectName "Elasticsearch.Net"
   
    static member TryFindName (name: string) =
        DotNetProject.All
        |> Seq.map(fun p -> p.ProjectName)
        |> Seq.tryFind(fun p -> p.Nuget.ToLowerInvariant() = name.ToLowerInvariant())
