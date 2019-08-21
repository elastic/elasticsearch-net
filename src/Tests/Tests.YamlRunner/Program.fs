open System
open System.Threading.Tasks
open FSharp.Data
open ShellProgressBar


let barOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Cyan,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       ProgressCharacter = '─',
       BackgroundColor = Nullable ConsoleColor.DarkGray,
       CollapseWhenFinished = true
    )
let subBarOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.DarkYellow,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       ProgressCharacter = '─',
       CollapseWhenFinished = true
    )
let branchOrTag = "master"
let folders branch = 
    let url = sprintf "https://github.com/elastic/elasticsearch/tree/%s/rest-api-spec/src/main/resources/rest-api-spec/test" branch
    let doc = HtmlDocument.Load(url)
    doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> not <| f.EndsWith(".asciidoc"))
        
let downloadYamlFile url (progress:ChildProgressBar) = async {
    let! yaml = Http.AsyncRequestString url
    progress.Tick(url)
    return yaml
}
let folderFiles branch folder (progress:ProgressBar) = async { 
    let url = sprintf "https://github.com/elastic/elasticsearch/tree/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s" branch folder
    let! doc = HtmlDocument.AsyncLoad(url)
    progress.Tick()
    let url file = sprintf "https://raw.githubusercontent.com/elastic/elasticsearch/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s/%s" branch folder file
    let yamlFiles =
        doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> f.EndsWith(".yml"))
        |> List.map url
    
    let filesProgress = progress.Spawn(yamlFiles.Length, sprintf "Downloading %i files in %s" yamlFiles.Length folder, subBarOptions)
    let actions =
        yamlFiles
        |> List.map (fun url -> downloadYamlFile url filesProgress)
    
    let result = Async.RunSynchronously <| Async.Parallel (actions, 4)
    return result
}

[<EntryPoint>]
let main argv =
    // load the live package stats for FSharp.Data
    let folders = folders branchOrTag
    use pbar = new ProgressBar(folders.Length, "Listing folders", barOptions)
    
    let x = folders |> List.map(fun folder -> folderFiles branchOrTag folder pbar)
    
    let result = Async.RunSynchronously <| Async.Parallel (x, 2)
    pbar.Dispose()
    
    printfn "Hello World %O" result 
    0 // return an integer exit code
    
