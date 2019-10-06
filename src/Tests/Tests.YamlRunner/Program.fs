module Tests.YamlRunner.Main

open System
open System.Diagnostics
open System.Xml.Linq
open Argu
open Tests.YamlRunner
open Tests.YamlRunner.Models
open Tests.YamlRunner.OperationExecutor
open Elasticsearch.Net

type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of TestSuite
    | [<AltCommandLine("-f")>]Folder of string
    | [<AltCommandLine("-t")>]TestFile of string
    | [<AltCommandLine("-e")>]Endpoint of string
    | [<AltCommandLine("-r")>]Revision of string
    | [<AltCommandLine("-o")>]JUnitOutputFile of string
    with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | NamedSuite _ -> "specify a known yaml test suite. defaults to `opensource`."
            | Revision _ -> "The git revision to reference (commit/branch/tag). defaults to `master`"
            | Folder _ -> "Only run tests in this folder"
            | TestFile _ -> "Only run tests starting with this filename"
            | Endpoint _ -> "The elasticsearch endpoint to run tests against"
            | JUnitOutputFile _ -> "The path and file name to use for the junit xml output, defaults to a random tmp filename"

let private runningProxy = Process.GetProcessesByName("fiddler").Length + Process.GetProcessesByName("mitmproxy").Length > 0
let private defaultEndpoint = 
    let defaultHost = if runningProxy then "ipv4.fiddler" else "localhost";
    sprintf "http://%s:9200" defaultHost;

let private createClient endpoint = 
    let uri = new Uri(endpoint)
    let settings = new ConnectionConfiguration(uri)
    let settings =
        match runningProxy with
        | true -> settings.Proxy(Uri("http://ipv4.fiddler:8080"), String(null), String(null))
        | _ -> settings
    new ElasticLowLevelClient(settings)
    
let validateRevisionParams endpoint passedRevision =    
    let client = createClient endpoint
    let r =
        let config = new RequestConfiguration(DisableDirectStreaming=Nullable(true))
        let p = new RootNodeInfoRequestParameters(RequestConfiguration = config)
        client.RootNodeInfo<DynamicResponse>(p)
        
    printfn "%s" r.DebugInformation
    if not r.Success then
        failwithf "No running elasticsearch found at %s" endpoint
    
    let version = r.Get<string>("version.number") 
    let runningRevision = r.Get<string>("version.build_hash")
    
    // TODO validate the endpoint running confirms to expected `passedRevision`
    // needs to handle tags (7.4.0) and branches (7.x, 7.4, master)
    // not quite sure whats the rules are
    let revision = runningRevision
        
    (client, revision, version)
    
let runMain (parsed:ParseResults<Arguments>) = async {
    
    let namedSuite = parsed.TryGetResult NamedSuite |> Option.defaultValue OpenSource
    let directory = parsed.TryGetResult Folder //|> Option.defaultValue "indices.create" |> Some
    let file = parsed.TryGetResult TestFile //|> Option.defaultValue "10_basic.yml" |> Some
    let endpoint = parsed.TryGetResult Endpoint |> Option.defaultValue defaultEndpoint
    let passedRevision = parsed.TryGetResult Revision
    let outputFile =
        parsed.TryGetResult JUnitOutputFile
        |> Option.defaultValue (System.IO.Path.GetTempFileName())
    
    let (client, revision, version) = validateRevisionParams endpoint passedRevision
    
    printfn "Found version %s downloading specs from: %s" version revision
    
    let! locateResults = Commands.LocateTests namedSuite revision directory file
    let readResults = Commands.ReadTests locateResults 
    let! runResults = Commands.RunTests readResults client
    let summary = Commands.ExportTests runResults outputFile
    
    let contents = System.IO.File.ReadAllText outputFile
    printfn "%s" contents
    
    printfn "Total Tests: %i Failed: %i Errors: %i Skipped: %i"
        summary.Tests summary.Failed summary.Errors summary.Skipped
    printfn "Total Time %O" <| TimeSpan.FromSeconds summary.Time
        
    return summary.Failed + summary.Errors
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
    
