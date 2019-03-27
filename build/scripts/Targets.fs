namespace Scripts

open System
open Fake
open System.IO

open Paths
open Build
open Test
open Versioning
open Documentation
open Release
open Benchmarker
open InheritDoc
open Commandline
open Fake.IO
open Tooling

module Main =

    let [<EntryPoint>] main args = 
    
        printfn "Arguments passed to function : %A" args

        Commandline.parse()

        Target "Touch" <| fun _ -> traceHeader "Touching build"
        Target "Build" <| fun _ -> traceHeader "STARTING BUILD"
        Target "Start" <| fun _ -> 
            match (isMono, Commandline.validMonoTarget) with
            | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (Commandline.target)
            | _ -> traceHeader "STARTING BUILD"

        Target "Clean" Clean

        Target "Restore" Restore

        Target "FullBuild" <| fun _ -> Compile Commandline.needsFullBuild
            
        Target "Test" Tests.RunUnitTests

        Target "Integrate" Tests.RunIntegrationTests

        Target "Benchmark" Benchmarker.Run

        Target "InternalizeDependencies" <| fun _ -> ILRepack |> ignore

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
            let differ = Paths.PaketDotNetGlobalTool "assembly-differ" @"tools\netcoreapp2.1\any\assembly-differ.dll"
            let args = Commandline.arguments |> List.skip 1 |> String.concat " "
            let command = sprintf @"%s %s" differ args
            setProcessEnvironVar "NUGET" Tooling.nugetFile
            DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) command |> ignore

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
          
        0

