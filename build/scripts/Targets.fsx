#load @"Commandline.fsx"
#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Documentation.fsx"
#load @"Releasing.fsx"
#load @"Benchmarking.fsx"
#load @"Profiling.fsx"
#load @"XmlDocPatcher.fsx"

open System
open Fake

open Paths
open Building
open Testing
open Versioning
open Documentation
open Releasing
open Profiling
open Benchmarking
open XmlDocPatcher
open Documentation
open Signing

open Commandline
Commandline.parse()

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "IncrementalBuild" <| fun _ -> Build.Compile false

Target "FullBuild" <| fun _ -> Build.Compile false
    
Target "Test" Tests.RunUnitTests

Target "Profile" <| fun _ -> 
    Profiler.Run()
    let url = getBuildParam "elasticsearch"
    Profiler.IndexResults url

Target "Integrate" <| Tests.RunIntegrationTests

Target "Benchmark" Benchmarker.Run

Target "InheritDoc"  InheritDoc.PatchInheritDocs

Target "Documentation" Documentation.Generate

Target "Version" <| fun _ -> 
    tracefn "Current Version: %s" (Versioning.CurrentVersion.ToString())

Target "Release" <| fun _ -> 
    Release.NugetPack()   
    Versioning.ValidateArtifacts()
    StrongName.ValidateDllsInNugetPackage()

Target "Canary" <| fun _ -> 
    trace "Running canary build" 
    let apiKey = (getBuildParam "apikey");
    let feed = (getBuildParamOrDefault "feed" "elasticsearch-net");
    if (not (String.IsNullOrWhiteSpace apiKey) || apiKey = "ignore") then Release.PublishCanaryBuild apiKey feed

// Dependencies
"Clean" 
  =?> ("Version", hasBuildParam "version")
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  =?> ("Test", (not Commandline.skipTests))
  ==> "InheritDoc"
  ==> "Documentation"
  ==> "Build"

"Clean"
  ==> "FullBuild" 
  ==> "Profile"

"Clean" 
  ==> "FullBuild"
  ==> "Benchmark"

"Version"
  ==> "Release" ==> "Canary"

"FullBuild"
  ==> "Integrate"

"Build"
  ==> "Release"

// start build
RunTargetOrDefault Commandline.target