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

Target "BuildApp" <| fun _ -> Build.CompileDnx()

Target "Test"  <| fun _ -> Tests.RunDnx()
    
Target "QuickTest"  <| fun _ -> Tests.RunDnx()

Target "Integrate"  <| fun _ -> Tests.RunDnxIntegration (getBuildParamOrDefault "esversions" "")

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

Target "Benchmark" <| fun _ -> Benchmarker.RunDnx()

Target "QuickCompile"  <| fun _ -> Build.CompileDnx()

Target "CreateKeysIfAbsent" <| fun _ -> Sign.CreateKeysIfAbsent()

Target "Version" <| fun _ -> Versioning.PatchAssemblyInfos()

Target "Release" <| fun _ -> 
    Release.PackAllDnx()
    Sign.ValidateNugetDllAreSignedCorrectly()

Target "Nightly" <| fun _ -> trace "build nightly" 

BuildFailureTarget "NotifyTestFailures" <| fun _ -> Tests.Notify() |> ignore

Target "Use" <| fun _ -> Tooling.Dnvm.Exec ["use " + (getBuildParamOrDefault "dnxversion" "default")] |> ignore

// Dependencies
"Clean" 
  ==> "CreateKeysIfAbsent"
  =?> ("Version", hasBuildParam "version")
  ==> "BuildApp"
  =?> ("Test", (not (hasBuildParam "skiptests")))
  ==> "Build"

"Clean" 
  ==> "BuildApp"
  ==> "Profile"

"Clean" 
  ==> "BuildApp"
  ==> "Benchmark"

"CreateKeysIfAbsent"
  ==> "Version"
  ==> "Release"
  ==> "Nightly"

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
