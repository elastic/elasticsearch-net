namespace Scripts

open System.IO

open Paths
open Projects
open Commandline

module Documentation = 

    let Generate args = 
        let docGenerator = PrivateProject(DocGenerator)
        let path = Paths.ProjectOutputFolder docGenerator DotNetFramework.NetCoreApp2_1
        let generator = sprintf "%s.dll" docGenerator.Name 
        
        Tooling.DotNet.ExecIn path [generator; args.DocsBranch] |> ignore

    // TODO: hook documentation validation into the process
    let Validate() = 
        let elasticDocsDir = "../elasticsearch-docs"
        if (Directory.Exists elasticDocsDir = false) then
            let fullPath = Path.Combine(Directory.GetCurrentDirectory(), elasticDocsDir) |> Path.GetFullPath
            printfn "No elasticsearch docs repo found at %s. Cannot validate generated documentation" fullPath
        //else
            // Needs to be able to run the build_docs.pl perl script. The best options on Windows for this
            // are Cygwin or Linux Bash for Windows.
            //let docBuildScript = combinePaths elasticDocsDir "build_docs.pl"


        |> ignore


