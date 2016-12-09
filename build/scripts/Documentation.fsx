#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"

open System

open Fake 

open Paths
open System.IO

module Documentation = 

    let Generate() =
        let generator = "build/output/v4.6/DocGenerator/DocGenerator.exe"
        ExecProcess (fun p ->
            p.WorkingDirectory <- "src/CodeGeneration/DocGenerator"
            p.FileName <- generator
          ) 
          (TimeSpan.FromMinutes (1.0)) |> ignore

    // TODO: hook documentation validation into the process
    let Validate() =
        let elasticDocsDir = "../elasticsearch-docs"
        if (directoryExists elasticDocsDir = false) then
            let fullPath = combinePaths currentDirectory elasticDocsDir |> Path.GetFullPath
            traceFAKE "No elasticsearch docs repo found at %s. Cannot validate generated documentation" fullPath
        //else
            // Needs to be able to run the build_docs.pl perl script. The best options on Windows for this
            // are Cygwin or Linux Bash for Windows.
            //let docBuildScript = combinePaths elasticDocsDir "build_docs.pl"


        |> ignore


