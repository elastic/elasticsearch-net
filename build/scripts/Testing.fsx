#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
open System
open Fake 
open Paths
open Fake.Testing

type Tests() = 
    static member RunAll() =
        !! Paths.Source("**/bin/Release/Tests.dll") 
            |> xUnit2 (fun p ->
            {p with
                XmlOutputPath = Some <| Paths.Output("TestResults.xml") 
                }
            )
        traceFAKE "This branch no longer NUnit (atleast for now) redo testing in build script"


