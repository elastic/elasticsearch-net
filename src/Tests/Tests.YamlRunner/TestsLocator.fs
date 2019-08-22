module Tests.YamlRunner.TestsLocator

open System
open System.Threading
open FSharp.Data
open Tests.YamlRunner.AsyncExtensions
open ShellProgressBar
open Tests.YamlRunner

let ListFolders namedSuite revision  = async {
    let url = TestsDownloader.TestGithubRootUrl namedSuite revision
    let! (_, html) = TestsDownloader.CachedOrDownload revision "_root_" "index.html" url 
    let doc = HtmlDocument.Parse(html)
    
    return
        doc.CssSelect("td.content a.js-navigation-open")
        |> List.map (fun a -> a.InnerText())
        |> List.filter (fun f -> not <| f.EndsWith(".asciidoc"))
}
    
let ListFolderFiles namedSuite revision folder = async { 
    let url = TestsDownloader.FolderListUrl namedSuite revision folder
    let! (_, html) =  TestsDownloader.CachedOrDownload revision folder "index.html" url 
    let doc = HtmlDocument.Parse(html)
    let yamlFiles =
        let fileUrl file = (file, TestsDownloader.TestRawUrl namedSuite revision folder file)
        doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> f.EndsWith(".yml"))
        |> List.map fileUrl
    return yamlFiles
}

let private downloadTestsInFolder (yamlFiles:list<string * string>) folder revision (progress: IProgressBar) subBarOptions = async {
    let mutable seenFiles = 0;
    let filesProgress = progress.Spawn(yamlFiles.Length, sprintf "Downloading [0/%i] files in %s" yamlFiles.Length folder, subBarOptions)
    let actions =
        yamlFiles
        |> Seq.map (fun (file, url) -> async {
            let! (localFile, yaml) =  TestsDownloader.CachedOrDownload revision folder file url
            let i = Interlocked.Increment (&seenFiles)
            let message = sprintf "Downloaded [%i/%i] files in %s" i yamlFiles.Length folder
            filesProgress.Tick(message)
            match String.IsNullOrWhiteSpace yaml with
            | true ->
                progress.WriteLine(sprintf "Skipped %s since it returned no data" url)
                return None
            | _ ->
                return Some localFile
        })
        
    let! completed = Async.ForEachAsync 4 actions
    return completed
}

let DownloadTestsInFolder folder namedSuite revision (progress: IProgressBar) subBarOptions = async {
    let! token = Async.StartChild <| ListFolderFiles namedSuite revision folder 
    let! yamlFiles = token
    let! localFiles = async {
       match yamlFiles.Length with
       | 0 ->
           progress.WriteLine(sprintf "%s folder yielded no tests" folder)
           return None
       | _ ->
           let! result = downloadTestsInFolder yamlFiles folder revision progress subBarOptions
           return Some <| result
    }
    progress.Tick()
    return localFiles;
}
