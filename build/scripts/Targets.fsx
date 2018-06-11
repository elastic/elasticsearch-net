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

Target "Touch" <| fun _ -> traceHeader "Touching build"
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"
Target "Start" <| fun _ -> 
    match (isMono, Commandline.validMonoTarget) with
    | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (Commandline.target)
    | _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "FullBuild" <| fun _ -> Build.Compile false
    
Target "Test" Tests.RunUnitTests

Target "Profile" <| fun _ -> 
    Profiler.Run()
    let url = getBuildParam "elasticsearch"
    Profiler.IndexResults url

Target "Integrate" Tests.RunIntegrationTests

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
    Release.GenerateNotes()

Target "TestNugetPackage" <| fun _ -> 
    //RunReleaseUnitTests restores the canary nugetpackages in tests, since these end up being cached
    //its too evasive to run on development machines or TC, Run only on AppVeyor containers.
    if buildServer <> AppVeyor then Tests.RunUnitTests()
    else Tests.RunReleaseUnitTests()
    
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
"Start"
  =?> ("Clean", Commandline.needsClean )
  =?> ("Version", hasBuildParam "version")
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  =?> ("Test", (not Commandline.skipTests && Commandline.target <> "canary"))
  =?> ("InternalizeDependencies", (not isMono))
  ==> "InheritDoc"
  =?> ("Documentation", (not Commandline.skipDocs))
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
  =?> ("TestNugetPackage", (not isMono && not Commandline.skipTests))
  ==> "Canary"

"Start"
  =?> ("Clean", Commandline.needsClean )
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  ==> "Integrate"

"Build"
  ==> "Release"
  
"Touch"
"Start"
  ==> "Clean"
  ==> "Diff"
  
RunTargetOrListTargets()

