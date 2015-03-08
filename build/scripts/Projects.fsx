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
        | f when f = "elasticsearch.net.connection.thrift" -> 
            Some "Add thrift support to elasticsearch."
        | f when f = "elasticsearch.net.connection.httpclient" -> 
            Some "IConnection implementation that uses HttpClient (.NET 4.5 only)"
        | f when f = "elasticsearch.net.jsonnet" -> 
            Some "IElasticsearchSerializer implementation that allows you to use Json.NET with the lowlevel client"
        | _ -> None

type DotNetFramework = 
    | NET40 
    | NET45 
    static member All = [NET40; NET45] 
    member this.Identifier = 
        match this with
        | NET40 -> { MSBuild = "v4.0"; Nuget = "net40"; }
        | NET45 -> { MSBuild = "v4.5"; Nuget = "net45"; }
    
type DotNet40Project =
    | Nest
    | ElasticsearchNet
    | ElasticsearchNetJsonNet
    | ElasticsearchNetConnectionThrift
    static member All = [ElasticsearchNet; ElasticsearchNetJsonNet; ElasticsearchNetConnectionThrift; Nest] 

type DotNet45Project = 
    | ElasticsearchNetConnectionHttpClient
    static member All = [ElasticsearchNetConnectionHttpClient] 

type DotNetProject = 
    | DotNet40Project of DotNet40Project
    | DotNet45Project of DotNet45Project
    static member All =
        Seq.concat [
            DotNet40Project.All |> List.map(fun p -> DotNet40Project p);
            DotNet45Project.All |> List.map(fun p -> DotNet45Project p)
        ]

    member this.ProjectName =
        match this with
        | DotNet40Project net40 ->
            match net40 with
            | Nest -> ProjectName "Nest"
            | ElasticsearchNet -> ProjectName "Elasticsearch.Net"
            | ElasticsearchNetJsonNet -> ProjectName "Serialization\Elasticsearch.Net.JsonNet"
            | ElasticsearchNetConnectionThrift -> ProjectName "Connections\Elasticsearch.Net.Connection.Thrift"
        | DotNet45Project net45 -> 
            match net45 with
            | ElasticsearchNetConnectionHttpClient -> ProjectName "Connections\Elasticsearch.Net.Connection.HttpClient"
    
    static member TryFindName (name: string) =
        DotNetProject.All
        |> Seq.map(fun p -> p.ProjectName)
        |> Seq.tryFind(fun p -> p.Nuget.ToLowerInvariant() = name.ToLowerInvariant())
