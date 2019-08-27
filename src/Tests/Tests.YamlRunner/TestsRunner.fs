module Tests.YamlRunner.TestsRunner

open System
open ShellProgressBar
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader

let private randomTime = Random()

let RunOperation m operation = async {
    return OperationExecutor.Execute operation
}

let private createOperations m (ops:Operations) = 
    let executedOperations =
        ops
        |> List.map (fun op -> async {
            let! pass = RunOperation m op
            let! x = Async.Sleep <| randomTime.Next(0, 10)
            return pass
        })
    (m, executedOperations)
    

let RunTestFile (progress:IProgressBar) (file:YamlTestDocument) = async {
    
    let setup =  file.Setup |> Option.map (createOperations "Setup") |> Option.toList 
    let teardown = file.Teardown |> Option.map (createOperations "Teardown") |> Option.toList 
    let passed = file.Tests |> List.map (fun s -> s.Operations |> createOperations s.Name) 
    
    let sections = setup @ passed @ teardown
    
    let l = sections.Length
    let ops = sections |> List.sumBy (fun (_, i) -> i.Length)
    progress.MaxTicks <- ops
    
    let runSection progressHeader sectionHeader ops = async {
        let l = ops |> List.length
        let result =
            ops
            |> List.indexed
            |> Seq.map (fun (i, op) -> async {
                let operations = sprintf "%s [%i/%i] operations: %s" progressHeader (i+1) l sectionHeader
                progress.Tick(operations)
                return! op
            })
            |> Seq.map Async.RunSynchronously
            |> Seq.toList
        return result
    }
    
    let runAllSections =
        sections
        |> Seq.indexed
        |> Seq.map (fun (i, suite) -> 
            let progressHeader = sprintf "[%i/%i] sections" (i+1) l
            let (sectionHeader, ops) = suite
            runSection progressHeader sectionHeader ops 
        )
        |> Seq.map Async.RunSynchronously
    
    return runAllSections |> Seq.toList
    
}

let RunTestsInFolder (progress:IProgressBar) (barOptions:ProgressBarOptions) mainMessage (folder:YamlTestFolder) = async {
    let l = folder.Files.Length
    let run (i, document) = async {
        let file = sprintf "%s/%s" document.FileInfo.Directory.Name document.FileInfo.Name
        let message = sprintf "%s [%i/%i] Files : %s" mainMessage (i+1) l file
        progress.Tick(message)
        let message = sprintf "Inspecting file for sections" 
        use p = progress.Spawn(0, message, barOptions)
        let! result = RunTestFile p document
        return result
    }
        
    let actions =
        folder.Files
        |> Seq.indexed 
        |> Seq.map run 
        |> Seq.map Async.RunSynchronously
    return actions
}

