#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling

module Tests = 
    let private testProjectJson parallelization =
        DotNetProject.All
        |> Seq.iter(fun p -> 
            let path = Paths.ProjectJson p.Name
            DotNet.Exec ["restore"; path; "--verbosity Warning"]
        )

        let testPath = Paths.Source "Tests/project.json"
        DotNet.Exec ["restore"; testPath; "--verbosity Warning"]
        DotNet.Exec ["build"; testPath; "--configuration Release"; "-f"; "netcoreapp1.0"]
        DotNet.Exec ["test"; testPath; "-parallel"; parallelization; "-xml"; Paths.Output("TestResults-Core-Clr.xml")]

    let private testDesktopClr parallelization = 
        let folder = Paths.ProjectOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.Net46
        let testDll = Path.Combine(folder, "Tests.dll")
        XUnit.Exec [testDll; "-parallel"; parallelization; "-xml"; Paths.Output("TestResults-Desktop-Clr.xml")] 
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