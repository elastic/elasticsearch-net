module Tests.YamlRunner.Main

open Argu
open Tests.YamlRunner
open Tests.YamlRunner.Models

type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of TestSuite
    | [<AltCommandLine("-r")>]Revision of string
    | [<AltCommandLine("-d")>]Directory of string
    | [<AltCommandLine("-f")>]File of string
    with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | NamedSuite _ -> "specify a known yaml test suite. defaults to `opensource`."
            | Revision _ -> "The git revision to reference (commit/branch/tag). defaults to `master`"
            | Directory _ -> "Only run tests in this folder"
            | File _ -> "Only run tests starting with this filename"


let runMain (parsed:ParseResults<Arguments>) = async {
    
    let namedSuite = parsed.TryGetResult NamedSuite |> Option.defaultValue OpenSource
    let revision = parsed.TryGetResult Revision |> Option.defaultValue "master"
    let directory = parsed.TryGetResult Directory |> Option.defaultValue "indices.create" |> Some
    let file = parsed.TryGetResult File //|> Option.defaultValue "10_basic.yml" |> Some
    
    let! locateResults = Commands.LocateTests namedSuite revision directory file
    let readResults = Commands.ReadTests locateResults 
    let! runTesults = Commands.RunTests readResults 
    
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
    
