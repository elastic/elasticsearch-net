// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System
open ProcNet
open ProcNet.Std

module Tooling = 

    type ExecResult = { ExitCode: int; Output: Std.LineOut seq;}
    
    let private defaultTimeout = TimeSpan.FromMinutes(5.)
    
    type NoopWriter () =
        interface IConsoleOutWriter with
            member self.Write (_: Exception) = ignore()
            member self.Write (_: ConsoleOut) = ignore()
    
    let private defaultConsoleWriter = Some <| (ConsoleOutColorWriter() :> IConsoleOutWriter)
    
    let readInWithTimeout timeout workinDir bin (writer: IConsoleOutWriter option) args = 
        let startArgs = StartArguments(bin, args |> List.toArray)
        if (Option.isSome workinDir) then
            startArgs.WorkingDirectory <- Option.defaultValue "" workinDir
        let result = Proc.Start(startArgs, timeout, Option.defaultValue<IConsoleOutWriter> (NoopWriter())  writer)
        
        if not result.Completed then failwithf "process failed to complete within %O: %s" timeout bin
        if not result.ExitCode.HasValue then failwithf "process yielded no exit code: %s" bin
        { ExitCode = result.ExitCode.Value; Output = seq result.ConsoleOut}
        
    let read bin args = readInWithTimeout defaultTimeout None bin defaultConsoleWriter args 
    let readQuiet bin args = readInWithTimeout defaultTimeout None bin None args
    
    let execInWithTimeout timeout workinDir bin args = 
        let startArgs = ExecArguments(bin, args |> List.toArray)
        if (Option.isSome workinDir) then
            startArgs.WorkingDirectory <- Option.defaultValue "" workinDir
        let result = Proc.Exec(startArgs, timeout)
        try
            if not result.HasValue || result.Value > 0 then
                failwithf "process returned %i: %s" result.Value bin
        with
        | :? ProcExecException as ex -> failwithf "%s" ex.Message

    let execIn workingDir bin args = execInWithTimeout defaultTimeout workingDir bin args
    
    let exec bin args = execIn None bin args
    

    type BuildTooling(timeout, path) =
        let timeout = match timeout with | Some t -> t | None -> defaultTimeout
        member this.Path = path
        member this.ReadQuietIn workingDirectory arguments =
            readInWithTimeout defaultTimeout (Some workingDirectory) this.Path None arguments
        member this.ReadInWithTimeout workingDirectory arguments timeout =
            readInWithTimeout timeout (Some workingDirectory) this.Path defaultConsoleWriter arguments
        member this.ExecInWithTimeout workingDirectory arguments timeout = execInWithTimeout timeout (Some workingDirectory) this.Path arguments
        member this.ExecWithTimeout arguments timeout = execInWithTimeout timeout None this.Path arguments
        member this.ExecIn workingDirectory arguments = this.ExecInWithTimeout workingDirectory arguments timeout
        member this.Exec arguments = this.ExecWithTimeout arguments timeout

    let DotNet = BuildTooling(None, "dotnet")

    