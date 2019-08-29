module Tests.YamlRunner.OperationExecutor
open System.IO
open Tests.YamlRunner.Models


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

let Execute (op:ExecutionContext) =
    match op.Operation with
    | Unknown u -> op.Throw <| sprintf "Unknown operation %s" u
    | Skip s -> ignore
    | Do d -> ignore
    | Set s -> ignore
    | TransformAndSet ts -> ignore
    | Assert a -> ignore
