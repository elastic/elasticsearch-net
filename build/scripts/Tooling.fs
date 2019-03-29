namespace Scripts

open System
open System.IO
open System.Net
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
        member this.ExecInWithTimeout workingDirectory arguments timeout = execInWithTimeout timeout (Some workingDirectory) this.Path arguments
        member this.ExecWithTimeout arguments timeout = execInWithTimeout timeout None this.Path arguments
        member this.ExecIn workingDirectory arguments = this.ExecInWithTimeout workingDirectory arguments timeout
        member this.Exec arguments = this.ExecWithTimeout arguments timeout

    let nugetFile =
        let targetLocation = "build/tools/nuget/nuget.exe" 
        if (not (File.Exists targetLocation))
        then
            printfn "Nuget not found at %s. Downloading now" targetLocation
            let url = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe" 
            Directory.CreateDirectory("build/tools/nuget") |> ignore
            use webClient = new WebClient()
            webClient.DownloadFile(url, targetLocation)
            printfn "nuget downloaded"
        targetLocation 

    let Nuget = BuildTooling(None, nugetFile)
    let ILRepack = BuildTooling(None, "packages/build/ILRepack/tools/ILRepack.exe")
    let DotNet = BuildTooling(Some <| TimeSpan.FromMinutes(5.), "dotnet")

    type DotTraceTool = {
        Name:string;
        Download:string;
        TargetDir:string;
    }

    type DiffTooling(exe) =       
        let installPath = @"C:\Program Files (x86)\Progress\JustAssembly\Libraries"  
        let downloadPage = "https://www.telerik.com/download-trial-file/v2/justassembly"  
        let toolPath = Path.Combine(installPath,exe)
        
        member this.Exec arguments =
            if (Directory.Exists installPath |> not) then
                failwith (sprintf "JustAssembly is not installed in the default location %s. Download and install from %s" installPath downloadPage)
        
            execInWithTimeout defaultTimeout (Some ".") toolPath arguments 
            
    let JustAssembly = DiffTooling("JustAssembly.CommandLineTool.exe")
    