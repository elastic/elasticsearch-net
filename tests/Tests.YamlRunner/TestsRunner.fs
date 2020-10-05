// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Tests.YamlRunner

open System.Diagnostics
open Elastic.Transport
open ShellProgressBar
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader
open Tests.YamlRunner.OperationExecutor
open Tests.YamlRunner.Stashes
open Elasticsearch.Net
open Skips

type TestRunner(client:IElasticLowLevelClient, version: string, suite: TestSuite, progress:IProgressBar, barOptions:ProgressBarOptions) =
    
    member this.OperationExecutor = OperationExecutor(client)
    
    member private this.RunOperation file section operation nth stashes (subProgressBar:IProgressBar) = async {
        let executionContext = {
            Version = version
            Suite= suite
            File= file
            Folder= file.Directory
            Section= section
            NthOperation= nth
            Operation= operation
            Stashes = stashes
            Elapsed = ref 0L
        }
        let sw = Stopwatch.StartNew()
        try
            let! pass = this.OperationExecutor.Execute executionContext subProgressBar
            executionContext.Elapsed := sw.ElapsedMilliseconds
            match pass with
            | Failed f ->
                let c = pass.Context
                subProgressBar.WriteLine <| sprintf "%s %s %s: %s %s" pass.Name c.Folder.Name c.File.Name (operation.Log()) (f.Log())
            | _ -> ignore()
            return pass
        with
        | e ->
            subProgressBar.WriteLine <| sprintf "E! File: %s/%s Op: (%i) %s Section: %s " file.Directory.Name file.Name nth (operation.Log()) section 
            return Failed <| SeenException (executionContext, e)
    }
    
    member private this.CreateOperations m file (ops:Operations) subProgressBar = 
        let executedOperations =
            let stashes = Stashes()
            ops
            |> List.indexed
            |> List.map (fun (i, op) -> async {
                let! pass = this.RunOperation file m op i stashes subProgressBar
                return pass
            })
        (m, executedOperations)
        
    member private this.RunTestFile subProgressbar (file:YamlTestDocument) sectionFilter = async {
        let m section ops = this.CreateOperations section file.FileInfo ops subProgressbar
        let bootstrap section operations =
            let ops = operations |> Option.map (m section) |> Option.toList |> List.collect (fun (_, ops) -> ops)
            ops
        
        let setup =  bootstrap "Setup" file.Setup 
        let teardown = bootstrap "TEARDOWN" file.Teardown 
        let sections =
            file.Tests
            |> List.map (fun s -> s.Operations |> m s.Name)
            |> List.filter(fun s ->
                let (name, _) = s
                match sectionFilter with | Some s when s <> name -> false | _ -> true
            )
            |> List.collect (fun s ->
                let (name, ops) = s
                [(name, setup @ ops)]
            )
        
        let l = sections.Length
        let ops = sections |> List.sumBy (fun (_, i) -> i.Length)
        subProgressbar.MaxTicks <- ops
        
        let runSection progressHeader sectionHeader (ops: Async<ExecutionResult> list) = async {
            let l = ops |> List.length
            let result =
                ops
                |> List.indexed
                |> Seq.unfold (fun ms ->
                    match ms with
                    | (i, op) :: tl ->
                        let operations = sprintf "%s [%i/%i] operations: %s" progressHeader (i+1) l sectionHeader
                        subProgressbar.Tick(operations)
                        let r = Async.RunSynchronously op
                        match r with
                        | Succeeded _context -> Some (r, tl)
                        | NotSkipped _context -> Some (r, tl)
                        | Skipped (_context, _reason) ->
                            Some (r, [])
                        | Failed _context -> Some (r, [])
                    | [] -> None
                )
                |> List.ofSeq
            return sectionHeader, result
        }
        
        let runAllSections =
            sections
            |> Seq.indexed
            |> Seq.collect (fun (i, suite) ->
                let runTests () =
                    let run section =
                        let progressHeader = sprintf "[%i/%i] sections" (i+1) l
                        let (sectionHeader, ops) = section
                        runSection progressHeader sectionHeader ops;
                    [
                        // setup run as part of the suite, unfold will stop if setup fails or skips
                        run suite;
                        //always run teardown
                        run ("TEARDOWN", teardown)
                    ]
                let file =
                    let fi = file.FileInfo
                    let di = file.FileInfo.Directory
                    sprintf "%s/%s" di.Name fi.Name
                match SkipList.TryGetValue <| SkipFile(file) with
                | (true, s) ->
                    let (sectionHeader, _) = suite
                    match s with
                    | All -> []
                    | Section s when s = sectionHeader -> []
                    | Sections s when s |> List.exists (fun s -> s = sectionHeader) -> []
                    | _ -> runTests()
                | (false, _) -> runTests()
            )
            |> Seq.map Async.RunSynchronously
        
        return runAllSections |> Seq.toList
        
    }
    
    member this.GlobalSetup () =
        match suite with
        | Oss -> ignore()
        | XPack ->
            let data = PostData.String @"{""password"":""x-pack-test-password"", ""roles"":[""superuser""]}"
            let r = client.Security.PutUser<DynamicResponse>("x_pack_rest_user", data)
            let userCreated = r.Success 
            if (not userCreated) then failwithf "Global setup for %A failed\r\n%s" suite r.DebugInformation
            client.Indices.Refresh<VoidResponse>("_all") |> ignore
            

    member this.RunTestsInFolder mainMessage (folder:YamlTestFolder) sectionFilter = async {
        let l = folder.Files.Length
        let run (i, document) = async {
            let file = sprintf "%s/%s" document.FileInfo.Directory.Name document.FileInfo.Name
            let message = sprintf "%s [%i/%i] Files : %s" mainMessage (i+1) l file
            progress.Tick(message)
            let message = sprintf "Inspecting file for sections" 
            use p = progress.Spawn(0, message, barOptions)
            
            let! result = this.RunTestFile p document sectionFilter
            
            return document, result
        }
            
        let actions =
            folder.Files
            |> Seq.indexed 
            |> Seq.map run 
            |> Seq.map Async.RunSynchronously
        return actions
    }

