#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths

type DocumentationBlock =
    | Markdown of string
    | Code of string

type DocumentationFile(ast: SyntaxTree, fileName: string) = 
    member this.x = ""

module Documentation = 

    let private wintersmith action =
        let node = "../" + Paths.Tooling.Node.Path
        let wintersmith = "../" + Paths.Tooling.Wintersmith.Path
        ExecProcess (fun p ->
            p.WorkingDirectory <- "docs"  
            p.FileName <- node
            p.Arguments <- sprintf "\"%s\" %s" wintersmith action
          ) 
          (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0)) |> ignore

    let build = 
        wintersmith "build"

