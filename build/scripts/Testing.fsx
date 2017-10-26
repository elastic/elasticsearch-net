#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Commandline.fsx"
#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling
open Commandline

module Tests =
    open System

    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS"

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "clusterfilter" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        let numberOfConnections = getBuildParamOrDefault "numberOfConnections" ""
        let forceSource = if Commandline.forceSourceSerialization then "true" else "false";
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter
        setProcessEnvironVar "NEST_NUMBER_OF_CONNECTIONS" numberOfConnections
        setProcessEnvironVar "NEST_TEST_SEED" Commandline.seed
        setProcessEnvironVar "NEST_SOURCE_SERIALIZER" forceSource

    let private dotnetTest (target: Commandline.MultiTarget) =
        CreateDir Paths.BuildOutput
        let command = 
            let p = ["xunit"; "-parallel"; "all"; "-xml"; "../.." @@ Paths.Output("TestResults-Desktop-Clr.xml")] 
            match (target, buildingOnTravis) with 
            | (_, true) 
            | (Commandline.MultiTarget.One, _) -> ["-framework"; "netcoreapp1.1"] |> List.append p
            | _  -> p

        let dotnet = Tooling.BuildTooling("dotnet")
        dotnet.ExecIn "src/Tests" command |> ignore

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
