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

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "escluster" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter

    let private dotnetTest() =
        let folder = Paths.IncrementalOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.NetCoreApp1_1
        let testPath = sprintf "%s/Tests.dll" folder
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(10.) }) (sprintf "%s -- Test" testPath) |> ignore

    let private runTestExeOnDesktopCLR() = 
        let folder = Paths.IncrementalOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.Net46
        let testRunner = Tooling.BuildTooling(folder @@ "Tests.exe")
        testRunner.Exec ["Test"; "-parallel"; "-xml"; Paths.Output("TestResults-Desktop-Clr.xml")] |> ignore
        
    let IncrementalTest() = 
        setLocalEnvVars()
        runTestExeOnDesktopCLR() 

    let RunUnitTests() = 
        setLocalEnvVars()
        dotnetTest()
        runTestExeOnDesktopCLR()

    let RunIntegrationTests() =
        setLocalEnvVars()
        let commaSeparatedEsVersions = getBuildParamOrDefault "esversions" "" 
        let esVersions = 
            match commaSeparatedEsVersions with
            | "" -> failwith "when running integrate you have to pass a comma separated list of elasticsearch versions to test"
            | _ -> commaSeparatedEsVersions.Split ',' |> Array.toList 
        
        for esVersion in esVersions do
            setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
            runTestExeOnDesktopCLR()
            //TODO enable integration testing on .net CORE
            //dotnetTest()
