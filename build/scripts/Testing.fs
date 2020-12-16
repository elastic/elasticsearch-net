// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System
open System.Globalization
open Fake.Core
open System.IO
open Commandline
open Versioning

module Tests =


    let SetTestEnvironmentVariables args = 
        let clusterFilter = match args.CommandArguments with | Integration a -> a.ClusterFilter | _ -> None
        let testFilter = match args.CommandArguments with | Integration a -> a.TestFilter | Test t -> t.TestFilter | _ -> None
        
        let env key v =
            match v with
            | Some v -> Environment.setEnvironVar key <| sprintf "%O" v
            | None -> ignore()
        
        env "NEST_INTEGRATION_CLUSTER" clusterFilter
        env "NEST_TEST_FILTER" testFilter
        let seed = Some <| args.Seed.ToString(CultureInfo.InvariantCulture)
        env "NEST_TEST_SEED" seed 

        for random in args.RandomArguments do 
            let tokens = random.Split [|':'|]
            let key = tokens.[0].ToUpper()
            let b = if tokens.Length = 1 then true else (bool.Parse (tokens.[1]))
            let key = sprintf "NEST_RANDOM_%s" key
            let value = (if b then "true" else "false")
            env key (Some <| value)
        ignore()

    let private dotnetTest proj args =
        let runSettings =
            // force the logger section to be cleared so that azure devops can work its magic.
            // relies heavily on the original console logger
            let prefix = if runningOnAzureDevops then ".ci" else ""
            sprintf "tests/%s.runsettings" prefix
        
        Directory.CreateDirectory Paths.BuildOutput |> ignore
        let command = ["test"; proj; "--nologo"; "-c"; "Release"; "-s"; runSettings; "--no-build"]
        
        let wantsTrx =
            let wants = match args.CommandArguments with | Integration a -> a.TrxExport | Test t -> t.TrxExport | _ -> false
            match wants with | true -> ["--logger"; "trx"] | false -> []
        let wantsCoverage =
            let wants = match args.CommandArguments with | Test t -> t.CodeCoverage | _ -> false
            match wants with | true -> ["--collect:\"XPlat Code Coverage\""] | false -> []
           
        let commandWithAdditionalOptions =
            wantsCoverage |> List.append wantsTrx |> List.append command
            
        Tooling.DotNet.ExecInWithTimeout "." commandWithAdditionalOptions (TimeSpan.FromMinutes 60.)

    let RunReleaseUnitTests version args =
        //xUnit always does its own build, this env var is picked up by Tests.csproj
        //if its set it will include the local package source (build/output/)
        //and references NEST and NEST.JsonNetSerializer by the current version
        //this works by not including the local package cache (nay source) 
        //in the project file via:
        //<RestoreSources></RestoreSources>
        //This will download all packages but its the only way to make sure we reference the built
        //package and not one from cache...y
        Environment.setEnvironVar "TestPackageVersion" (version.Full.ToString())
        Tooling.DotNet.ExecIn "tests/Tests" ["clean";] |> ignore
        Tooling.DotNet.ExecIn "tests/Tests" ["restore";] |> ignore
        dotnetTest "tests/Tests/Tests.csproj" args

    let RunUnitTests args =
        dotnetTest "tests/tests.proj" args

    let RunIntegrationTests args =
        let passedVersions = match args.CommandArguments with | Integration a -> Some a.ElasticsearchVersions | _ -> None
        match passedVersions with
        | None -> failwith "No versions specified to run integration tests against"
        | Some esVersions ->
            for esVersion in esVersions do
                Environment.setEnvironVar "NEST_INTEGRATION_TEST" "1"
                Environment.setEnvironVar "NEST_INTEGRATION_VERSION" esVersion
                dotnetTest "tests/Tests/Tests.csproj" args
