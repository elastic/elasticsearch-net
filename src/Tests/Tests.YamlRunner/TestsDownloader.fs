module Tests.YamlRunner.TestsDownloader

open System
open System.IO
open FSharp.Data
open Tests.YamlRunner.Models


let private rootListingUrl = "https://github.com/elastic/elasticsearch"
let private rootRawUrl = "https://raw.githubusercontent.com/elastic/elasticsearch"

let private openSourceResourcePath = "rest-api-spec/src/main/resources"
let private xpackResourcesPath = "x-pack/plugin/src/test/resources"

let private path namedSuite revision =
    let path = match namedSuite with | OpenSource -> openSourceResourcePath | XPack -> xpackResourcesPath
    sprintf "%s/%s/rest-api-spec/test" revision  path
    
let TestGithubRootUrl namedSuite revision = sprintf "%s/tree/%s" rootListingUrl <| path namedSuite revision
    
let FolderListUrl namedSuite revision folder =
    let root = TestGithubRootUrl namedSuite revision
    sprintf "%s/%s" root folder
    
let TestRawUrl namedSuite revision folder file =
    let path = path namedSuite revision
    sprintf "%s/%s/%s/%s" rootRawUrl path folder file
        
let private randomTime = Random()

let TemporaryPath revision = lazy(Path.Combine(Path.GetTempPath(), "elastic", sprintf "tests-%s" revision))

let private download url = async {
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    let! yaml = Http.AsyncRequestString url
    return yaml
}
let CachedOrDownload revision folder file url = async {
    let parent = (TemporaryPath revision).Force()
    let directory = Path.Combine(parent, folder)
    let file = Path.Combine(directory, file)
    let fileExists = File.Exists file
    let directoryExists = Directory.Exists directory
    let! result = async {
        match (fileExists, directoryExists) with
        | (true, _) ->
            let! text = Async.AwaitTask <| File.ReadAllTextAsync file
            return text
        | (_, d) ->
            if (not d) then Directory.CreateDirectory(directory) |> ignore
            let! contents = download url
            let write = File.WriteAllTextAsync(file, contents)
            do! Async.AwaitTask write
            return contents
    }
    return (file, result)
}
    
