#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake
open Paths
open System.IO

// Normalizes path for different OS 
let inline normalizePath (path : string) =  
    path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) 

type Tests() =
    static member RunAll() =
        !! Paths.Source("**/bin/Release/*.Tests.Unit.dll")
        |> NUnit (fun p ->
          {p with
             ToolPath = "build/tools/Nunit.Runners/tools"
             DisableShadowCopy = true;
             OutputFile = normalizePath(Paths.Output("TestResults.xml")) }
         )
