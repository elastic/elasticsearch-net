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
    //let! x = Async.Sleep <| randomTime.Next(500, 900)
    return true
}

let RunOperations (progress:IProgressBar) (barOptions:ProgressBarOptions) m (ops:Operations) =
    let mutable seen = 0;
    let l = ops.Length
    let message = sprintf "%s [0/%i] operations" m l
    //progress.WriteLine <| message
    let actions =
        let p = progress.Spawn(l, message, barOptions)
        ops
        |> List.map (fun op -> async {
            let i = Interlocked.Increment (&seen)
            let message = sprintf "%s [%i/%i] operations" m i l
            p.Tick(message)
            let! pass = RunOperation progress m op
            
            let! x = Async.Sleep <| randomTime.Next(50, 200)
            return pass
        })
    
    actions
    

let RunTestFile (progress:IProgressBar) (barOptions:ProgressBarOptions) (file:YamlTestDocument) = async {
    let mutable seen = 0;
    let message = sprintf "Inspecting file for sections" 
    use p = progress.Spawn(0, message, barOptions)
    
    let message m = sprintf "%s: %s" m file.FileInfo.FullName
    let f a v = RunOperations p barOptions <| message a <| v
    let setup =  file.Setup |> Option.map (f "Setup") |> Option.toList //|> Option.map Async.RunSynchronously
    
    let passed = file.Tests |> List.map (fun s -> s.Operations |> f s.Name) //|> List.map Async.RunSynchronously 
    
    let teardown =  file.Teardown |> Option.map (f "Teardown") |> Option.toList //|> Option.map Async.RunSynchronously
    
    let suites = (setup @ passed @ teardown)
    //let combined  = suites |> List.concat
    
    let l = suites.Length
    p.MaxTicks <- l
    
    let actions =
        suites
        |> List.map (fun suite -> async {
            let! x = Async.Sleep <| randomTime.Next(50, 200)
            let i = Interlocked.Increment (&seen)
            let message = sprintf "[%i/%i] sections" i l
            p.Tick(message)
            let result = suite |> List.map Async.RunSynchronously
            return result
        })
        |> List.map Async.RunSynchronously
    
    return actions
    
}

let RunTestsInFolder (progress:IProgressBar) (barOptions:ProgressBarOptions) (folder:YamlTestFolder) = async {
    let mutable seen = 0;
    let l = folder.Files.Length
    let message = sprintf "Executing [%i/%i] files" seen l
    use p = progress.Spawn(l, message, barOptions)
    let run document = async {
        let i = Interlocked.Increment (&seen)
        let message = sprintf "Executing [%i/%i] files: %s" i l document.FileInfo.FullName
        p.Message <- message
        let! result = RunTestFile p barOptions document
        p.Tick()
        result
    }
        
    let actions =
        folder.Files
        |> List.map run 
        |> List.map Async.RunSynchronously
    return actions
}

