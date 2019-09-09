module Tests.YamlRunner.OperationExecutor
open System.IO
open Tests.YamlRunner.DoMapper
open Tests.YamlRunner.Models
open ShellProgressBar

type ExecutionContext = {
    Suite: TestSuite
    Folder: DirectoryInfo
    File: FileInfo
    Section: string
    NthOperation: int
    Operation: Operation
}
    with member __.Throw message = failwithf "%s" message

let Do executionContext = ignore()

let Execute (op:ExecutionContext) (progress:IProgressBar) = async {
    match op.Operation with
    | Unknown u -> op.Throw <| sprintf "Unknown operation %s" u
    | Skip s -> Async.Ignore
    | Do d -> 
        let (name, data) = d.ApiCall
        let found, lookup = DoMapper.DoMap.TryGetValue name
        if found then 
            try
                let invoke = lookup data
                let! r = Async.AwaitTask <| invoke.Invoke(data)
                progress.WriteLine <| sprintf "%s %s" (r.ApiCall.HttpMethod.ToString()) (r.Uri.PathAndQuery)
            with
            | ParamException e ->
                match d.Catch with
                | Some UnknownParameter -> printfn "success"
                | _ ->
                    printfn "%s %O" e d.Catch
                    //reraise() 
                
        else 
            printfn "%s: %b" name found
        
    | Set s -> Async.Ignore
    | TransformAndSet ts -> Async.Ignore
    | Assert a -> Async.Ignore
}
