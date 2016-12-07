#load @"Paths.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Documentation.fsx"
#load @"Releasing.fsx"
#load @"Profiling.fsx"
#load @"XmlDocPatcher.fsx"

open System

open Fake 

open Building
open Testing
open Signing
open Versioning
open Releasing
open Profiling
open XmlDocPatcher

// Default target
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" <| fun _ -> Build.Clean()

Target "BuildApp" <| fun _ -> Build.Compile()

Target "Test"  <| fun _ -> Tests.RunUnitTests()

Target "InheritDoc"  <| fun _ -> InheritDoc.patchInheritDocs()

Target "TestForever"  <| fun _ -> Tests.RunUnitTestsForever()

Target "Integrate"  <| fun _ -> Tests.RunIntegrationTests (getBuildParamOrDefault "esversions" "") (getBuildParamOrDefault "escluster" "") (getBuildParamOrDefault "testfilter" "")

Target "Profile" <| fun _ -> Profiler.Run()

Target "Benchmark" <| fun _ -> Benchmarker.Run()

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
  ==> "InheritDoc"
  ==> "Build"

"Clean" 
  ==> "BuildApp"
  ==> "TestForever"

"Clean" 
  ==> "BuildApp"
  ==> "Profile"

"Clean" 
  ==> "BuildApp"
  ==> "Benchmark"

"Version"
  ==> "Release"
  ==> "Canary"

"BuildApp"
  ==> "Integrate"

"Build"
  ==> "Release"

// start build
RunTargetOrDefault "Build"