// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
    let path = match namedSuite with | Oss -> openSourceResourcePath | XPack -> xpackResourcesPath
    sprintf "%s/%s/rest-api-spec/test" revision  path
    
let TestGithubRootUrl namedSuite revision = sprintf "%s/tree/%s" rootListingUrl <| path namedSuite revision
    
let FolderListUrl namedSuite revision folder =
    let root = TestGithubRootUrl namedSuite revision
    sprintf "%s/%s" root folder
    
let TestRawUrl namedSuite revision folder file =
    let path = path namedSuite revision
    sprintf "%s/%s/%s/%s" rootRawUrl path folder file
        
let private randomTime = Random()

let TemporaryPath revision suite = lazy(Path.Combine(Path.GetTempPath(), "elastic", sprintf "tests-%s-%s" suite revision))

let private download url = async {
    let! _wait = Async.Sleep <| randomTime.Next(500, 900)
    let! yaml = Http.AsyncRequestString url
    return yaml
}
let CachedOrDownload namedSuite revision folder file url = async {
    let suite = match namedSuite with | Oss -> "oss" | XPack -> "xpack"
    let parent = (TemporaryPath revision suite).Force()
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
    
