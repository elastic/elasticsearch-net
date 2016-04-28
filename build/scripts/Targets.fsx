// include Fake lib
#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake 

#load @"Paths.fsx"
#load @"Projects.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Documentation.fsx"
#load @"Releasing.fsx"
#load @"Profiling.fsx"

open Paths
open Building
open Testing
open Signing
open Versioning
open Documentation
open Releasing
open Profiling
open System
open System.IO

let private buildFailed errors =
    raise (BuildException("The project build failed.", errors |> List.ofSeq))
    
let private testsFailed errors =
    raise (BuildException("The project tests failed.", errors |> List.ofSeq))

// Default target
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" <| fun _ -> CleanDir Paths.BuildOutput

Target "BuildApp" <| fun _ -> Build.Compile()

Target "Test"  <| fun _ -> Tests.RunUnitTests()
    
Target "QuickTest"  <| fun _ -> Tests.RunUnitTests()

Target "Integrate"  <| fun _ -> Tests.RunIntegrationTests() (getBuildParamOrDefault "esversions" "")

Target "WatchTests"  <| fun _ -> 
    traceFAKE "Starting quick test (incremental compile then test)"
    use watcher = (!! "src/Tests/**/*.cs").And("src/Tests/**/*.md") |> WatchChanges (fun changes -> 
            printfn "%A" changes
            Build.QuickCompile()
            //Documentation.RunLitterateur()
            Tests.RunContinuous()
        )
    
    System.Console.ReadLine() |> ignore 
    watcher.Dispose() 

Target "Profile" <| fun _ -> Profiler.Run()

Target "Benchmark" <| fun _ -> Benchmarker.Run()

Target "QuickCompile"  <| fun _ -> Build.QuickCompile()

Target "Version" <| fun _ -> 
    Versioning.PatchAssemblyInfos()
    Versioning.PatchProjectJsons()

Target "Release" <| fun _ -> 
    Release.PatchReleaseNotes()
    Release.PackAllDnx()   
    Sign.ValidateNugetDllAreSignedCorrectly()
    Versioning.ValidateArtifacts()

Target "Canary" <| fun _ -> 
    trace "Running canary build" 
    let apiKey = (getBuildParam "apikey");
    let feed = (getBuildParamOrDefault "feed" "elasticsearch-net");
    if (not (String.IsNullOrWhiteSpace apiKey) || apiKey = "ignore") then Release.PublishCanaryBuild apiKey feed

BuildFailureTarget "NotifyTestFailures" <| fun _ -> Tests.Notify() |> ignore


// Dependencies
"Clean" 
  =?> ("Version", hasBuildParam "version")
  ==> "BuildApp"
  =?> ("Test", (not ((getBuildParam "skiptests") = "1")))
  ==> "Build"

"Clean" 
  ==> "BuildApp"
  ==> "Profile"

"Clean" 
  ==> "BuildApp"
  ==> "Benchmark"

"Version"
  ==> "Release"
  ==> "Canary"

"QuickCompile"
  ==> "QuickTest"

"QuickCompile"
  ==> "Integrate"

"WatchTests"

"Build"
  ==> "Release"

"BuildApp"
"CreateKeysIfAbsent"
"Version"

// start build
RunTargetOrDefault "Build"