module Tests.YamlRunner.OperationExecutor
open SharpYaml.Events
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

type ExecutionResult =
| Succeeded of ExecutionContext
| Skipped of ExecutionContext 
| Failed of ExecutionContext 

let Execute (op:ExecutionContext) (progress:IProgressBar) = async {
    match op.Operation with
    | Unknown u -> return Skipped op
    | Skip s -> return Skipped op
    | Do d -> 
        let (name, data) = d.ApiCall
        let found, lookup = DoMapper.DoMap.TryGetValue name
        if found then 
            try
                let invoke = lookup data
                let! r = Async.AwaitTask <| invoke.Invoke(data)
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
        
    | Set s -> return Skipped op
    | TransformAndSet ts -> return Skipped op
    | Assert a -> return Skipped op
}
