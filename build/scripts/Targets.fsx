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
#load @"Differ.fsx"
#nowarn "0044" //TODO sort out FAKE 5

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
open Differ
open Differ.Differ

Commandline.parse()

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "FullBuild" <| fun _ -> Build.Compile false
    
Target "Test" Tests.RunUnitTests

Target "Profile" <| fun _ -> 
    Profiler.Run()
    let url = getBuildParam "elasticsearch"
    Profiler.IndexResults url

Target "Integrate" <| Tests.RunIntegrationTests

Target "Benchmark" <| fun _ ->
    let runInteractive = ((getBuildParam "nonInteractive") <> "1")
    Benchmarker.Run(runInteractive)
    let url = getBuildParam "elasticsearch"
    let username = getBuildParam "username"
    let password = getBuildParam "password"
    Benchmarker.IndexResults (url, username, password)

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

Target "Diff" <| fun _ ->
    let diffType = getBuildParam "diffType"
    let project = getBuildParam "project"
    let first = getBuildParam "first"
    let second = getBuildParam "second"
    let format = getBuildParam "format"
    tracefn "Performing %s diff %s using %s with %s and %s" format project diffType first second
    Differ.Generate(diffType, project, first, second, format)

// Dependencies
"Clean" 
  =?> ("Version", hasBuildParam "version")
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  =?> ("Test", (not Commandline.skipTests))
  ==> "InheritDoc"
  =?> ("Documentation", (not Commandline.skipDocs))
  ==> "Build"

"Clean"
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Profile"

"Clean" 
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Benchmark"

"Version"
  ==> "Release"
  ==> "Canary"

"Clean"
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Integrate"

"Build"
  ==> "Release"
  
"Clean"
  ==> "Diff"
  
RunTargetOrListTargets()

