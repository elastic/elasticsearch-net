module Tests.YamlRunner.Main

open Argu
open System
open ShellProgressBar
open Tests.YamlRunner.AsyncExtensions

let barOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Cyan,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       BackgroundColor = Nullable ConsoleColor.DarkGray,
       ProgressCharacter = '─'
    )
let subBarOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Yellow,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       ProgressCharacter = '─'
    )
    
type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of Locations.TestSuite
    | [<AltCommandLine("-r")>]Revision of string
    with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | NamedSuite _ -> "specify a known yaml test suite. defaults to `opensource`."
            | Revision _ -> "The git revision to reference (commit/branch/tag). defaults to `master`"


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
            let namedSuite = parsed.TryGetResult NamedSuite |> Option.defaultValue Locations.OpenSource
            let revision = parsed.TryGetResult Revision |> Option.defaultValue "master"
            
            let! folders = YamlTestsDownloader.ListFolders namedSuite revision
            
            let l = folders.Length
            use progress = new ProgressBar(l, sprintf "Listing %i folders" l, barOptions)
            progress.WriteLine((YamlTestsDownloader.TemporaryPath revision).Force())
            let folderDownloads =
                folders
                |> Seq.map(fun folder -> YamlTestsDownloader.DownloadTestsInFolder folder namedSuite revision progress subBarOptions)
            let! completed = Async.ForEachAsync 4 folderDownloads
                
            return 0
        } |> Async.RunSynchronously
    
