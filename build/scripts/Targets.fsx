#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
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
open Documentation

// Default target

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"
Target "Quick" <| fun _ -> traceHeader "STARTING INCREMENTAL BUILD"

Target "Clean" Build.Clean

Target "BuildApp" Build.Compile

Target "Test" Tests.RunTest

Target "UnitTests" Tests.RunUnitTests

Target "Forever"  Tests.RunUnitTestsForever
    
Target "Integrate"  Tests.RunIntegrationTests 

Target "Profile" Profiler.Run

Target "Benchmark" Benchmarker.Run

Target "InheritDoc"  InheritDoc.PatchInheritDocs

Target "Documentation" Documentation.Generate

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
  =?> ("UnitTests", (not ((getBuildParam "skiptests") = "1")))
  ==> "InheritDoc"
  ==> "Documentation"
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

"BuildApp"
  ==> "Integrate"

"BuildApp"
  =?> ("Test", (not ((getBuildParam "skiptests") = "1")))
  ==> "Quick"

"BuildApp"
  ==> "Forever"

"Build"
  ==> "Release"

// start build
RunTargetOrDefault "Build"