namespace Scripts

open System
open System.IO
open ProcNet
open Fake.IO.Globbing.Operators
open ProcNet.Std

module Tooling = 

    type ExecResult = { ExitCode: int option; Output: Std.LineOut seq;}
    
    let private defaultTimeout = TimeSpan.FromMinutes(5.)
    
    let readInWithTimeout timeout workinDir bin args = 
        let startArgs = StartArguments(bin, args |> List.toArray)
        if (Option.isSome workinDir) then
            startArgs.WorkingDirectory <- Option.defaultValue "" workinDir
        let result = Proc.Start(startArgs, timeout, ConsoleOutColorWriter())
        if not result.Completed then failwithf "process failed to complete within %O: %s" timeout bin
        let exitCode = match result.ExitCode.HasValue with | false -> None | true -> Some result.ExitCode.Value
        { ExitCode = exitCode; Output = seq result.ConsoleOut}
        
    let read bin args = readInWithTimeout defaultTimeout None bin args
    
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
        member this.ReadInWithTimeout workingDirectory arguments timeout = readInWithTimeout timeout (Some workingDirectory) this.Path arguments
        member this.ExecInWithTimeout workingDirectory arguments timeout = execInWithTimeout timeout (Some workingDirectory) this.Path arguments
        member this.ExecWithTimeout arguments timeout = execInWithTimeout timeout None this.Path arguments
        member this.ExecIn workingDirectory arguments = this.ExecInWithTimeout workingDirectory arguments timeout
        member this.Exec arguments = this.ExecWithTimeout arguments timeout

    let nugetFile = Path.GetFullPath "build/scripts/bin/Release/netcoreapp2.2/NuGet.exe" 
    let Nuget = BuildTooling(None, nugetFile)
    let ILRepack = BuildTooling(None, "build/scripts/bin/Release/netcoreapp2.2/ILRepack.exe")
    let DotNet = BuildTooling(Some <| TimeSpan.FromMinutes(5.), "dotnet")

    