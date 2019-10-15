module Tests.YamlRunner.AsyncExtensions

open System.Runtime.CompilerServices
open Microsoft.FSharp.Control
open System.Threading.Tasks
open System.Collections.Generic
open System.Runtime.ExceptionServices

[<AutoOpen>]
module AsyncExtensions =             
    type Microsoft.FSharp.Control.Async with   
    
    // Wrote this as Async.Parallel eagerly materializes and forcefully executes in order.
    // There is an extension that came in as dependency that extends Async.Parallel with maxDegreeOfParallelism
    // but for some reason this did not behave as I had expected
    [<Extension>]
    static member inline ForEachAsync (maxDegreeOfParallelism: int) asyncs = async {
        let tasks = new List<Task<'a>>(maxDegreeOfParallelism)
        let results = new List<'a>()
        for async in asyncs do
            let! x = Async.StartChildAsTask async
            tasks.Add <| x
            if (tasks.Count >= maxDegreeOfParallelism) then
                let! task = Async.AwaitTask <| Task.WhenAny(tasks)
                if (task.IsFaulted) then ExceptionDispatchInfo.Capture(task.Exception).Throw();
                results.Add(task.Result)
                let removed = tasks.Remove <| task
                ignore()
                
        let! completed = Async.AwaitTask <| Task.WhenAll tasks
        for c in completed do results.Add c
        return results |> Seq.toList
    }
    
    [<Extension>]
    static member inline WhenAny asyncs = async {
        let! t = 
            asyncs
            |> Seq.map Async.StartAsTask
            |> Task.WhenAny
            |> Async.AwaitTask

        return t.Result
    }


