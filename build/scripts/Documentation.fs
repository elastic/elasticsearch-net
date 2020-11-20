// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System.IO

open Commandline
open Fake.Core

module Documentation = 

    exception DocGenError of string 
    let Generate args = 
        let path = Paths.InplaceBuildOutput "DocGenerator" "net5.0"
        let generator = sprintf "%s.dll" "DocGenerator"
        
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
        
        let rec retry times fn =
            if times > 1 then
                try
                    fn(times)
                with 
                | _ -> retry (times - 1) fn
            else
                fn(times)
                
        // This seems silly? That's because it is!
        // If `Paths.MagicDocumentationFile` (a cache in Elasticsearch.Net `obj` folder) is missing
        // `BuildAlyzer` has problems loading the project.
        //
        // This file only appears however when running the generation command multiple times
        //
        // Solution: brute force run it a bunch of times
        retry 5 <| fun times ->
            printfn "Attempt %i to generate the docs" (System.Math.Abs(times - 5) + 1)
            let result = Tooling.DotNet.ReadQuietIn path dotnetArgs
            if result.ExitCode = 0 || times = 1 || File.Exists(Paths.MagicDocumentationFile) then
                result.Output |> Seq.iter (fun l -> if l.Error then printfn "%s" l.Line else eprintfn "%s" l.Line)
                if result.ExitCode <> 0 then
                    eprintfn "documentation generation failed"
                    raise (DocGenError("documentation generation failed"))
            else
                eprintfn "documentation generation failed"
                raise (DocGenError("documentation generation failed"))

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


