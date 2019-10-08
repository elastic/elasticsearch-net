namespace Scripts

open System
open System.IO

open Paths
open Build
open Commandline
open Bullseye
open ProcNet
open Fake.Core

module Main =

    let private target name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private conditional optional name action = target name (if optional then action else (fun _ -> skip name)) 
    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
    
    /// <summary>Sets command line environments indicating we are building from the command line</summary>
    let setCommandLineEnvVars () =
        Environment.setEnvironVar"NEST_COMMAND_LINE_BUILD" "1"
        
        let sourceDir = Paths.Source("Tests/Tests.Configuration");
        let defaultYaml = Path.Combine(sourceDir, "tests.default.yaml");
        let userYaml = Path.Combine(sourceDir, "tests.yaml");
        let e f = File.Exists f;
        match ((e userYaml), (e defaultYaml)) with
        | (true, _) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(userYaml))
        | (_, true) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(defaultYaml))
        | _ -> failwithf "Expected to find a tests.default.yaml or tests.yaml in %s" sourceDir
        
          
    let [<EntryPoint>] main args = 
        
        setCommandLineEnvVars ()
        
        let parsed = Commandline.parse (args |> Array.toList)
        
        let buildVersions = Versioning.BuildVersioning parsed
        let artifactsVersion = Versioning.ArtifactsVersion buildVersions
        Versioning.Validate parsed.Target buildVersions
        
        Tests.SetTestEnvironmentVariables parsed
        
        target "touch" <| fun _ -> printfn "Touching build %O" artifactsVersion

        target "start" <| fun _ -> 
            match (isMono, parsed.ValidMonoTarget) with
            | (true, false) -> failwithf "%s is not a valid target on mono because it can not call ILRepack" (parsed.Target)
            | _ -> printfn "STARTING BUILD"

        conditional parsed.NeedsClean "clean" Build.Clean 

        conditional parsed.NeedsFullBuild "full-build" <| fun _ -> Build.Compile parsed artifactsVersion

        conditional (not isMono && (Commandline.runningOnCi || parsed.Target = "release")) "internalize-dependencies" <|
            fun _ -> ShadowDependencies.ShadowDependencies artifactsVersion 

        conditional (parsed.GenDocs) "documentation" <| fun _ -> Documentation.Generate parsed

        conditional (not parsed.SkipTests) "test" <| fun _ -> Tests.RunUnitTests parsed |> ignore
        
        target "version" <| fun _ -> printfn "Artifacts Version: %O" artifactsVersion

        target "restore" Restore

        target "inherit-doc" <| InheritDoc.PatchInheritDocs

        target "test-nuget-package" <| fun _ -> 
            // run release unit tests puts packages in the system cache prevent this from happening locally
            if not Commandline.runningOnCi then ignore ()
            else Tests.RunReleaseUnitTests artifactsVersion |> ignore
            
        target "nuget-pack" <| fun _ -> Release.NugetPack artifactsVersion

        conditional (parsed.Target = "canary" && not isMono) "nuget-pack-versioned" <| fun _ -> Release.NugetPackVersioned artifactsVersion

        conditional (parsed.Target <> "canary") "generate-release-notes" <| fun _ -> ReleaseNotes.GenerateNotes buildVersions 

        target "validate-artifacts" <| fun _ -> Versioning.ValidateArtifacts artifactsVersion
        
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

        command "diff" [ "clean"; ] <| fun _ -> Differ.Run parsed
        
        command "rest-spec-tests" [ ] <| fun _ -> ReposTooling.RestSpecTests parsed.RemainingArguments

        command "cluster" [ "restore"; "full-build" ] <| fun _ -> ReposTooling.LaunchCluster parsed
        
        command "codegen" [ ] <| ReposTooling.GenerateApi 

        Targets.RunTargetsAndExit([parsed.Target], fun e -> e.GetType() = typeof<ProcExecException>)

        0

