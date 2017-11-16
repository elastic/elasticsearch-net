#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.IO.Compression.FileSystem.dll"
#nowarn "0044" //TODO sort out FAKE 5

open System
open System.IO
open System.Diagnostics
open System.Net

#load @"Paths.fsx"

open Fake

Fake.ProcessHelper.redirectOutputToTrace <-true
    
module Tooling = 
    open Paths
    open Projects

    (* helper functions *)
    #if mono_posix
    #r "Mono.Posix.dll"
    open Mono.Unix.Native
    let private applyExecutionPermissionUnix path =
        let _,stat = Syscall.lstat(path)
        Syscall.chmod(path, FilePermissions.S_IXUSR ||| stat.st_mode) |> ignore
    #else
    let private applyExecutionPermissionUnix path = ()
    #endif

    let private execAt (workingDir:string) (exePath:string) (args:string seq) =
        let processStart (psi:ProcessStartInfo) =
            let ps = Process.Start(psi)
            ps.WaitForExit ()
            ps.ExitCode
        let fullExePath = exePath |> Path.GetFullPath
        applyExecutionPermissionUnix fullExePath
        let exitCode = 
            ProcessStartInfo(
                        fullExePath,
                        args |> String.concat " ",
                        WorkingDirectory = (workingDir |> Path.GetFullPath),
                        UseShellExecute = false) 
                   |> processStart
        if exitCode <> 0 then
            exit exitCode
        ()


    let execProcessWithTimeout proc arguments timeout workingDir = 
        let args = arguments |> String.concat " "
        ExecProcess (fun info ->
            info.FileName <- proc
            info.WorkingDirectory <- workingDir
            info.Arguments <- args
        ) timeout

    let execProcessWithTimeoutAndReturnMessages proc arguments timeout = 
        let args = arguments |> String.concat " "
        let code = 
            ExecProcessAndReturnMessages (fun info ->
            info.FileName <- proc
            info.WorkingDirectory <- "."
            info.Arguments <- args
            ) timeout
        code

    let private defaultTimeout = TimeSpan.FromMinutes 20.0

    let execProcessInDirectory proc arguments workingDir =
        let exitCode = execProcessWithTimeout proc arguments defaultTimeout workingDir
        match exitCode with
        | 0 -> exitCode
        | _ -> failwithf "Calling %s resulted in unexpected exitCode %i" proc exitCode 

    let execProcess proc arguments = execProcessInDirectory proc arguments "."

    let execProcessAndReturnMessages proc arguments =
        execProcessWithTimeoutAndReturnMessages proc arguments defaultTimeout

    let nugetFile =
        let targetLocation = "build/tools/nuget/nuget.exe" 
        if (not (File.Exists targetLocation))
        then
            trace (sprintf "Nuget not found at %s. Downloading now" targetLocation)
            let url = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe" 
            Directory.CreateDirectory("build/tools/nuget") |> ignore
            use webClient = new WebClient()
            webClient.DownloadFile(url, targetLocation)
            trace "nuget downloaded"
        targetLocation 

    type BuildTooling(path) =
        member this.Path = path
        member this.Exec arguments = execProcess this.Path arguments
        member this.ExecIn workingDirectory arguments = execProcessInDirectory this.Path arguments workingDirectory

    let Nuget = new BuildTooling(nugetFile)
    let ILRepack = new BuildTooling("packages/build/ILRepack/tools/ILRepack.exe")

    type DotTraceTool = {
        Name:string;
        Download:string;
        TargetDir:string;
    }

    let jetBrainsTools = [{ 
                            DotTraceTool.Name = "JetBrains DotTrace Self-Profile API";
                            Download = "https://download-cf.jetbrains.com/resharper/JetBrains.Profiler.SelfSdk.2016.3.2.zip";
                            TargetDir = "dottrace-selfprofile";
                         };
                         { 
                            DotTraceTool.Name = "JetBrains DotTrace Commandline Tools";
                            Download = "https://download-cf.jetbrains.com/resharper/JetBrains.dotTrace.CommandLineTools.2016.3.20170126.121657.zip";
                            TargetDir = "dottrace-commandline";
                         }]

    jetBrainsTools
    |> Seq.iter(fun t -> 
        let toolName = Path.GetFileNameWithoutExtension t.Download
        let buildToolsDirectory = Paths.Build("tools")
        let targetDir = sprintf "%s/%s" buildToolsDirectory t.TargetDir
        
        if (not (Directory.Exists targetDir)) then
            tracefn "No %s found in %s. Downloading now" t.Name buildToolsDirectory
            let zipFile = sprintf "%s/%s.zip" buildToolsDirectory toolName
            use webClient = new WebClient()
            webClient.DownloadFile(t.Download, zipFile)
            System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, targetDir)
            File.Delete zipFile
            tracefn "%s downloaded" t.Name
    )

    type ProfilerTooling(path) =
        let commandLineTool = Paths.CheckedInTool((jetBrainsTools.Item 1).TargetDir)
        let toolPath = commandLineTool @@ path
        member this.Exec arguments = execAt Environment.CurrentDirectory toolPath arguments

    let DotTraceProfiler = new ProfilerTooling("ConsoleProfiler.exe")
    let DotTraceReporter = new ProfilerTooling("Reporter.exe")
    let DotTraceSnapshotStats = new ProfilerTooling("SnapshotStat.exe")

    type DotNetTooling(exe) =
       member this.Exec arguments =
            this.ExecWithTimeout arguments (TimeSpan.FromMinutes 30.)

        member this.ExecWithTimeout arguments timeout =
            let result = execProcessWithTimeout exe arguments timeout "."
            if result <> 0 then failwith (sprintf "Failed to run dotnet tooling for %s args: %A" exe arguments)

    let DotNet = DotNetTooling("dotnet.exe")