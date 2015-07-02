// include Fake lib
#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
open Fake 

#load @"Paths.fsx"
#load @"Projects.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Documentation.fsx"
#load @"Releasing.fsx"
open Paths
open Building
open Testing
open Signing
open Versioning
open Documentation
open Releasing

// Default target
Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" <| fun _ -> CleanDir Paths.BuildOutput

Target "BuildApp" <| fun _ -> Build.CompileAll()

Target "Test"  <| fun _ -> Tests.RunAll()

Target "QuickTest"  <| fun _ -> Tests.RunAll()

Target "WatchTests"  <| fun _ -> 
    traceFAKE "Starting quick test (incremental compile then test)"
    use watcher = !! "src/**/*.cs" |> WatchChanges (fun changes -> 
            printfn "%A" changes
            Build.QuickCompile()
            Tests.RunContinuous()
        )
    
    System.Console.ReadLine() |> ignore //Needed to keep FAKE from exiting
    
    watcher.Dispose() // Use to stop the watch from elsewhere, ie another task.

Target "QuickCompile"  <| fun _ -> Build.QuickCompile()

Target "CreateKeysIfAbsent" <| fun _ -> Sign.CreateKeysIfAbsent()

Target "Version" <| fun _ -> Versioning.PatchAssemblyInfos()

Target "Release" <| fun _ -> 
    Release.PackAll()
    Sign.ValidateNugetDllAreSignedCorrectly()

Target "Docs" <| fun _ -> Documentation.Execute "build" 

Target "DocsPreview" <| fun _ -> 
  Documentation.Execute "plugin install livereload" 
  Documentation.Execute "preview" 

Target "Nightly" <| fun _ -> trace "build nightly" 

// Dependencies
"Clean" 
  ==> "CreateKeysIfAbsent"
  =?> ("Version", hasBuildParam "version")
  ==> "BuildApp"
  =?> ("Test", (not (hasBuildParam "skiptests")))
  ==> "Build"

"CreateKeysIfAbsent"
  ==> "Version"
  ==> "Release"
  ==> "Nightly"

"QuickCompile"
  ==> "QuickTest"

"WatchTests"

"Build"
  ==> "Release"

"DocsPreview"
"BuildApp"
"CreateKeysIfAbsent"
"Version"
// start build
RunTargetOrDefault "Build"
