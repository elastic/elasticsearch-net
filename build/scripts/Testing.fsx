#I @"../../packages/build/FAKE/tools"
open Fake.Core
#r @"FakeLib.dll"

#load @"Commandline.fsx"
#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling
open Commandline
open Versioning


module Tests =
    open System

    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS"

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "clusterfilter" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        let numberOfConnections = getBuildParamOrDefault "numberOfConnections" ""
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter
        setProcessEnvironVar "NEST_NUMBER_OF_CONNECTIONS" numberOfConnections
        setProcessEnvironVar "NEST_TEST_SEED" Commandline.seed
        for random in Commandline.randomArgs do 
            let tokens = random.Split [|':'|]
            let key = tokens.[0].ToUpper()
            let b = if tokens.Length = 1 then true else (bool.Parse (tokens.[1]))
            let v = sprintf "NEST_RANDOM_%s" key
            setProcessEnvironVar v (if b then "true" else "false")
        ignore()

    let private dotnetTest (target: Commandline.MultiTarget) =
        CreateDir Paths.BuildOutput
        let command = 
            let p = ["xunit"; "-parallel"; "all"; "-xml"; "../.." @@ Paths.Output("TestResults-Desktop-Clr.xml")] 
            match (target, buildingOnTravis) with 
            //make sure we don't test against net46 on mono or travis systems
            | (_, true) 
            | (Commandline.MultiTarget.One, _) -> ["-framework"; "netcoreapp2.1"] |> List.append p
            | _  -> p

        let dotnet = Tooling.BuildTooling("dotnet")
        let exitCode = dotnet.ExecWithTimeoutIn "src/Tests" command (TimeSpan.FromMinutes 30.) 
        if exitCode > 0 then raise (Exception <| (sprintf "test finished with exitCode %d" exitCode))

    let RunReleaseUnitTests() =
        setLocalEnvVars()
        //xUnit always does its own build, this env var is picked up by Tests.csproj
        //if its set it will include the local package source (build/output/_packages)
        //and references NEST and NEST.JsonNetSerializer by the current version
        //this works by not including the local package cache (nay source) 
        //in the project file via:
        //<RestoreSources></RestoreSources>
        //This will download all packages but its the only way to make sure we reference the built
        //package and not one from cache...y
        setProcessEnvironVar "TestPackageVersion" (Versioning.CurrentVersion.ToString())
        let dotnet = Tooling.BuildTooling("dotnet")
        dotnet.ExecIn "src/Tests" ["clean";] |> ignore
        dotnet.ExecIn "src/Tests" ["restore";] |> ignore
        dotnetTest Commandline.MultiTarget.One

    let RunUnitTests() =
        setLocalEnvVars()
        dotnetTest Commandline.multiTarget

    let RunIntegrationTests() =
        setLocalEnvVars()
        let commaSeparatedEsVersions = getBuildParamOrDefault "esversions" "" 
        let esVersions = 
            match commaSeparatedEsVersions with
            | "" -> failwith "when running integrate you have to pass a comma separated list of elasticsearch versions to test"
            | _ -> commaSeparatedEsVersions.Split ',' |> Array.toList 
        
        for esVersion in esVersions do
            setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
            dotnetTest Commandline.multiTarget |> ignore
