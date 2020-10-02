// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.AsyncExtensions

open System.Runtime.CompilerServices
open Microsoft.FSharp.Control
open System.Threading.Tasks
open System.Collections.Generic
open System.Runtime.ExceptionServices

[<AutoOpen>]
[<Extension>]
module AsyncExtensions =             
    type Async with   
    
    // Wrote this as Async.Parallel eagerly materializes and forcefully executes in order.
    // There is an extension that came in as dependency that extends Async.Parallel with maxDegreeOfParallelism
    // but for some reason this did not behave as I had expected
    [<Extension>]
    static member inline ForEachAsync (maxDegreeOfParallelism: int) asyncs = async {
        let tasks = List<Task<'a>>(maxDegreeOfParallelism)
        let results = List<'a>()
        for async in asyncs do
            let! x = Async.StartChildAsTask async
            tasks.Add <| x
            if (tasks.Count >= maxDegreeOfParallelism) then
                let! task = Async.AwaitTask <| Task.WhenAny(tasks)
                if (task.IsFaulted) then ExceptionDispatchInfo.Capture(task.Exception).Throw();
                results.Add(task.Result)
                let _ = tasks.Remove <| task
                ignore()
                
        let! completed = Async.AwaitTask <| Task.WhenAll tasks
        for c in completed do results.Add c
        return results |> Seq.toList
    }
