#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
#r "System.Xml.Linq.dll"

#load @"Paths.fsx"

open System
open Fake 
open Paths
open Fake.Testing
open System.Xml.Linq;

// xunit console runner is broken on mono 4.0.2 better run with a nightly build:
// http://download.mono-project.com/archive/nightly/macos-10-x86/

// however even in the latest beta 4.3.0 the runner hangs on mono for me
// https://github.com/xunit/xunit/issues/158

module Tests = 
    let xmlOutput = Paths.Output("TestResults.xml")

    let RunAll() =
        !! Paths.Source("**/bin/Release/Tests.dll") 
            |> xUnit2 (fun p -> {p with XmlOutputPath = Some <| xmlOutput } )

    let private notify = fun _ -> 
        let results = XDocument.Load xmlOutput
        let assembly = results.Root.Element <| XName.Get "assembly"
        let attr name = 
            let a = assembly.Attribute <| XName.Get name
            Int32.Parse(a.Value)

        let errors = attr "failed"
        let total = attr "total"
        let skipped = attr "skipped"
        match errors with
        | 0 ->
            let successMessage = sprintf "\"All %i tests are passing!\"" total
            Paths.Tooling.Notifier.Exec ["-t " + successMessage; "-m " + successMessage]
        | _ ->
            let errorMessage = sprintf "\"%i failed %i run, %i skipped\"" errors total skipped
            Paths.Tooling.Notifier.Exec ["-t " + errorMessage; "-m " + errorMessage]

    let RunContinuous = fun _ ->
        try  
            !! Paths.Source("**/bin/Release/Tests.dll") 
            |> xUnit2 (fun p -> { p with XmlOutputPath = Some <| xmlOutput })
        finally
            notify()
