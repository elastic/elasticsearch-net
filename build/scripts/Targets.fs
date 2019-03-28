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
open Bullseye
open Bullseye

module Main =

    let deps x = 
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
          |> ignore

        "Start"
          =?> ("Clean", Commandline.needsClean )
          =?> ("FullBuild", Commandline.needsFullBuild)
          ==> "Benchmark"
          |> ignore

        "Version"
          ==> "Release"
          =?> ("TestNugetPackage", (not isMono && not Commandline.skipTests))
          ==> "Canary"
          |> ignore

        "Start"
          =?> ("Clean", Commandline.needsClean )
          ==> "Restore"
          =?> ("FullBuild", Commandline.needsFullBuild)
          ==> "Integrate"
          |> ignore

        "Build"
          ==> "NugetPack"
          =?> ("NugetPackVersioned", Commandline.target = "canary")
          ==> "ValidateArtifacts"
          =?> ("GenerateReleaseNotes", Commandline.target <> "canary")
          ==> "Release"
          |> ignore
          
        "Touch" |> ignore
        "Start"
          ==> "Clean"
          ==> "Diff"
          |> ignore
          
        "Start"
          ==> "Restore"
          ==> "FullBuild"
          ==> "Cluster"
          |> ignore

        ignore()

    let private t name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private w optional name action = t name (if optional then action else (fun _ -> skip name)) 

    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
          
    let [<EntryPoint>] main args = 

        Commandline.parse()

        t "touch" <| fun _ -> printfn "Touching build"

        t "start" <| fun _ -> 
            match (isMono, Commandline.validMonoTarget) with
            | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (Commandline.target)
            | _ -> traceHeader "STARTING BUILD"

        w Commandline.needsClean "clean" Clean 

        w Commandline.needsFullBuild "full-build" Compile 

        w (hasBuildParam "version") "version" <| fun _ -> 
            tracefn "Current Version: %s" (Versioning.CurrentVersion.ToString())

        w (not isMono) "internalize-dependencies" ShadowDependencies 

        w Commandline.skipDocs "documentation" <| Documentation.Generate

        w (Commandline.skipTests && Commandline.target <> "canary") "test" Tests.RunUnitTests

        t "restore" Restore

        t "inherit-doc" <| InheritDoc.PatchInheritDocs

        t "test-nuget-package" <| fun _ -> 
            //RunReleaseUnitTests restores the canary nugetpackages in tests, since these end up being cached
            //its too evasive to run on development machines or TC, Run only on AppVeyor containers.
            if buildServer <> AppVeyor then Tests.RunUnitTests()
            else Tests.RunReleaseUnitTests()
            
        t "nuget-pack" <| Release.NugetPack

        w (Commandline.target = "canary") "nuget-pack-versioned" <| Release.NugetPackVersioned

        w (Commandline.target <> "canary") "generate-release-notes" <| Release.GenerateNotes

        t "validate-artifacts" <| Versioning.ValidateArtifacts
        


        // the following are expected to be called as targets directly        
        let buildChain = [
            "clean"; "version"; "restore"; "full-build"; "test"; 
            "internalize-dependencies"; "inherit-doc"; "documentation"; 
        ]
        command "build" buildChain <| fun _ -> traceHeader "STARTING BUILD"

        command "benchmark" [ "clean"; "full-build"; ] Benchmarker.Run

        command "canary" [ "version"; "release"; "test-nuget-package";] (fun _ -> tracefn "Finished Release Build %O" Versioning.CurrentVersion)

        command "integrate" [ "clean"; "restore"; "full-build";] Tests.RunIntegrationTests

        command "release" [ 
           "build"; "nuget-pack"; "nuget-pack-versioned"; "validate-artifacts"; "generate-release-notes"
        ] (fun _ -> traceHeader (sprintf "Finished Release Build %O" Versioning.CurrentVersion))

        command "diff" [ "clean"; ] <| fun _ ->
          let differ = Paths.PaketDotNetGlobalTool "assembly-differ" @"tools\netcoreapp2.1\any\assembly-differ.dll"
          let args = Commandline.arguments |> List.skip 1 |> String.concat " "
          let command = sprintf @"%s %s" differ args
          setProcessEnvironVar "NUGET" Tooling.nugetFile
          DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) command 

        command "cluster" [ "restore"; "full-build" ] <| fun _ -> 
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

        Targets.RunTargetsAndExit([Commandline.target])

        0

