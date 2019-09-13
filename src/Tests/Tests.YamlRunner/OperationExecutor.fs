module Tests.YamlRunner.OperationExecutor

open Elasticsearch.Net
open System
open System.Collections.Generic
open System.IO
open System.Text
open System.Threading
open Tests.YamlRunner.DoMapper
open Tests.YamlRunner.Models
open Tests.YamlRunner.Stashes
open ShellProgressBar

type ExecutionContext = {
    Suite: TestSuite
    Folder: DirectoryInfo
    File: FileInfo
    Section: string
    NthOperation: int
    Operation: Operation
    Stashes: Stashes
}
    with member __.Throw message = failwithf "%s" message

type ExecutionResult =
| Succeeded of ExecutionContext
| Skipped of ExecutionContext 
| Failed of ExecutionContext 

let Execute (op:ExecutionContext) (progress:IProgressBar) = async {
    let stashes = op.Stashes
    match op.Operation with
    | Unknown u -> return Skipped op
    | Skip s -> return Skipped op
    | Do d -> 
        let (name, data) = d.ApiCall
        let found, lookup = DoMapper.DoMap.TryGetValue name
        if found then 
            try
                let invoke = lookup data
                let resolvedData = stashes.Resolve data
                let! r = Async.AwaitTask <| invoke.Invoke resolvedData
                
                op.Stashes.[StashedId.Body] <- r.Body
                op.Stashes.Response <- Some r
                
                //progress.WriteLine <| sprintf "%s %s" (r.ApiCall.HttpMethod.ToString()) (r.Uri.PathAndQuery)
                return Succeeded op
            with
            | ParamException e ->
                match d.Catch with
                | Some UnknownParameter -> return Succeeded op
                | _ ->
                    printfn "%s %O" e d.Catch
                    return Failed op
                
        else 
            printfn "%s: %b" name found
            return Failed op
        
    | Set s ->
        let v (prop:ResponseProperty) id =
            let (ResponseProperty prop) = prop
            match stashes.Response with
            | Some r ->
                let v = stashes.GetResponseValue prop
                stashes.[id] <- v
                progress.WriteLine <| sprintf "%A %s %O" id prop v
                Succeeded op
            | None ->
                progress.WriteLine <| sprintf "%A %s NOT FOUND" id prop
                failwithf "Attempted to look up %s but no response was set prior" prop
                Failed op
        
        let responses = s |> Map.map v 
        
        return Succeeded op
    | TransformAndSet ts -> return Skipped op
    | Assert a -> return Skipped op
}
