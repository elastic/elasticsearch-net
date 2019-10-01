module Tests.YamlRunner.Main

open System
open System.Diagnostics
open Argu
open Tests.YamlRunner
open Tests.YamlRunner.Models
open Elasticsearch.Net

type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of TestSuite
    | [<AltCommandLine("-f")>]Folder of string
    | [<AltCommandLine("-t")>]TestFile of string
    | [<AltCommandLine("-e")>]Endpoint of string
    | [<AltCommandLine("-r")>]Revision of string
    | [<AltCommandLine("-D");>]DownloadOnly of bool
    with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | NamedSuite _ -> "specify a known yaml test suite. defaults to `opensource`."
            | Revision _ -> "The git revision to reference (commit/branch/tag). defaults to `master`"
            | Folder _ -> "Only run tests in this folder"
            | TestFile _ -> "Only run tests starting with this filename"
            | Endpoint _ -> "The elasticsearch endpoint to run tests against"
            | DownloadOnly _ -> "Only download the tests, do not attempt to run them"

let private createClient endpoint = 
    let runningProxy = Process.GetProcessesByName("fiddler").Length + Process.GetProcessesByName("mitmproxy").Length > 0
    let defaultHost = if runningProxy then "ipv4.fiddler" else "localhost";
    let defaultUrl = sprintf "http://%s:9200" defaultHost;
    let uri = match endpoint with | Some s -> new Uri(s) | _ -> new Uri(defaultUrl)
    let settings = new ConnectionConfiguration(uri)
    let settings =
        match runningProxy with
        | true -> settings.Proxy(Uri("http://ipv4.fiddler:8080"), String(null), String(null))
        | _ -> settings
    new ElasticLowLevelClient(settings)

let runMain (parsed:ParseResults<Arguments>) = async {
    
    let namedSuite = parsed.TryGetResult NamedSuite |> Option.defaultValue OpenSource
    let revision = parsed.TryGetResult Revision |> Option.defaultValue "master"
    let directory = parsed.TryGetResult Folder //|> Option.defaultValue "indices.create" |> Some
    let file = parsed.TryGetResult TestFile //|> Option.defaultValue "10_basic.yml" |> Some
    let endpoint = parsed.TryGetResult Endpoint
    let downloadOnly = parsed.TryGetResult DownloadOnly |> Option.defaultValue false
    
    let client = createClient endpoint
    
    let! locateResults = Commands.LocateTests namedSuite revision directory file
    match downloadOnly with
    | true ->
        let exitCode = if locateResults.Length > 0 then 0 else 1;
        return exitCode
    | false ->
        let readResults = Commands.ReadTests locateResults 
        let! runTesults = Commands.RunTests readResults client
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
    
