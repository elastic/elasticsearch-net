module Tests.YamlRunner.Commands

open System
open System.Threading
open ShellProgressBar
open Tests.YamlRunner.AsyncExtensions
open Tests.YamlRunner.TestsLocator
open Tests.YamlRunner.TestsReader

let private barOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Cyan,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       BackgroundColor = Nullable ConsoleColor.DarkGray,
       ProgressCharacter = '─'
    )
let private subBarOptions = 
    ProgressBarOptions(ForegroundColor = ConsoleColor.Yellow, ForegroundColorDone = Nullable ConsoleColor.DarkGreen, ProgressCharacter = '─')

let LocateTests namedSuite revision = async {
    let! folders = TestsLocator.ListFolders namedSuite revision
    let l = folders.Length
    use progress = new ProgressBar(l, sprintf "Listing %i folders" l, barOptions)
    let folderDownloads =
        folders
        |> Seq.map(fun folder -> TestsLocator.DownloadTestsInFolder folder namedSuite revision progress subBarOptions)
    let! completed = Async.ForEachAsync 4 folderDownloads
    return completed 
}

let ReadTests (tests:LocateResults list) = 
    
    let readPaths paths = paths |> List.map TestsReader.ReadYamlFile
    
    tests |> List.map (fun t -> { Folder= t.Folder; Files = readPaths t.Paths})
    
let RunTests (tests:YamlTestFolder list) = async {
    do! Async.SwitchToNewThread()
    let l = tests |> List.sumBy (fun t -> t.Files.Length)
    use progress = new ProgressBar(l, sprintf "Folders [0/%i]" l, barOptions)
    let a (i, v) = async {
        let mainMessage = sprintf "Folders [%i/%i] (%s)" (i+1) l v.Folder 
        let! op = TestsRunner.RunTestsInFolder progress subBarOptions mainMessage v
        return op |> Seq.toList
    }
    let x =
        tests
        |> Seq.indexed
        |> Seq.map a
        |> Seq.map Async.RunSynchronously
        |> Seq.toList
    return x
}

