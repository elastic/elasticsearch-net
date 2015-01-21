#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths

type Documentation() = 
    static member Execute action =
        let node = Paths.Tool("Node.js/node.exe")
        let wintersmith = @"..\build\tools\node_modules\wintersmith\bin\wintersmith"
        ExecProcess (fun p ->
            p.WorkingDirectory <- "docs"  
            p.FileName <- node
            p.Arguments <- sprintf "\"%s\" %s" wintersmith action
          ) 
          (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0)) |> ignore


