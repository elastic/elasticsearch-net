#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.IO.Compression.FileSystem.dll"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake

[<AutoOpen>]
module Projects = 
    type DotNetFrameworkIdentifier = { MSBuild: string; Nuget: string; DefineConstants: string; }

    type DotNetFramework = 
        | Net45 
        | Net46 
        | NetStandard1_3
        | NetCoreApp1_1
        static member All = [Net45; Net46; NetStandard1_3] 
        member this.Identifier = 
            match this with
            | Net45 -> { MSBuild = "v4.5"; Nuget = "net45"; DefineConstants = if not isMono then "TRACE;NET45" else "NET45"; }
            | Net46 -> { MSBuild = "v4.6"; Nuget = "net46"; DefineConstants = if not isMono then "TRACE;NET46" else "NET46"; }
            | NetStandard1_3 -> { MSBuild = "netstandard1.3"; Nuget = "netstandard1.3"; DefineConstants = if not isMono then "TRACE;DOTNETCORE" else "DOTNETCORE"; }
            | NetCoreApp1_1 -> { MSBuild = "netcoreapp1.1"; Nuget = "netcoreapp1.1"; DefineConstants = if not isMono then "TRACE;DOTNETCORE" else "DOTNETCORE"; }

    type Project =
        | Nest
        | ElasticsearchNet

    type PrivateProject =
        | Tests
        | DocGenerator


    type DotNetProject = 
        | Project of Project
        | PrivateProject of PrivateProject

        static member All = 
            seq [
                Project Project.ElasticsearchNet; 
                Project Project.Nest; 
                PrivateProject PrivateProject.Tests
            ]

        static member AllPublishable = seq [Project Project.ElasticsearchNet; Project Project.Nest;] 
        static member Tests = seq [PrivateProject PrivateProject.Tests;] 

        member this.Name =
            match this with
            | Project p ->
                match p with
                | Nest -> "Nest"
                | ElasticsearchNet -> "Elasticsearch.Net"
            | PrivateProject p ->
                match p with
                | Tests -> "Tests"
                | DocGenerator -> "DocGenerator"
       
        static member TryFindName (name: string) =
            DotNetProject.All
            |> Seq.map(fun p -> p.Name)
            |> Seq.tryFind(fun p -> p.ToLowerInvariant() = name.ToLowerInvariant())

    type DotNetFrameworkProject = { framework: DotNetFramework; project: DotNetProject }
    let AllPublishableProjectsWithSupportedFrameworks = seq {
        for framework in DotNetFramework.All do
        for project in DotNetProject.AllPublishable do
            yield { framework = framework; project= project}
        }

