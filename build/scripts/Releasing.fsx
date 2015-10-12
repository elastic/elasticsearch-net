#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
#load @"Projects.fsx"
#load @"Versioning.fsx"
open System
open Fake 
open Paths
open Projects
open Versioning

type Release() = 
    static let nugetPack = fun (projectName: ProjectName) ->
        let name = projectName.Nuget;
        CreateDir Paths.NugetOutput
        let package = (sprintf @"build\%s.nuspec" name)
        let packageContents = ReadFileAsString package
        let re = @"(?<start>\<version\>|""(Elasticsearch.Net|Nest)"" version="")[^""><]+(?<end>\<\/version\>|"")"
        let replacedContents = regex_replace re (sprintf "${start}%s${end}" Versioning.FileVersion) packageContents
        WriteStringToFile false package replacedContents
    
        let dir = sprintf "%s/%s/" Paths.BuildOutput name
        let nugetOutFile =  Paths.Output(sprintf "%s/%s.%s.nupkg" name name Versioning.FileVersion);
        NuGetPack (fun p ->
          {p with 
            Version = Versioning.FileVersion
            WorkingDir = "build" 
            OutputPath = dir
          })
          package
        traceFAKE "%s" dir
        MoveFile Paths.NugetOutput nugetOutFile

    static member PackAll() =
        DotNetProject.All
        |> Seq.map (fun p -> p.ProjectName)
        |> Seq.iter(fun p -> nugetPack p)
