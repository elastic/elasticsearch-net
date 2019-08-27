module Tests.YamlRunner.TestsRunner

open System
open System.Threading
open ShellProgressBar
open Tests.YamlRunner
open Tests.YamlRunner.AsyncExtensions
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader

let private randomTime = Random()
let RunOperation m operation = async {
    //let! x = Async.Sleep <| randomTime.Next(500, 900)
    return true
}

let createOperations m (ops:Operations) = 
    let executedOperations =
        ops
        |> List.map (fun op -> async {
            let! pass = RunOperation m op
            let! x = Async.Sleep <| randomTime.Next(50, 200)
            return pass
        })
    (m, executedOperations)
    

let RunTestFile (progress:IProgressBar) (barOptions:ProgressBarOptions) (file:YamlTestDocument) = async {
    
    let message m = sprintf "%s: %s" m file.FileInfo.FullName
    let f a v = createOperations <| message a <| v
    
    let setup =  file.Setup |> Option.map (f "Setup") |> Option.toList 
    let teardown = file.Teardown |> Option.map (f "Teardown") |> Option.toList 
    let passed = file.Tests |> List.map (fun s -> s.Operations |> f s.Name) 
    
    let sections = (setup @ passed @ teardown) 
    
    let l = sections.Length
    let ops = sections |> List.sumBy (fun (_, i) -> i.Length)
    progress.MaxTicks <- ops
    
    let actions =
        sections
        |> Seq.indexed
        |> Seq.map (fun (i, suite) -> async {
            let sections = sprintf "[%i/%i] sections" (i+1) l
            
            let (m, ops) = suite
            let lOps = ops.Length
            let result =
                ops
                |> Seq.indexed
                |> Seq.map (fun (i, op) -> async {
                    let operations = sprintf "%s [%i/%i] operations: %s" sections (i+1) lOps m
                    progress.Tick(operations)
                    return! op
                })
                |> Seq.map Async.RunSynchronously
                |> Seq.toList
            return result
        })
        |> Seq.map Async.RunSynchronously
    
    return actions |> Seq.toList
    
}

let RunTestsInFolder (progress:IProgressBar) (barOptions:ProgressBarOptions) mainMessage (folder:YamlTestFolder) = async {
    let l = folder.Files.Length
    let run (i, document) = async {
        let file = sprintf "%s/%s" document.FileInfo.Directory.Name document.FileInfo.Name
        let message = sprintf "%s Files [%i/%i] file: %s" mainMessage (i+1) l file
        progress.Tick(message)
        let message = sprintf "Inspecting file for sections" 
        use p = progress.Spawn(0, message, barOptions)
        let! result = RunTestFile p barOptions document
        return result
    }
        
    let actions =
        folder.Files
        |> Seq.indexed 
        |> Seq.map run 
        |> Seq.map Async.RunSynchronously
    return actions
}

