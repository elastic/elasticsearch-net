// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.TestsDownloader

open System
open System.IO
open Newtonsoft.Json.Linq
open FSharp.Data
open Tests.YamlRunner.Models


let private rootListingUrl = "https://github.com/elastic/elasticsearch"
let private rootRawUrl = "https://raw.githubusercontent.com/elastic/elasticsearch"

let private openSourceResourcePath = "rest-api-spec/src/main/resources"
let private xpackResourcesPath = "x-pack/plugin/src/test/resources"

let private path namedSuite revision =
    let path = match namedSuite with | Free -> openSourceResourcePath | Platinum -> xpackResourcesPath
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

let private download url file = async {
    let! _wait = Async.Sleep <| randomTime.Next(500, 900)
    let! data = Http.AsyncRequestStream url
    use outputFile = new FileStream(file, FileMode.Create)
    do! (Async.AwaitIAsyncResult <| data.ResponseStream.CopyToAsync( outputFile )) |> Async.Ignore
    return file
}
let private downloadAndRead url file = async {
    let! _wait = Async.Sleep <| randomTime.Next(500, 900)
    let! data = Http.AsyncRequestStream url
    use outputFile = new FileStream(file, FileMode.Create)
    do! (Async.AwaitIAsyncResult <| data.ResponseStream.CopyToAsync( outputFile )) |> Async.Ignore
    let contents = File.ReadAllText file
    return (file, contents)
}

let CachedOrDownload revision folder file url = async {
    let parent = (TemporaryPath revision).Force()
    let directory = Path.Combine(parent, folder)
    let file = Path.Combine(directory, file)
    let fileExists = File.Exists file
    let directoryExists = Directory.Exists directory
    let! result = async {
        match (fileExists, directoryExists) with
        | (true, _) -> return file
        | (_, d) ->
            if (not d) then Directory.CreateDirectory(directory) |> ignore
            let! file = download url file
            return file
    }
    return result
}

let DownloadBuildInformation (version:SemVer.Version) revision = async {
    
    let resourcesZipUrl = 
        match version.PreRelease with
        | "SNAPSHOT" ->
            printfn "Found version %O locating build for revision: %s" version revision
            let url = sprintf "https://artifacts-api.elastic.co/v1/versions/%O" version
            
            let versionsJson = Async.RunSynchronously <| CachedOrDownload revision "_artifacts-api" "versions.json" url
            let json = File.ReadAllText versionsJson
            
            let json = JObject.Parse json
            let packages =
                json.SelectTokens("$.version.builds..projects.elasticsearch")
                |> Seq.toList
                |> List.filter(fun token -> token.SelectToken("$.commit_hash").Value<string>() = revision)
                |> List.map(fun token -> token.SelectToken("$.packages"))
                |> List.tryHead
            
            let package =
                match packages with
                | None -> raise <| Exception(sprintf "Can not locate SNAPSHOT for hash: %s" revision)
                | Some packages -> packages.SelectToken(sprintf "$.['rest-resources-zip-%s.zip'].url" "8.0.0-SNAPSHOT")
                
            package.Value<string>()
        | _ ->
            printfn "Found version %O locating released zip" version
            sprintf "https://artifacts-api.elastic.co/v1/downloads/elasticsearch/rest-resources-zip-%O.zip" version
            
    let zipFileName = sprintf "rest-resources-zip-%O-%s.zip" version revision
    let! downloadedZipFile = CachedOrDownload revision "_artifacts-api" zipFileName resourcesZipUrl
    
    printfn "Downloaded: %s" resourcesZipUrl
    printfn "Location on disk: %s" downloadedZipFile
    return downloadedZipFile 
}

let Unzip file version revision =
    let unzipLocation = 
        let parent = (TemporaryPath revision).Force()
        Path.Combine(parent, "_unzipped")
    if (Directory.Exists unzipLocation) then Directory.Delete (unzipLocation, true)
    if (not <| Directory.Exists unzipLocation) then Directory.CreateDirectory unzipLocation |> ignore
    System.IO.Compression.ZipFile.ExtractToDirectory(file, unzipLocation);
    printf "Unzipped %s to: %s" file unzipLocation
    unzipLocation
    
    
    

    
