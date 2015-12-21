#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake
open Paths

type Tests() =
    static member RunAll() =
        !! Paths.Source("**/bin/Release/*.Tests.Unit.dll")
        |> NUnit (fun p ->
          {p with
             ToolPath = "build/tools/Nunit.Runners/tools"
             DisableShadowCopy = true;
             OutputFile = Paths.Output("TestResults.xml") }
         )
