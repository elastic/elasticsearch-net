namespace Scripts

open System

open Build
open Commandline
open Bullseye

module Main =

    let private target name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private conditional optional name action = target name (if optional then action else (fun _ -> skip name)) 
    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
          
    let [<EntryPoint>] main args = 

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

        conditional (not isMono) "internalize-dependencies" <| fun _ -> ShadowDependencies.ShadowDependencies artifactsVersion 

        conditional parsed.SkipDocs "documentation" <| fun _ -> Documentation.Generate parsed

        conditional (not parsed.SkipTests && parsed.Target <> "canary") "test" <| fun _ -> Tests.RunUnitTests parsed
        
        target "version" <| fun _ -> printfn "Artifacts Version: %O" artifactsVersion

        target "restore" Restore

        target "inherit-doc" <| InheritDoc.PatchInheritDocs

        target "test-nuget-package" <| fun _ -> 
            //run release unit tests puts packages in the system cache prevent this from happening locally
            if not Commandline.runningOnCi then Tests.RunUnitTests parsed
            else Tests.RunReleaseUnitTests artifactsVersion
            
        target "nuget-pack" <| fun _ -> Release.NugetPack artifactsVersion

        conditional (parsed.Target = "canary") "nuget-pack-versioned" <| fun _ -> Release.NugetPackVersioned artifactsVersion

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

        command "cluster" [ "restore"; "full-build" ] <| fun _ -> Cluster.Run parsed

        Targets.RunTargetsAndExit([parsed.Target])

        0

