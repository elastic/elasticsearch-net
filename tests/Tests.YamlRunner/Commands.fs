// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.Commands

open System
open ShellProgressBar
open Tests.YamlRunner.AsyncExtensions
open Tests.YamlRunner.TestsLocator
open Tests.YamlRunner.TestsReader
open Tests.YamlRunner

let private barOptions = 
    ProgressBarOptions(
       ForegroundColor = ConsoleColor.Cyan,
       ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
       BackgroundColor = Nullable ConsoleColor.DarkGray,
       ProgressCharacter = '─',
       CollapseWhenFinished = true
    )
let private subBarOptions = 
    ProgressBarOptions(
        ForegroundColor = ConsoleColor.Yellow,
        ForegroundColorDone = Nullable ConsoleColor.DarkGreen,
        ProgressCharacter = '─',
        CollapseWhenFinished = true
    )

let LocateTests namedSuite revision directoryFilter fileFilter = async {
    let! folders = TestsLocator.ListFolders namedSuite revision directoryFilter
    if folders.Length = 0 then
        raise <| Exception("No folders found trying to list the yaml specs")
        
    let l = folders.Length
    use progress = new ProgressBar(l, sprintf "Listing %i folders" l, barOptions)
    progress.WriteLine <| sprintf "Listing %i folders" l
    let folderDownloads =
        folders
        |> Seq.map(fun folder -> TestsLocator.DownloadTestsInFolder folder fileFilter namedSuite revision progress subBarOptions)
    let! completed = Async.ForEachAsync 4 folderDownloads
    return completed 
}

let ReadTests (tests:LocateResults list) = 
    
    let readPaths paths = paths |> List.map TestsReader.ReadYamlFile  
    
    tests |> List.map (fun t -> { Folder= t.Folder; Files = readPaths t.Paths})
    
let RunTests (tests:YamlTestFolder list) client version namedSuite sectionFilter = async {
    do! Async.SwitchToNewThread()
    
    let f = tests.Length
    let l = tests |> List.sumBy (fun t -> t.Files.Length)
    use progress = new ProgressBar(l, sprintf "Folders [0/%i]" l, barOptions)
    let runner = TestRunner(client, version, namedSuite, progress, subBarOptions)
    runner.GlobalSetup() 
    let a (i, v) = async {
        let mainMessage = sprintf "[%i/%i] Folders : %s | " (i+1) f v.Folder
        let! op = runner.RunTestsInFolder mainMessage v sectionFilter
        return v, op |> Seq.toList
    }
    let x =
        tests
        |> Seq.indexed
        |> Seq.map a
        |> Seq.map Async.RunSynchronously
        |> Seq.toList
    
    progress.Message <- sprintf "[%i/%i] Folders [%i/%i] Files" f f l l
        
    return x
}

let ExportTests = TestsExporter.Export

let PrettyPrintResults = TestsExporter.PrettyPrintResults 

