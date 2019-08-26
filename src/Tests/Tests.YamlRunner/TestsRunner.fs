module Tests.YamlRunner.TestsRunner

open System
open System.Threading
open ShellProgressBar
open Tests.YamlRunner
open Tests.YamlRunner.AsyncExtensions
open Tests.YamlRunner.Models
open Tests.YamlRunner.TestsReader

let private randomTime = Random()
let RunOperation (progress:IProgressBar) m operation = async {
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    return true
}

let RunOperations (progress:IProgressBar) (barOptions:ProgressBarOptions) m (ops:Operations) =
    let mutable seen = 0;
    let l = ops.Length
    progress.WriteLine <| m
    use progress = progress.Spawn(l, sprintf "%s [0/%i] operations" m l, barOptions)
    let actions =
        ops
        |> List.map (fun op -> async {
            let i = Interlocked.Increment (&seen)
            let message = sprintf "%s [%i/%i] operations" m i l
            let! x = Async.Sleep <| randomTime.Next(500, 900)
            let! pass = RunOperation progress m op
            progress.Tick(message)
            return pass
        })
    
    let x = Async.Sequential actions
    x
    

let asyncIter f inp = async {
    match inp with
    | None -> return None
    | Some v ->
        let! x = f v
        return Some x
}
let RunTestFile (progress:IProgressBar) (subBarOptions:ProgressBarOptions) (file:YamlTestDocument) = async {
    let message m = sprintf "%s: %s" m file.FileInfo.Name
    let f a = RunOperations progress subBarOptions <| message a
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    let passed = file.Setup |> (asyncIter (f "setup"))
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    let passed = file.Tests |> Seq.map (fun s -> s.Operations |> f s.Name)
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    let passed = file.Teardown |> asyncIter (f "teardown")
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    progress.Tick();
    
}

let RunTestsInFolder (progress:IProgressBar) (subBarOptions:ProgressBarOptions) (folder:YamlTestFolder) = async {
    
    let! x = Async.Sleep <| randomTime.Next(500, 900)
    let actions = folder.Files |> List.map (fun f -> RunTestFile progress subBarOptions f)
    
    let! completed  = Async.ForEachAsync 2 actions
    return completed
}

