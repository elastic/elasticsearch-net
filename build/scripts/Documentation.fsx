#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths

module Documentation = 

    let RunLitterateur = fun _ ->
        let litterateur = "src/CodeGeneration/Nest.Litterateur/bin/Release/Nest.Litterateur.exe"
        ExecProcess (fun p ->
            p.WorkingDirectory <- "src/CodeGeneration/Nest.Litterateur/bin/Release"
            p.FileName <- litterateur
          ) 
          (TimeSpan.FromMinutes (1.0)) |> ignore




