#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake

#load @"Paths.fsx"

open System
open System.IO
open System.Linq
open System.Diagnostics
open Paths

module Benchmarker =
   let private testsProjectDirectory = Path.GetFullPath(Paths.Source("Tests"))
   let private benchmarkOutput = Path.GetFullPath(Paths.Output("benchmarks")) |> directoryInfo

   let private copyToOutput file = CopyFile benchmarkOutput.FullName file

   let Run() =
        ensureDirExists benchmarkOutput
        let projectJson = testsProjectDirectory @@ "project.json"
        // running benchmarks can timeout so clean up any generated benchmark files
        try
            DotNetCli.Restore(fun p ->
                { p with
                    Project = projectJson
                })

            DotNetCli.RunCommand(fun p ->
                { p with
                    WorkingDir = testsProjectDirectory
                }) "run Benchmark"
        finally
            let benchmarkOutputFiles =
                let output = combinePaths testsProjectDirectory "BenchmarkDotNet.Artifacts"
                Directory.EnumerateFiles(output, "*.*", SearchOption.AllDirectories)
                |> Seq.toList

            for file in benchmarkOutputFiles do copyToOutput file
            DeleteFiles benchmarkOutputFiles
