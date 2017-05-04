#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling

module Tests = 
    open System.Threading
    open System

    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS"

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "escluster" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter

    type MultiTarget = All | One  
    let private dotnetTest target =
        CreateDir Paths.BuildOutput
        let command = 
            let p = ["xunit"; "-parallel"; "all"; "-xml"; "../.." @@ Paths.Output("TestResults-Desktop-Clr.xml")] 
            match (target, buildingOnTravis) with 
            | (_, true) 
            | (One, _) -> ["-framework"; "netcoreapp1.1"] |> List.append p 
            | _  -> p

        let dotnet = Tooling.BuildTooling("dotnet")
        dotnet.ExecIn "src/Tests" command |> ignore

    let IncrementalTest() = 
        setLocalEnvVars()
        dotnetTest One 

    let RunUnitTests() = 
        setLocalEnvVars()
        dotnetTest All

    let RunIntegrationTests target =
        setLocalEnvVars()
        let commaSeparatedEsVersions = getBuildParamOrDefault "esversions" "" 
        let esVersions = 
            match commaSeparatedEsVersions with
            | "" -> failwith "when running integrate you have to pass a comma separated list of elasticsearch versions to test"
            | _ -> commaSeparatedEsVersions.Split ',' |> Array.toList 
        
        for esVersion in esVersions do
            setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
            dotnetTest target |> ignore
