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

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "escluster" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter

    let private testProjectJson() =
        DotNetProject.All
        |> Seq.iter(fun p -> 
            let path = Paths.ProjectJson p.Name
            Tooling.DotNet.Exec ["restore"; path]
        )

        let testPath = Paths.Source "Tests/project.json"
        Tooling.DotNet.Exec ["restore"; testPath]
        Tooling.DotNet.Exec ["build"; testPath; "--configuration Release"; "-f"; "netcoreapp1.0"]
        Tooling.DotNet.Exec ["test"; testPath; "-parallel"; "all"; "-xml"; Paths.Output("TestResults-Core-Clr.xml")]

    let private testDesktopClr() = 
        let folder = Paths.ProjectOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.Net46
        let testDll = Path.Combine(folder, "Tests.dll")
        Tooling.XUnit.Exec [testDll; "-parallel"; "all"; "-xml"; Paths.Output("TestResults-Desktop-Clr.xml")] 
        |> ignore
        
    let RunTest() = 
        setLocalEnvVars()
        let folder = Paths.IncrementalOutputFolder (PrivateProject PrivateProject.Tests) DotNetFramework.Net45
        let testDll = Path.Combine(folder, "Tests.dll")
        Tooling.XUnit.Exec [testDll; "-parallel"; "all"; "-xml"; Paths.Output("TestResults-Desktop-Clr.xml")] |> ignore

    let RunUnitTestsForever() = 
        while true do 
            RunTest() 
            Thread.Sleep(1000)

    let RunUnitTests() = 
        setLocalEnvVars()
        testDesktopClr()
        testProjectJson()

    let RunIntegrationTests() =
        setLocalEnvVars()
        let commaSeparatedEsVersions = getBuildParamOrDefault "esversions" "" 
        let esVersions = 
            match commaSeparatedEsVersions with
            | "" -> failwith "when running integrate you have to pass a comma separated list of elasticsearch versions to test"
            | _ -> commaSeparatedEsVersions.Split ',' |> Array.toList 
        
        for esVersion in esVersions do
            setProcessEnvironVar "NEST_INTEGRATION_VERSION" esVersion
            testDesktopClr()
