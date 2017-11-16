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

Commandline.parse()

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"
Target "Start" <| fun _ -> traceHeader "STARTING BUILD"

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

Target "InternalizeDependencies" Build.ILRepack

Target "InheritDoc" InheritDoc.PatchInheritDocs

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
"Start"
  =?> ("Clean", Commandline.needsClean )
  =?> ("Version", hasBuildParam "version")
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  =?> ("Test", (not Commandline.skipTests))
  ==> "InternalizeDependencies"
  ==> "InheritDoc"
  ==> "Documentation"
  ==> "Build"

"Start"
  =?> ("Clean", Commandline.needsClean )
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Profile"

"Start"
  =?> ("Clean", Commandline.needsClean )
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Benchmark"

"Version"
  ==> "Release"
  ==> "Canary"

"Start"
  =?> ("Clean", Commandline.needsClean )
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Integrate"

"Build"
  ==> "Release"

RunTargetOrListTargets()

