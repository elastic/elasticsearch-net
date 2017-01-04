#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open Fake 

open Paths
open Projects
open Tooling
open Versioning

module Release = 
    let NugetPack() = 
        DotNetProject.AllPublishable
        |> Seq.iter(fun p ->
            CreateDir Paths.NugetOutput

            let name = p.Name;
            let nuspec = (sprintf @"build\%s.nuspec" name)
            let nugetOutFile =  Paths.Output(sprintf "%s.%s.nupkg" name Versioning.FileVersion)

            Tooling.Nuget.Exec ["pack"; nuspec; "-version"; Versioning.FileVersion; "-outputdirectory"; Paths.BuildOutput; ] |> ignore
            traceFAKE "%s" Paths.BuildOutput
            MoveFile Paths.NugetOutput nugetOutFile
        )

    let PublishCanaryBuild accessKey feed = 
        !! "build/output/_packages/*-ci*.nupkg"
        |> Seq.iter(fun f -> 
            let source = "https://www.myget.org/F/"+ feed + "/api/v2/package"
            let success = Tooling.Nuget.Exec ["push"; f; accessKey; "-source"; source; "-verbosity"; "detailed"; "-noninteractive"] 
            match success with
            | 0 -> traceFAKE "publish to myget succeeded" |> ignore
            | _ -> failwith "publish to myget failed" |> ignore
        )