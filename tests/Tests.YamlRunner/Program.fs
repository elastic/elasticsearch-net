module Tests.YamlRunner.Main

open System
open System.Linq
open System.Diagnostics
open Argu
open Tests.YamlRunner
open Tests.YamlRunner.Models
open Elasticsearch.Net
open Elasticsearch.Net
open Elasticsearch.Net
open Elasticsearch.Net

type Arguments =
    | [<First; MainCommand; CliPrefix(CliPrefix.None)>] NamedSuite of TestSuite
    | [<AltCommandLine("-f")>]Folder of string
    | [<AltCommandLine("-t")>]TestFile of string
    | [<AltCommandLine("-e")>]Endpoint of string
    | [<AltCommandLine("-r")>]Revision of string
    | [<AltCommandLine("-o")>]JUnitOutputFile of string
    | [<AltCommandLine("-p")>]Profile of bool
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
            | Profile _ -> "Print out process id and wait for confirmation to kick off the tests"

let private runningMitmProxy = Process.GetProcessesByName("mitmproxy").Length > 0
let private runningProxy = runningMitmProxy || Process.GetProcessesByName("fiddler").Length > 0
let private defaultEndpoint namedSuite = 
    let host = 
        match (runningProxy, namedSuite) with
        | (true, OpenSource) -> "ipv.fiddler"
        | _ -> "localhost"
    let https = match namedSuite with | XPack -> "s" | _ -> ""
    sprintf "http%s://%s:9200" https host;

let private createClient endpoint namedSuite = 
    let uri, userInfo = 
        let e = Uri(endpoint)
        let sanitized = UriBuilder(e)
        sanitized.UserName <- null
        sanitized.Password <- null
        let uri = sanitized.Uri
        let tokens = e.UserInfo.Split(':') |> Seq.toList
        match (tokens, namedSuite) with 
        | ([username; password], _) -> uri, Some (username, password)
        | (_, XPack) -> uri, Some ("elastic", "changeme")
        | _ -> uri, None
    let settings = new ConnectionConfiguration(uri)
    // proxy 
    let proxySettings =
        match (runningMitmProxy, namedSuite) with
        | (true, OpenSource) -> settings.Proxy(Uri("http://ipv4.fiddler:8080"), String(null), String(null))
        | _ -> settings
    // auth
    let authSettings =
        match userInfo with
        | Some(username, password) -> proxySettings.BasicAuthentication(username, password)
        | _ -> proxySettings
    // certs
    let authSettings =
        match namedSuite with
        | XPack -> 
            proxySettings.ServerCertificateValidationCallback(fun _ _ _ _ -> true)
        | _ -> proxySettings
    new ElasticLowLevelClient(authSettings)
    
let validateRevisionParams endpoint passedRevision namedSuite =    
    let client = createClient endpoint namedSuite
    
    let node = client.Settings.ConnectionPool.Nodes.First()
    let auth =     
        match client.Settings.BasicAuthenticationCredentials with 
        | null -> ""
        | s -> sprintf "%s:%s" s.Username (s.Password.CreateString())
        
    printfn "Running elasticsearch %O %s" (node.Uri) auth
    
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
    let endpoint = parsed.TryGetResult Endpoint |> Option.defaultValue (defaultEndpoint namedSuite)
    let profile = parsed.TryGetResult Profile |> Option.defaultValue false
    let passedRevision = parsed.TryGetResult Revision
    let outputFile =
        parsed.TryGetResult JUnitOutputFile
        |> Option.defaultValue (System.IO.Path.GetTempFileName())
        
    let (client, revision, version) = validateRevisionParams endpoint passedRevision namedSuite
    
    printfn "Found version %s downloading specs from: %s" version revision
    
    let! locateResults = Commands.LocateTests namedSuite revision directory file
    let readResults = Commands.ReadTests locateResults
    if profile then
        printf "Waiting for profiler to attach to pid: %O" <| Process.GetCurrentProcess().Id
        Console.ReadKey() |> ignore
        
    let! runResults = Commands.RunTests readResults client version namedSuite
    let summary = Commands.ExportTests runResults outputFile
    
    Commands.PrettyPrintResults outputFile
    
    printfn "JUnit output: %s" outputFile
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
    
