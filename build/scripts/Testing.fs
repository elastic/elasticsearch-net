namespace Scripts

open System
open System.Globalization
open Tooling
open Fake.Core
open System.IO
open Commandline
open Versioning

module Tests =

    let private buildingOnAzurePipeline = Environment.environVarAsBool "TF_BUILD"
    let private buildingOnTeamCity = match Environment.environVarOrNone "TEAMCITY_VERSION" with | Some _ -> true | None -> false

    let SetTestEnvironmentVariables args = 
        let clusterFilter = match args.CommandArguments with | Integration a -> a.ClusterFilter | _ -> None
        let testFilter = match args.CommandArguments with | Integration a -> a.TestFilter | Test t -> t.TestFilter | _ -> None
        
        let env key v =
            match v with
            | Some v -> Environment.setEnvironVar key <| sprintf "%O" v
            | None -> ignore()
        
        env "NEST_INTEGRATION_CLUSTER" clusterFilter
        env "NEST_TEST_FILTER" testFilter
        let seed =
            match args.Seed with
            | Some i -> Some <| i.ToString(CultureInfo.InvariantCulture)
            | None -> None
        env "NEST_TEST_SEED" seed 

        for random in args.RandomArguments do 
            let tokens = random.Split [|':'|]
            let key = tokens.[0].ToUpper()
            let b = if tokens.Length = 1 then true else (bool.Parse (tokens.[1]))
            let key = sprintf "NEST_RANDOM_%s" key
            let value = (if b then "true" else "false")
            env key (Some <| value)
        ignore()

    let private dotnetTest (target: Commandline.MultiTarget) seed =
        Directory.CreateDirectory Paths.BuildOutput |> ignore
        let command = 
            let p = ["test"; "."; "-c"; "RELEASE"]
            //make sure we only test netcoreapp on linux or requested on the command line to only test-one
            match (target, Environment.isLinux) with 
            | (_, true) -> ["--framework"; "netcoreapp3.0"] |> List.append p
            | (Commandline.MultiTarget.One, _) ->
                let random = match seed with | Some i -> Random(i) | None -> Random()
                let fw = DotNetFramework.AllTests |> List.sortBy (fun _ -> random.Next()) |> List.head
                ["--framework"; fw.Identifier.MSBuild] |> List.append p
            | _  -> p
        let commandWithCodeCoverage =
            // TODO /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
            // Using coverlet.msbuild package
            // https://github.com/tonerdo/coverlet/issues/110
            // Bites us here as well a PR is up already but not merged will try again afterwards
            // https://github.com/tonerdo/coverlet/pull/329
            match (buildingOnAzurePipeline) with
            | (true) -> [ "--logger"; "trx"; "--collect"; "\"Code Coverage\""; "-v"; "m"] |> List.append command
            | _  -> command
            
        if Environment.UserInteractive then
            let out = Tooling.DotNet.StartInWithTimeout "src/Tests/Tests" commandWithCodeCoverage (TimeSpan.FromMinutes 30.)
            if out.ExitCode <> 0 then failwith "dotnet test failed"
        else 
            Tooling.DotNet.ExecInWithTimeout "src/Tests/Tests" commandWithCodeCoverage (TimeSpan.FromMinutes 30.)

    let RunReleaseUnitTests (ArtifactsVersion(version)) seed =
        //xUnit always does its own build, this env var is picked up by Tests.csproj
        //if its set it will include the local package source (build/output/_packages)
        //and references NEST and NEST.JsonNetSerializer by the current version
        //this works by not including the local package cache (nay source) 
        //in the project file via:
        //<RestoreSources></RestoreSources>
        //This will download all packages but its the only way to make sure we reference the built
        //package and not one from cache...y
        Environment.setEnvironVar "TestPackageVersion" (version.Full.ToString())
        Tooling.DotNet.ExecIn "src/Tests/Tests" ["clean";] |> ignore
        Tooling.DotNet.ExecIn "src/Tests/Tests" ["restore";] |> ignore
        dotnetTest Commandline.MultiTarget.One seed

    let RunUnitTests args = dotnetTest args.MultiTarget args.Seed 

    let RunIntegrationTests args =
        let passedVersions = match args.CommandArguments with | Integration a -> Some a.ElasticsearchVersions | _ -> None
        match passedVersions with
        | None -> failwith "No versions specified to run integration tests against"
        | Some esVersions ->
            for esVersion in esVersions do
                Environment.setEnvironVar "NEST_INTEGRATION_TEST" "1"
                Environment.setEnvironVar "NEST_INTEGRATION_VERSION" esVersion
                dotnetTest args.MultiTarget args.Seed |> ignore
