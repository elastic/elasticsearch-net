open System
open System.Threading
open System.Threading.Tasks
open FSharp.Data
open ShellProgressBar


let barOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Cyan,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       ProgressCharacter = '─',
       BackgroundColor = Nullable ConsoleColor.DarkGray
    )
let subBarOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Yellow,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       ProgressCharacter = '─'
    )
let branchOrTag = "master"
let folders branch = 
    let url = sprintf "https://github.com/elastic/elasticsearch/tree/%s/rest-api-spec/src/main/resources/rest-api-spec/test" branch
    let doc = HtmlDocument.Load(url)
    doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> not <| f.EndsWith(".asciidoc"))
        
let downloadYamlFile url = async {
    //let! yaml = Http.AsyncRequestString url
    let! x = Async.Sleep 500
    return ""
}
let folderFiles branch folder = async { 
    let url = sprintf "https://github.com/elastic/elasticsearch/tree/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s" branch folder
    let! doc = HtmlDocument.AsyncLoad(url)
    let url file = sprintf "https://raw.githubusercontent.com/elastic/elasticsearch/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s/%s" branch folder file
    let yamlFiles =
        doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> f.EndsWith(".yml"))
        |> List.map url
    return (folder, yamlFiles)
}
let foreach<'a> maxDegreeOfParallelism (asyncs:seq<Async<'a>>) = async {
    let tasks = new System.Collections.Generic.List<Task<'a>>(4)
    for async in asyncs do
        let! x = Async.StartChildAsTask async
        tasks.Add <| x
        if (tasks.Count >= maxDegreeOfParallelism) then
            let! task = Async.AwaitTask <| Task.WhenAny(tasks)
            if (task.IsFaulted) then System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(task.Exception).Throw();
            let removed = tasks.Remove <| task
            ignore()
    let! completed = Async.AwaitTask <| Task.WhenAll tasks
    ignore()
}

let any asyncs =
    async {
        let! t = 
            asyncs
            |> Seq.map Async.StartAsTask
            |> System.Threading.Tasks.Task.WhenAny
            |> Async.AwaitTask

        return t.Result }


let f (yamlFiles:Async<string * list<string>>) (progress: IProgressBar) = async {
    let! token = Async.StartChild yamlFiles
    let! (folder, yamlFiles) = token
    
    let mutable seenFiles = 0;
    let filesProgress = progress.Spawn(yamlFiles.Length, sprintf "Downloading [0/%i] files in %s" yamlFiles.Length folder, subBarOptions)
    let actions =
        yamlFiles
        |> Seq.map (fun url -> async {
            let! result = downloadYamlFile url
            let i = Interlocked.Increment (&seenFiles)
            let message = sprintf "Downloaded [%i/%i] files in %s" i yamlFiles.Length folder
            filesProgress.Tick(message)
            return result
        })
//        |> Seq.indexed
//        |> Seq.groupBy (fun (i, _) -> i % yamlFiles.Length / 4)
//        |> Seq.map (fun (_, indexed) -> indexed |> Seq.map (fun (_, url) -> url))
        
        
    let! completed = foreach 1 actions
    progress.Tick()
}

let h = async {
    let folders = folders branchOrTag
    
    
    use pbar = new ProgressBar(folders.Length, "Listing folders", barOptions)
    let folderDownloads =
        folders
        |> Seq.map(fun folder -> f (folderFiles branchOrTag folder) pbar)
//        |> Seq.indexed
//        |> Seq.groupBy (fun (i, _) -> i % folders.Length / 2)
//        |> Seq.map (fun (_, indexed) -> indexed |> Seq.map (fun (_, folder) -> folder))
//        |> Seq.toList

    let! completed = foreach 4 folderDownloads
        
//    for group in folderDownloads do
//        let tasks = group |> Seq.map (fun files -> f files pbar)
//        let! token = Async.StartChild <| Async.Parallel (tasks , 4)
//        let! result = token
//        ignore()
        
    pbar.Dispose()
    
    printfn "Hello World" 
}

[<EntryPoint>]
let main argv =
    async {
        do! Async.SwitchToThreadPool ()
        let! exitCode = h
        return 0
    } |> Async.RunSynchronously
    
