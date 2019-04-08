namespace Scripts

open System
open Tooling
open Commandline
open Versioning
open Fake.Core
open System.IO

module Tests =

    let private buildingOnAzurePipeline = Environment.environVarAsBool "TF_BUILD"
    let private buildingOnTeamCity = match Environment.environVarOrNone "TEAMCITY_VERSION" with | Some x -> true | None -> false

    let SetTestEnvironmentVariables args = 
        let clusterFilter = match args.CommandArguments with | Integration a -> a.ClusterFilter | _ -> None
        let testFilter = match args.CommandArguments with | Integration a -> a.TestFilter | Test t -> t.TestFilter | _ -> None
        
        let env key v =
            match v with
            | Some v -> Environment.setEnvironVar key <| sprintf "%O" v
            | None -> ignore()
        
        env "NEST_INTEGRATION_CLUSTER" clusterFilter
        env "NEST_TEST_FILTER" testFilter
        env "NEST_TEST_SEED" (Some <| args.Seed)

        for random in args.RandomArguments do 
            let tokens = random.Split [|':'|]
            let key = tokens.[0].ToUpper()
            let b = if tokens.Length = 1 then true else (bool.Parse (tokens.[1]))
            let key = sprintf "NEST_RANDOM_%s" key
            let value = (if b then "true" else "false")
            env key (Some <| value)
        ignore()

    let private dotnetTest (target: Commandline.MultiTarget) =
        Directory.CreateDirectory Paths.BuildOutput |> ignore
        let command = 
            let p = ["test"; "."; "-c"; "RELEASE"]
            //make sure we only test netcoreapp on linux or requested on the command line to only test-one
            match (target, Environment.isLinux) with 
            | (_, true) 
            | (Commandline.MultiTarget.One, _) -> ["--framework"; "netcoreapp2.1"] |> List.append p
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
            
        // using std redirection since that foces vstest not to redirect Console.Write from Elastic.Xunit somehow
        // still trying to work out whats going on there
        // No fancy colors on the command line using this though 
        Tooling.DotNet.ReadInWithTimeout "src/Tests/Tests" commandWithCodeCoverage (TimeSpan.FromMinutes 30.) 

    let RunReleaseUnitTests (ArtifactsVersion(version)) =
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
        dotnetTest Commandline.MultiTarget.One 

    let RunUnitTests args = dotnetTest args.MultiTarget 

    let RunIntegrationTests args =
        let passedVersions = match args.CommandArguments with | Integration a -> Some a.ElasticsearchVersions | _ -> None
        match passedVersions with
        | None -> failwith "No versions specified to run integration tests against"
        | Some esVersions ->
            for esVersion in esVersions do
                Environment.setEnvironVar "NEST_INTEGRATION_VERSION" esVersion
                dotnetTest args.MultiTarget |> ignore
