module Tests.YamlRunner.Main

open Argu
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader

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
    
    let! locateResults = Commands.LocateTests namedSuite revision 
    
    return 0
}

[<EntryPoint>]
let main argv =
    
    let documents = TestsReader.test ()
    0
    
    
//    let parser = ArgumentParser.Create<Arguments>(programName = "yaml-test-runner")
//    let parsed = 
//        try
//            Some <| parser.ParseCommandLine(inputs = argv, raiseOnUsage = true)
//        with e ->
//            printfn "%s" e.Message
//            None
//    match parsed with
//    | None -> 1
//    | Some parsed ->
//        async {
//            do! Async.SwitchToThreadPool ()
//            return! runMain parsed
//        } |> Async.RunSynchronously
//    
