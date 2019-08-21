module Tests.YamlRunner.Main

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
        
let randomTime = Random()
let downloadYamlFile url = async {
    //let! yaml = Http.AsyncRequestString url
    let! x = Async.Sleep <| randomTime.Next(200, 900)
    return ""
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
    let! completed = foreach 4 actions
    progress.Tick()
}

let h = async {
    
    let folders = YamlTestsDownloader.ListFolders Locations.OpenSource branchOrTag
    
    use pbar = new ProgressBar(folders.Length, "Listing folders", barOptions)
    let folderDownloads =
        folders
        |> Seq.map(fun folder -> f (folderFiles branchOrTag folder) pbar)
    let! completed = foreach 4 folderDownloads
        
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
    
