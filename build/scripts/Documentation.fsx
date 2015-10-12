#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths

module Documentation = 

    let private wintersmith action =
        let node = sprintf "\"%s\"" Paths.Tooling.Node.Path
        let wintersmith = sprintf "\"../%s\"" Paths.Tooling.Wintersmith.Path
        ExecProcess (fun p ->
            p.WorkingDirectory <- "docs"  
            p.FileName <- node
            p.Arguments <- sprintf "%s %s" wintersmith action
          ) 
          (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0)) |> ignore

    let Execute action = 
        wintersmith action

    let RunLitterateur = fun _ ->
        let litterateur = "src/CodeGeneration/Nest.Litterateur/bin/Release/Nest.Litterateur.exe"
        ExecProcess (fun p ->
            p.WorkingDirectory <- "src/CodeGeneration/Nest.Litterateur/bin/Release"
            p.FileName <- litterateur
          ) 
          (TimeSpan.FromMinutes (1.0)) |> ignore




