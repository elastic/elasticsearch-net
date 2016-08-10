#load @"Paths.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Documentation.fsx"
#load @"Releasing.fsx"
#load @"Profiling.fsx"

open System

open Fake 

open Building
open Testing
open Signing
open Versioning
open Releasing
open Profiling

// Default target
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" <| fun _ -> Build.Clean()

Target "BuildApp" <| fun _ -> Build.Compile()

Target "Test"  <| fun _ -> Tests.RunUnitTests()
    
Target "QuickTest"  <| fun _ -> Tests.RunUnitTests()

Target "Integrate"  <| fun _ -> Tests.RunIntegrationTests() (getBuildParamOrDefault "esversions" "")

Target "Profile" <| fun _ -> Profiler.Run()

Target "Benchmark" <| fun _ -> Benchmarker.Run()

Target "QuickCompile"  <| fun _ -> Build.QuickCompile()

Target "Version" <| fun _ -> 
    Versioning.PatchAssemblyInfos()
    Versioning.PatchProjectJsons()

Target "Release" <| fun _ -> 
    Release.NugetPack()   
    Sign.ValidateNugetDllAreSignedCorrectly()
    Versioning.ValidateArtifacts()

Target "Canary" <| fun _ -> 
    trace "Running canary build" 
    let apiKey = (getBuildParam "apikey");
    let feed = (getBuildParamOrDefault "feed" "elasticsearch-net");
    if (not (String.IsNullOrWhiteSpace apiKey) || apiKey = "ignore") then Release.PublishCanaryBuild apiKey feed

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

"Build"
  ==> "Release"

// start build
RunTargetOrDefault "Build"