#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths

type Tests() = 
    static member RunAll() =
       //!! Paths.Source("**/bin/Release/Tests.dll") 
       //$|> NUnit (fun p ->
       //  {p with
       //      DisableShadowCopy = true;
       //      OutputFile = Paths.Output("TestResults.xml") }
       //)
       traceFAKE "This branch no longer NUnit (atleast for now) redo testing in build script"


