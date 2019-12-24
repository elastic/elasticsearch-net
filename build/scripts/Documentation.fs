namespace Scripts

open System.IO

open Projects
open Commandline
open Fake.Core

module Documentation = 

    let Generate args = 
        let docGenerator = PrivateProject(DocGenerator)
        let path = Paths.ProjectOutputFolder docGenerator DotNetFramework.NetCoreApp3_0
        let generator = sprintf "%s.dll" docGenerator.Name
        
        printfn "==> %s" path
        
        let (|NotNullOrEmpty|_|) (candidate:string) =
            if String.isNotNullOrEmpty candidate then Some candidate
            else None
        
        let dotnetArgs =     
            match (args.DocsBranch, args.ReferenceBranch) with
            | (NotNullOrEmpty b, NotNullOrEmpty d) -> [ generator; "-b"; b; "-d"; d ];
            | (NotNullOrEmpty b, _) -> [ generator; "-b"; b ];
            | (_, NotNullOrEmpty d) -> [ generator; "-d"; d ];
            | (_, _) -> [ generator ]
        
        Tooling.DotNet.ExecIn path dotnetArgs |> ignore

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


