module Tests.YamlRunner.Main

open Argu
open Elasticsearch.Net
open Tests.YamlRunner
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader
open Tests.YamlRunner.TestsLocator

type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of TestSuite
    | [<AltCommandLine("-r")>]Revision of string
    with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | NamedSuite _ -> "specify a known yaml test suite. defaults to `opensource`."
            | Revision _ -> "The git revision to reference (commit/branch/tag). defaults to `master`"


let runMain (parsed:ParseResults<Arguments>) = async {
    
    let namedSuite = parsed.TryGetResult NamedSuite |> Option.defaultValue OpenSource
    let revision = parsed.TryGetResult Revision |> Option.defaultValue "master"
    
    
//    let test = TestsLocator.TestLocalFile "/tmp/elastic/tests-master/bulk/60_deprecated.yml"
//    let read = TestsReader.ReadYamlFile test
    
    
    
    let! locateResults = Commands.LocateTests namedSuite revision
    let readResults = Commands.ReadTests locateResults 
    let! runTesults = Commands.RunTests readResults 
//    
//    printfn "folders: %O" locateResults.Length
//    for folder in locateResults do 
//        printfn "folder: %O" folder.Folder
//        for p in folder.Paths do
//            printfn "     %s" p.File
    
//    printfn "folders: %O" locateResults.Length
//    for folder in readResults do 
//        printfn "folder: %O" folder.Folder
//        for f in folder.Files do
//            for t in f.Tests do
//                printfn "     %s (%i steps)" t.Name t.Operations.Length
    
    return 0
}

[<EntryPoint>]
let main argv =
    
    let parser = ArgumentParser.Create<Arguments>(programName = "yaml-test-runner")
    let parsed = 
        try
            Some <| parser.ParseCommandLine(inputs = argv, raiseOnUsage = true)
        with e ->
            printfn "%s" e.Message
            None
    match parsed with
    | None -> 1
    | Some parsed ->
        async {
            do! Async.SwitchToThreadPool ()
            return! runMain parsed
        } |> Async.RunSynchronously
    
