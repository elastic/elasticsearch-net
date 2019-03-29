namespace Scripts

open Fake 
open Paths
open Tooling
open Commandline
open Versioning


module Tests =
    open Fake.Core
    open System

    let private buildingOnAzurePipeline = getEnvironmentVarAsBool "TF_BUILD"
    let private buildingOnTeamCity = match environVarOrNone "TEAMCITY_VERSION" with | Some x -> true | None -> false

    let SetTestEnvironmentVariables args = 
        let clusterFilter = match args.CommandArguments with | Integration a -> a.ClusterFilter | _ -> None
        let testFilter = match args.CommandArguments with | Integration a -> a.TestFilter | Test t -> t.TestFilter | _ -> None
        
        let env key v =
            match v with
            | Some v -> setProcessEnvironVar key <| sprintf "%O" v
            | None -> ignore()
        
        env "NEST_INTEGRATION_CLUSTER" clusterFilter
        env "NEST_TEST_FILTER" testFilter
        env "NEST_TEST_SEED" (Some <| args.Seed)
        env "NEST_COMMAND_LINE_BUILD" <| Some "1"

        for random in args.RandomArguments do 
            let tokens = random.Split [|':'|]
            let key = tokens.[0].ToUpper()
            let b = if tokens.Length = 1 then true else (bool.Parse (tokens.[1]))
            let key = sprintf "NEST_RANDOM_%s" key
            let value = (if b then "true" else "false")
            env key (Some <| value)
        ignore()

    let private dotnetTest (target: Commandline.MultiTarget) =
        CreateDir Paths.BuildOutput
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
            
        let dotnet = Tooling.BuildTooling("dotnet")
        let exitCode = dotnet.ExecWithTimeoutIn "src/Tests/Tests" commandWithCodeCoverage (TimeSpan.FromMinutes 30.) 
        if exitCode > 0 && not buildingOnTeamCity then raise (Exception <| (sprintf "test finished with exitCode %d" exitCode))

    let RunReleaseUnitTests (ArtifactsVersion(version)) =
        //xUnit always does its own build, this env var is picked up by Tests.csproj
        //if its set it will include the local package source (build/output/_packages)
        //and references NEST and NEST.JsonNetSerializer by the current version
        //this works by not including the local package cache (nay source) 
        //in the project file via:
        //<RestoreSources></RestoreSources>
        //This will download all packages but its the only way to make sure we reference the built
        //package and not one from cache...y
        setProcessEnvironVar "TestPackageVersion" (version.Full.ToString())
        let dotnet = Tooling.BuildTooling("dotnet")
        dotnet.ExecIn "src/Tests/Tests" ["clean";] |> ignore
        dotnet.ExecIn "src/Tests/Tests" ["restore";] |> ignore
        dotnetTest Commandline.MultiTarget.One 

    let RunUnitTests args = dotnetTest args.MultiTarget 

    let RunIntegrationTests args =
        let passedVersions = match args.CommandArguments with | Integration a -> Some a.ElasticsearchVersions | _ -> None
        match passedVersions with
        | None -> failwith "No versions specified to run integration tests against"
        | Some esVersions ->
            for esVersion in esVersions do
                setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
                dotnetTest args.MultiTarget |> ignore
