module Tests.YamlRunner.Commands

open System
open ShellProgressBar
open Tests.YamlRunner.AsyncExtensions

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

