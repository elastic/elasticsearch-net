// include Fake lib
#I @"../../packages/build/FAKE/tools"
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

Target "Test"  <| fun _ -> Tests.RunAllUnitTests()

Target "QuickTest"  <| fun _ -> Tests.RunAllUnitTests()

Target "Integrate"  <| fun _ -> Tests.RunAllIntegrationTests(getBuildParamOrDefault "esversions" "")

Target "WatchTests"  <| fun _ -> 
    traceFAKE "Starting quick test (incremental compile then test)"
    use watcher = (!! "src/Tests/**/*.cs").And("src/Tests/**/*.md") |> WatchChanges (fun changes -> 
            printfn "%A" changes
            Build.QuickCompile()
            //Documentation.RunLitterateur()
            Tests.RunContinuous()
        )
    
    System.Console.ReadLine() |> ignore 
    watcher.Dispose() 

Target "QuickCompile"  <| fun _ -> Build.QuickCompile()

Target "CreateKeysIfAbsent" <| fun _ -> Sign.CreateKeysIfAbsent()

Target "Version" <| fun _ -> Versioning.PatchAssemblyInfos()

Target "Release" <| fun _ -> 
    Release.PackAll()
    Sign.ValidateNugetDllAreSignedCorrectly()

Target "Nightly" <| fun _ -> trace "build nightly" 

BuildFailureTarget "NotifyTestFailures" <| fun _ -> Tests.Notify() |> ignore


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

"BuildApp"
"CreateKeysIfAbsent"
"Version"
// start build
RunTargetOrDefault "Build"
