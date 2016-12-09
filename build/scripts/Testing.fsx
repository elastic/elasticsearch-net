#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"

open System.IO
open Fake 
open Paths
open Projects

module Tests = 
    let private testProjectJson parallelization =
        let p = Paths.Source "Tests/project.json"
        Tooling.DotNet.Exec ["restore"; p; "--verbosity Warning"]
        Tooling.DotNet.Exec ["build"; p; "--configuration Release"; "-f"; "netcoreapp1.0"]
        Tooling.DotNet.Exec ["test"; p; "-parallel"; parallelization; "-xml"; Paths.Output("TestResults-Core-Clr.xml")]

    let private testDesktopClr parallelization = 
        let folder = Paths.ProjectOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.Net46
        let testDll = Path.Combine(folder, "Tests.dll")
        Tooling.XUnit.Exec [testDll; "-parallel"; parallelization; "-xml"; Paths.Output("TestResults-Desktop-Clr.xml")] 
        |> ignore

    let RunUnitTestsForever() = 
        while true do testDesktopClr "all"
        
    let RunUnitTests() = 
        testDesktopClr "all"
        testProjectJson "all"


    let RunIntegrationTests commaSeparatedEsVersions clusterFilter testFilter =
        let esVersions = 
            match commaSeparatedEsVersions with
            | "" -> failwith "when running integrate you have to pass a comma separated list of elasticsearch versions to test"
            | _ -> commaSeparatedEsVersions.Split ',' |> Array.toList 
        
        for esVersion in esVersions do
            setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
            setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
            setProcessEnvironVar "NEST_TEST_FILTER" testFilter
            testDesktopClr "all"