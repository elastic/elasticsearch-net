#r "../../packages/build/NEST/lib/net46/Nest.dll"
#r "../../packages/build/Elasticsearch.Net/lib/net46/Elasticsearch.Net.dll"
#r "../../packages/build/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r "../../packages/build/FSharp.Data/lib/net45/FSharp.Data.dll"
#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#nowarn "0044" //TODO sort out FAKE 5

open Fake

#load @"Paths.fsx"

open System
open System.IO
open System.Linq
open System.Diagnostics
open Paths

open FSharp.Data

open Nest
open Elasticsearch.Net
open Newtonsoft.Json
open Git.Branches
open Git.Information

module Benchmarker =

    let pipelineName = "benchmark-pipeline"
    let indexName = IndexName.op_Implicit("benchmark-reports")
    let typeName = TypeName.op_Implicit("benchmarkreport")
    
    let private testsProjectDirectory = Path.GetFullPath(Paths.TestsSource("Tests.Benchmarking"))

    let Run(runInteractive:bool) =

        let url = getBuildParam "elasticsearch"
        let username = getBuildParam "username"
        let password = getBuildParam "password"
        
        try
            if runInteractive then
                DotNetCli.RunCommand(fun p ->
                    { p with
                        WorkingDir = testsProjectDirectory
                    }) "run -f netcoreapp2.1 -c Release"
             else
                DotNetCli.RunCommand(fun p ->
                    { p with
                        WorkingDir = testsProjectDirectory
                    }) "run -f netcoreapp2.1 -c Release non-interactive"
