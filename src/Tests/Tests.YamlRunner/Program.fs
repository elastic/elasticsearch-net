open System
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
        
let downloadYamlFile url (progress:ChildProgressBar) = async {
    //let! yaml = Http.AsyncRequestString url
    let! x = Async.Sleep 2000
    progress.Tick(url)
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

let f (yamlFiles:Async<string * list<string>>) (progress: IProgressBar) = async {
    let! token = Async.StartChild yamlFiles
    let! (folder, yamlFiles) = token
    
    let filesProgress = progress.Spawn(yamlFiles.Length, sprintf "Downloading %i files in %s" yamlFiles.Length folder, subBarOptions)
    let actions =
        yamlFiles
        |> Seq.indexed
        |> Seq.groupBy (fun (i, _) -> i % yamlFiles.Length / 4)
        |> Seq.map (fun (_, indexed) -> indexed |> Seq.map (fun (_, url) -> url))
        //|> Seq.map (fun url -> downloadYamlFile url filesProgress)
        
    for files in actions do
        let tasks = files |> Seq.map (fun f -> downloadYamlFile f filesProgress)
        let! token = Async.StartChild <| Async.Parallel tasks
        let! result = token
        ignore()
            
    progress.Tick()
}

let h = async {
    let folders = folders branchOrTag
    
    
    use pbar = new ProgressBar(folders.Length, "Listing folders", barOptions)
    let folderDownloads =
        folders
        |> Seq.map(fun folder -> folderFiles branchOrTag folder)
        |> Seq.indexed
        |> Seq.groupBy (fun (i, _) -> i % folders.Length / 4)
        |> Seq.map (fun (_, indexed) -> indexed |> Seq.map (fun (_, folder) -> folder))
        |> Seq.toList
        
    for group in folderDownloads do
        pbar.WriteLine( sprintf "length %i" (group |> Seq.length) )
        let tasks = group |> Seq.map (fun files -> f files pbar)
        let! token = Async.StartChild <| Async.Parallel tasks
        let! result = token
        ignore()
        
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
    
