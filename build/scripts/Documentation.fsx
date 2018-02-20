#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"

open System
open System.IO

open Fake 

open Paths
open Projects

module Documentation = 

    let Generate() = 
        let docGenerator = PrivateProject(DocGenerator)
        let path = Paths.ProjectOutputFolder docGenerator DotNetFramework.Net46
        let generator = sprintf "%s/%s.exe" path docGenerator.Name
        ExecProcess (fun p ->
            p.WorkingDirectory <- Paths.Source("CodeGeneration") @@ docGenerator.Name
            p.FileName <- generator
        ) (TimeSpan.FromMinutes 3.) |> ignore

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


