namespace Scripts

open System
open System.IO

open Paths
open Build
open Tests
open Versioning
open Documentation
open Release
open ReleaseNotes
open Benchmarker
open InheritDoc
open Commandline
open Tooling
open Bullseye
open Bullseye
open Fake.Core
open Fake.IO

module Main =

    let private t name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private w optional name action = t name (if optional then action else (fun _ -> skip name)) 
    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
          
    let [<EntryPoint>] main args = 

        let parsed = Commandline.parse (args |> Array.toList)
        
        let buildVersions = Versioning.BuildVersioning parsed
        let artifactsVersion = Versioning.ArtifactsVersion buildVersions
        Versioning.Validate parsed.Target buildVersions
        
        Tests.SetTestEnvironmentVariables parsed
        

        t "touch" <| fun _ -> printfn "Touching build %O" artifactsVersion

        t "start" <| fun _ -> 
            match (isMono, parsed.ValidMonoTarget) with
            | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (parsed.Target)
            | _ -> printfn "STARTING BUILD"

        w parsed.NeedsClean "clean" Clean 

        w parsed.NeedsFullBuild "full-build" <| fun _ -> Compile parsed artifactsVersion

        w (not isMono) "internalize-dependencies" <| fun _ -> ShadowDependencies.ShadowDependencies artifactsVersion 

        w parsed.SkipDocs "documentation" <| fun _ -> Documentation.Generate parsed

        w (not parsed.SkipTests && parsed.Target <> "canary") "test" <| fun _ -> Tests.RunUnitTests parsed
        
        t "version" <| fun _ -> printfn "Artifacts Version: %O" artifactsVersion

        t "restore" Restore

        t "inherit-doc" <| InheritDoc.PatchInheritDocs

        t "test-nuget-package" <| fun _ -> 
            //run release unit tests puts packages in the system cache prevent this from happening locally
            if not Commandline.runningOnCi then Tests.RunUnitTests parsed
            else Tests.RunReleaseUnitTests artifactsVersion
            
        t "nuget-pack" <| fun _ -> Release.NugetPack artifactsVersion

        w (parsed.Target = "canary") "nuget-pack-versioned" <| fun _ -> Release.NugetPackVersioned artifactsVersion

        w (parsed.Target <> "canary") "generate-release-notes" <| fun _ -> ReleaseNotes.GenerateNotes buildVersions 

        t "validate-artifacts" <| fun _ -> Versioning.ValidateArtifacts artifactsVersion
        
        // the following are expected to be called as targets directly        
        let buildChain = [
            "clean"; "version"; "restore"; "full-build"; "test"; 
            "internalize-dependencies"; "inherit-doc"; "documentation"; 
        ]
        command "build" buildChain <| fun _ -> printfn "STARTING BUILD"

        command "benchmark" [ "clean"; "full-build"; ] <| fun _ -> Benchmarker.Run parsed

        command "canary" [ "version"; "release"; "test-nuget-package";] <| fun _ -> printfn "Finished Release Build %O" artifactsVersion

        command "integrate" [ "clean"; "restore"; "full-build";] <| fun _ -> Tests.RunIntegrationTests parsed

        command "release" [ 
           "build"; "nuget-pack"; "nuget-pack-versioned"; "validate-artifacts"; "generate-release-notes"
        ] (fun _ -> printfn "Finished Release Build %O" artifactsVersion)

        command "diff" [ "clean"; ] <| fun _ ->
          let differ = Paths.PaketDotNetGlobalTool "assembly-differ" @"tools\netcoreapp2.1\any\assembly-differ.dll"
          let args = parsed.RemainingArguments |> String.concat " "
          let command = sprintf @"%s %s" differ args
          Environment.setEnvironVar "NUGET" Tooling.nugetFile
          Tooling.DotNet.Exec [command] |> ignore

        command "cluster" [ "restore"; "full-build" ] <| fun _ -> 
            let clusterName = Option.defaultValue "" <| match parsed.CommandArguments with | Cluster c -> Some c.Name | _ -> None
            let clusterVersion = Option.defaultValue "" <|match parsed.CommandArguments with | Cluster c -> c.Version | _ -> None
            
            let testsProjectDirectory = Path.Combine(Path.GetFullPath(Paths.Output("Tests.ClusterLauncher")), "netcoreapp2.1")
            let tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            
            let sourceDir = Paths.Source("Tests/Tests.Configuration");
            let defaultYaml = Path.Combine(sourceDir, "tests.default.yaml");
            let userYaml = Path.Combine(sourceDir, "tests.yaml");
            let e f = File.Exists f;
            match ((e userYaml), (e defaultYaml)) with
            | (true, _) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(userYaml))
            | (_, true) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(defaultYaml))
            | _ -> ignore()
            
            Shell.copyDir tempDir testsProjectDirectory (fun s -> true)
            let command = sprintf "%s %s" clusterName clusterVersion
            let timeout = TimeSpan.FromMinutes(120.)
            let dll = Path.Combine(tempDir, "Tests.ClusterLauncher.dll");
            Tooling.DotNet.ExecInWithTimeout tempDir ["run"; dll; command] timeout  |> ignore
            
            Shell.deleteDir tempDir

        Targets.RunTargetsAndExit([parsed.Target])

        0

