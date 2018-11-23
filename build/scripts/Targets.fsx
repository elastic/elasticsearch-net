#load @"Commandline.fsx"
#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
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
open System.IO

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
open Commandline
open Differ
open Differ.Differ
open Fake.IO
open Octokit

Commandline.parse()

Target "Touch" <| fun _ -> traceHeader "Touching build"
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"
Target "Start" <| fun _ -> 
    match (isMono, Commandline.validMonoTarget) with
    | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (Commandline.target)
    | _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "FullBuild" <| fun _ -> Build.Compile Commandline.needsFullBuild
    
Target "Test" Tests.RunUnitTests

Target "Profile" <| fun _ -> 
    Profiler.Run()
    let url = getBuildParam "elasticsearch"
    Profiler.IndexResults url

Target "Integrate" Tests.RunIntegrationTests

Target "Benchmark" Benchmarker.Run

Target "InternalizeDependencies" Build.ILRepack

Target "InheritDoc" InheritDoc.PatchInheritDocs

Target "Documentation" Documentation.Generate

Target "Version" <| fun _ -> 
    tracefn "Current Version: %s" (Versioning.CurrentVersion.ToString())

Target "TestNugetPackage" <| fun _ -> 
    //RunReleaseUnitTests restores the canary nugetpackages in tests, since these end up being cached
    //its too evasive to run on development machines or TC, Run only on AppVeyor containers.
    if buildServer <> AppVeyor then Tests.RunUnitTests()
    else Tests.RunReleaseUnitTests()
    
Target "Canary" <| fun _ -> tracefn "Finished Release Build %O" Versioning.CurrentVersion
    
Target "Diff" <| fun _ ->
    let diffType = getBuildParam "diffType"
    let project = getBuildParam "project"
    let first = getBuildParam "first"
    let second = getBuildParam "second"
    let format = getBuildParam "format"
    tracefn "Performing %s diff %s using %s with %s and %s" format project diffType first second
    Differ.Generate(diffType, project, first, second, format)

Target "Cluster" <| fun _ -> 
    let clusterName = getBuildParam "clusterName"
    let clusterVersion = getBuildParam "clusterVersion"
    let testsProjectDirectory = Path.Combine(Path.GetFullPath(Paths.Output("Tests.ClusterLauncher")), "netcoreapp2.1")
    let tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    
    let sourceDir = Paths.Source("Tests/Tests.Configuration");
    let defaultYaml = Path.Combine(sourceDir, "tests.default.yaml");
    let userYaml = Path.Combine(sourceDir, "tests.yaml");
    let e f = File.Exists f;
    match ((e userYaml), (e defaultYaml)) with
    | (true, _) -> setProcessEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(userYaml))
    | (_, true) -> setProcessEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(defaultYaml))
    | _ -> ignore()
    
    Shell.copyDir tempDir testsProjectDirectory (fun s -> true)
    let command = sprintf "%s %s" clusterName clusterVersion
    DotNetCli.RunCommand(fun p ->
        { p with
            WorkingDir = tempDir;
            TimeOut = TimeSpan.FromMinutes(120.)
        }) (sprintf "%s %s" (Path.Combine(tempDir, "Tests.ClusterLauncher.dll")) command)
    
    Shell.deleteDir tempDir

Target "Release" <| fun _ -> traceHeader (sprintf "Finished Release Build %O" Versioning.CurrentVersion)

Target "NugetPack" Release.NugetPack

Target "NugetPackVersioned" Release.NugetPackVersioned

Target "ValidateArtifacts" Versioning.ValidateArtifacts

Target "GenerateReleaseNotes" Release.GenerateNotes


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
  ==> "NugetPack"
  =?> ("NugetPackVersioned", Commandline.target = "canary")
  ==> "ValidateArtifacts"
  =?> ("GenerateReleaseNotes", Commandline.target <> "canary")
  ==> "Release"
  
"Touch"
"Start"
  ==> "Clean"
  ==> "Diff"
  
"Start"
  ==> "Restore"
  ==> "FullBuild"
  ==> "Cluster"
  
RunTargetOrListTargets()

