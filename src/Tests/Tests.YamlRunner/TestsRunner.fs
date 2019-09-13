module Tests.YamlRunner.TestsRunner

open System
open ShellProgressBar
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader
open Tests.YamlRunner.OperationExecutor
open Tests.YamlRunner.Stashes
open Elasticsearch.Net

let private randomTime = Random()

let RunOperation file section operation nth stashes (progress:IProgressBar) = async {
    let executionContext = {
        Suite= OpenSource
        File= file
        Folder= file.Directory
        Section= section
        NthOperation= nth
        Operation= operation
        Stashes = stashes
    }
    return! OperationExecutor.Execute executionContext progress
}

let private createOperations m file (ops:Operations) progress = 
    let executedOperations =
        let stashes = Stashes()
        ops
        |> List.indexed
        |> List.map (fun (i, op) -> async {
            let! pass = RunOperation file m op i stashes progress
            //let! x = Async.Sleep <| randomTime.Next(0, 10)
            return pass
        })
    (m, executedOperations)

let RunTestFile progress (file:YamlTestDocument) = async {
    
    let m section ops = createOperations section file.FileInfo ops progress
    
    let setup =  file.Setup |> Option.map (m "Setup") |> Option.toList 
    let teardown = file.Teardown |> Option.map (m "Teardown") |> Option.toList 
    let passed = file.Tests |> List.map (fun s -> s.Operations |> m s.Name) 
    
    let sections = setup @ passed @ teardown
    
    let l = sections.Length
    let ops = sections |> List.sumBy (fun (_, i) -> i.Length)
    progress.MaxTicks <- ops
    
    let runSection progressHeader sectionHeader (ops: Async<ExecutionResult> list) = async {
        let l = ops |> List.length
        let result =
            ops
            |> List.indexed
            |> Seq.unfold (fun ms ->
                match ms with
                | (i, op) :: tl ->
                    let operations = sprintf "%s [%i/%i] operations: %s" progressHeader (i+1) l sectionHeader
                    progress.Tick(operations)
                    let r = Async.RunSynchronously op
                    match r with
                    | Succeeded context -> Some (r, tl)
                    | Skipped context -> Some (r, tl)
                    | Failed context -> Some (r, [])
                | [] -> None
            )
            |> List.ofSeq
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
        
        let x = DoMapper.Client.Indices.Delete<VoidResponse>("*") 
        let x = DoMapper.Client.Indices.DeleteTemplateForAll<VoidResponse>("*") 
        
        return result
    }
        
    let actions =
        folder.Files
        |> Seq.indexed 
        |> Seq.map run 
        |> Seq.map Async.RunSynchronously
    return actions
}

