#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"
#load @"Projects.fsx"
#load @"Versioning.fsx"
#load @"Building.fsx"

open System
open System.IO

open Fake 
open FSharp.Data

open Paths
open Projects
open Versioning
open Building

type Release() = 
    static member NugetPack() =
        DotNetProject.All
        |> Seq.iter(fun p ->
            CreateDir Paths.NugetOutput
            let name = p.Name;
            let nuspec = (sprintf @"build\%s.nuspec" name)
            let outputDirectory = sprintf "%s/%s/" Paths.BuildOutput name
            let nugetOutFile =  Paths.Output(sprintf "%s/%s.%s.nupkg" name name Versioning.FileVersion)
            Tooling.Nuget.Exec ["pack"; nuspec; "-version"; Versioning.FileVersion; "-outputdirectory"; outputDirectory; ] |> ignore
            traceFAKE "%s" outputDirectory
            MoveFile Paths.NugetOutput nugetOutFile
        )

    static member PublishCanaryBuild accessKey feed = 
        !! "build/output/_packages/*-ci*.nupkg"
        |> Seq.iter(fun f -> 
            let source = "https://www.myget.org/F/"+ feed + "/api/v2/package"
            let success = Tooling.Nuget.Exec ["push"; f; accessKey; "-source"; source; "-verbosity"; "detailed"; "-noninteractive"] 
            match success with
            | 0 -> traceFAKE "publish to myget succeeded" |> ignore
            | _ -> failwith "publish to myget failed" |> ignore
        )