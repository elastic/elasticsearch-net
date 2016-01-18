#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.IO.Compression.FileSystem.dll"
open Fake

open System
open System.Collections.Generic
open System.IO
open System.Diagnostics
open System.Net
open System.Linq

module Paths =

    let BuildFolder = "build"

    let BuildOutput = sprintf "%s/output" BuildFolder
    let ToolsFolder = "packages/build"
    let CheckedInToolsFolder = "build/Tools"
    let KeysFolder = sprintf "%s/keys" BuildFolder
    let NugetOutput = sprintf "%s/_packages" BuildOutput
    let SourceFolder = "src"
    
    let Repository = "https://github.com/elasticsearch/elasticsearch-net"

    let MsBuildOutput =
        !! "src/**/bin/**"
        |> Seq.map DirectoryName
        |> Seq.distinct
        |> Seq.filter (fun f -> (f.EndsWith("Debug") || f.StartsWith("Release")) && not (f.Contains "CodeGeneration")) 

    let Tool(tool) = sprintf "%s/%s" ToolsFolder tool
    let CheckedInTool(tool) = sprintf "%s/%s" CheckedInToolsFolder tool
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let Build(folder) = sprintf "%s/%s" BuildFolder folder
    let BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f
    let Quote(path) = sprintf "\"%s\"" path

module Tooling = 
    let private fileDoesNotExist path = path |> Path.GetFullPath |> File.Exists |> not
    let private dirDoesNotExist path = path |> Path.GetFullPath |> Directory.Exists |> not
    let private doesNotExist path = (fileDoesNotExist path) && (dirDoesNotExist path)

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

    let private exec = execAt Environment.CurrentDirectory

    let execProcessWithTimeout proc arguments timeout = 
        let args = arguments |> String.concat " "
        let code = 
            ExecProcessAndReturnMessages (fun info ->
                info.FileName <- proc
                info.WorkingDirectory <- "."
                info.Arguments <- args
            ) timeout
    
        code

    let execProcess proc arguments =
        let defaultTimeout = TimeSpan.FromMinutes (5.0)
        execProcessWithTimeout proc arguments defaultTimeout

    type NugetTooling(nugetId, path) =
        member this.Path = Paths.Tool(path)
        member this.Exec arguments = exec this.Path arguments

    let private dotTraceCommandLineTools = "JetBrains.dotTrace.CommandLineTools.10.0.20151114.191633"
    let private buildToolsDirectory = Paths.Build("tools")
    let private dotTraceDirectory = sprintf "%s/%s" buildToolsDirectory dotTraceCommandLineTools
    
    if (Directory.Exists(dotTraceDirectory) = false)
    then
        trace (sprintf "No JetBrains DotTrace tooling found in %s. Downloading now" buildToolsDirectory) 
        let url = sprintf "https://d1opms6zj7jotq.cloudfront.net/resharper/%s.zip" dotTraceCommandLineTools      
        let zipFile = sprintf "%s/%s.zip" buildToolsDirectory dotTraceCommandLineTools
        use webClient = new WebClient()
        webClient.DownloadFile(url, zipFile)
        System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, dotTraceDirectory)
        File.Delete zipFile
        trace "JetBrains DotTrace tooling downloaded"

    let nugetFile = "build/tools/nuget/nuget.exe"
    if (File.Exists(nugetFile) = false)
    then
        trace "Nuget not found %s. Downloading now"
        let url = "http://nuget.org/nuget.exe" 
        Directory.CreateDirectory("build/tools/nuget") |> ignore
        use webClient = new WebClient()
        webClient.DownloadFile(url, nugetFile)
        trace "nuget downloaded"

    type ProfilerTooling(path) =
        member this.Path = sprintf "%s/%s" dotTraceDirectory path
        member this.Exec arguments = exec this.Path arguments

    let GitLink = new NugetTooling("GitLink", "gitlink/lib/net45/gitlink.exe")
    let Node = new NugetTooling("node.js", "Node.js/node.exe")
    let private npmCli = "Npm/node_modules/npm/cli.js"
    let Npm = new NugetTooling("npm", npmCli)
    let XUnit = new NugetTooling("xunit.runner.console", "xunit.runner.console/tools/xunit.console.exe")
    let DotTraceProfiler = new ProfilerTooling("ConsoleProfiler.exe")
    let DotTraceReporter = new ProfilerTooling("Reporter.exe")           
    let DotTraceSnapshotStats = new ProfilerTooling("SnapshotStat.exe")

    //only used to boostrap fake itself
    let Fake = new NugetTooling("FAKE", "FAKE/tools/FAKE.exe")

    type NpmTooling(npmId, binJs) =
        let modulePath =  sprintf "%s/node_modules/%s" Paths.ToolsFolder npmId
        let binPath =  sprintf "%s/%s" modulePath binJs
        let npm =  sprintf "%s/%s" Paths.ToolsFolder npmCli
        do
            if doesNotExist modulePath then
                traceFAKE "npm module %s not found installing in %s" npmId modulePath
                if (isMono) then
                    execProcess "npm" ["install"; npmId; "--prefix"; "./packages/build" ] |> ignore
                else 
                Node.Exec [npm; "install"; npmId; "--prefix"; "./packages/build" ]
        member this.Path = binPath

        member this.Exec arguments =
                if (isMono) then
                    (execProcess "node" <| binPath :: arguments) |> ignore
                else
                    exec Node.Path <| binPath :: arguments

    let Notifier = new NpmTooling("node-notifier", "bin.js")

    let private userProfileDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)

    type DnvmVersion(str:string) =
        let p = str.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        let parts = 
            match p.Length with
            | 6 -> List.ofArray p
            | 5 -> 
                match p.[0] with
                | "*" -> (List.ofArray p) @ [""]
                | _ -> "" :: List.ofArray p
            | 4 -> "" :: (List.ofArray p) @ [""]
            | _ -> raise(BuildException("Invalid number of arguments to dnvm version: " + p.Length.ToString(), List.ofArray p)) 
 
        member this.Active = isNotNullOrEmpty parts.[0] 
        member this.Version = parts.[1]          
        member this.Runtime = parts.[2] 
        member this.Architecture = parts.[3] 
        member this.OperatingSystem = parts.[4] 
        member this.Alias = parts.[5]

        member this.Location = 
            sprintf "%s/.dnx/runtimes/dnx-%s-%s-%s.%s" 
                userProfileDir
                this.Runtime 
                this.OperatingSystem 
                this.Architecture 
                this.Version

    type DnvmTooling() = 
        let dnvm = sprintf "%s/.dnx/bin/dnvm.cmd" userProfileDir
        let mutable dnvmVersion = "[unknown]"

        member this.Exec arguments =
            execProcessWithTimeout dnvm arguments (TimeSpan.FromSeconds 30.)

        member this.UpdateSelf() =
            this.Exec ["update-self"] |> ignore

        member this.List() =
            this.Exec ["list"]

        member this.Active() = 
            let getActiveVersion () = 
                let result = this.List()
                match result.OK with
                | true ->
                    let activeDnvm = result.Messages
                                    |> Seq.skip 3
                                    |> Seq.filter isNotNullOrEmpty
                                    |> Seq.map DnvmVersion
                                    |> Seq.find (fun d -> d.Active = true)

                    dnvmVersion <- activeDnvm.Location
                    dnvmVersion
                | _ -> raise(BuildException("No dnvm versions found on the machine. Please install dnx", []))

            match dnvmVersion with
            | "[unknown]" -> getActiveVersion()
            | _ -> dnvmVersion

    let Dnvm = new DnvmTooling()

    type DnxTooling(exe) =
          member this.Exec failedF workingDirectory arguments =
            let dnvm = Dnvm.Active()
            let proc = (sprintf "%s/bin/%s" dnvm exe)
            let result = execProcessWithTimeout proc arguments (TimeSpan.FromMinutes 5.)
            if not result.OK then failedF result.Errors

    let Dnu = new DnxTooling("dnu.cmd")
    let Dnx = new DnxTooling("dnx.exe")

