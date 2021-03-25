// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.Commands

open System
open System.IO
open ShellProgressBar
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

type LocateResults = { Folder: string; Paths: FileInfo list } 

let LocateTests version revision directoryFilter fileFilter = async {
    let! resourcesZip = TestsDownloader.DownloadBuildInformation version revision
    let unpackedLocation = TestsDownloader.Unzip resourcesZip version revision
    let folders =
        (DirectoryInfo <| Path.Combine(unpackedLocation, "rest-api-spec", "test")).GetDirectories()
        |> Array.filter(fun d -> directoryFilter|> Option.isNone || directoryFilter |> Option.exists(fun f -> d.Name = f))
        |> Array.map(fun dir ->
            let files =
                dir.GetFiles()
                |> Array.filter(fun d -> fileFilter |> Option.isNone || directoryFilter |> Option.exists(fun f -> d.Name = f))
                |> Array.filter(fun f -> f.Extension = ".yml")
                |> Array.toList
            { Folder = dir.Name; Paths = files }
        )
        |> Array.toList
    return folders
}

let ReadTests (tests:LocateResults list) = 
    let readPaths paths = paths |> List.map ReadYamlFile  
    tests |> List.map (fun t -> { Folder= t.Folder; Files = readPaths t.Paths})
    
let RunTests (tests:YamlTestFolder list) client version namedSuite sectionFilter = async {
    do! Async.SwitchToNewThread()
    
    let f = tests.Length
    let l = tests |> List.sumBy (fun t -> t.Files.Length)
    use progress = new ProgressBar(l, sprintf "Folders [0/%i]" l, barOptions)
    let runner = TestRunner(client, version, namedSuite, progress, subBarOptions)
    runner.GlobalSetup() 
    let a (i, (v:YamlTestFolder)) = async {
        let mainMessage = sprintf "[%i/%i] Folders : %s | " (i+1) f v.Folder
        printfn "Tests: %i" v.Files.Length
    
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

