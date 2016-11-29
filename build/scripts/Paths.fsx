#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.IO.Compression.FileSystem.dll"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake

module Projects = 
    type DotNetFrameworkIdentifier = { MSBuild: string; Nuget: string; DefineConstants: string; }

    type DotNetFramework = 
        | Net45 
        | Net46 
        | NetStandard1_3
        static member All = [Net45; Net46; NetStandard1_3] 
        member this.Identifier = 
            match this with
            | Net45 -> { MSBuild = "v4.5"; Nuget = "net45"; DefineConstants = "TRACE;NET45"; }
            | Net46 -> { MSBuild = "v4.6"; Nuget = "net46"; DefineConstants = "TRACE;NET46"; }
            | NetStandard1_3 -> { MSBuild = "netstandard1.3"; Nuget = "netstandard1.3"; DefineConstants = "TRACE;DOTNETCORE"; }

    type Project =
        | Nest
        | ElasticsearchNet

    type PrivateProject =
        | Tests

    type DotNetProject = 
        | Project of Project
        | PrivateProject of PrivateProject

        static member All = 
            seq [
                Project Project.ElasticsearchNet; 
                Project Project.Nest; 
                PrivateProject PrivateProject.Tests
            ]

        static member AllPublishable = seq [Project Project.ElasticsearchNet; Project Project.Nest;] 
        static member Tests = seq [PrivateProject PrivateProject.Tests;] 

        member this.Name =
            match this with
            | Project p ->
                match p with
                | Nest -> "Nest"
                | ElasticsearchNet -> "Elasticsearch.Net"
            | PrivateProject p ->
                match p with
                | Tests -> "Tests"
       
        static member TryFindName (name: string) =
            DotNetProject.All
            |> Seq.map(fun p -> p.Name)
            |> Seq.tryFind(fun p -> p.ToLowerInvariant() = name.ToLowerInvariant())


module Paths =
    open Projects

    let Repository = "https://github.com/elastic/elasticsearch-net"

    let BuildFolder = "build"
    let BuildOutput = sprintf "%s/output" BuildFolder


    let ProjectOutputFolder (project:DotNetProject) (framework:DotNetFramework) = 
        sprintf "%s/%s/%s" BuildOutput framework.Identifier.MSBuild project.Name

    let Tool tool = sprintf "packages/build/%s" tool
    let CheckedInToolsFolder = "build/Tools"
    let KeysFolder = sprintf "%s/keys" BuildFolder
    let NugetOutput = sprintf "%s/_packages" BuildOutput
    let SourceFolder = "src"
    

    let CheckedInTool(tool) = sprintf "%s/%s" CheckedInToolsFolder tool
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let Build(folder) = sprintf "%s/%s" BuildFolder folder

    let BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f

    let ProjectJson(projectName) =
        Source(sprintf "%s/project.json" projectName)

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


    let execProcessWithTimeout proc arguments timeout = 
        let args = arguments |> String.concat " "
        ExecProcess (fun info ->
            info.FileName <- proc
            info.WorkingDirectory <- "."
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

    let private defaultTimeout = TimeSpan.FromMinutes (if isLocalBuild then 5.0 else 15.0)

    let execProcess proc arguments =
        let exitCode = execProcessWithTimeout proc arguments defaultTimeout
        match exitCode with
        | 0 -> exitCode
        | _ -> failwithf "Calling %s resulted in unexpected exitCode %i" proc exitCode 


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
        member this.Exec arguments = execProcessWithTimeout this.Path arguments (TimeSpan.FromMinutes 30.)

    type ProfilerTooling(path) =
        let dotTraceCommandLineTools = "JetBrains.dotTrace.CommandLineTools.10.0.20151114.191633"
        let buildToolsDirectory = Paths.Build("tools")
        let dotTraceDirectory = sprintf "%s/%s" buildToolsDirectory dotTraceCommandLineTools
        member this.Bootstrap = fun _ ->
            if (not (Directory.Exists dotTraceDirectory)) then
                trace (sprintf "No JetBrains DotTrace tooling found in %s. Downloading now" buildToolsDirectory) 
                let url = sprintf "https://d1opms6zj7jotq.cloudfront.net/resharper/%s.zip" dotTraceCommandLineTools      
                let zipFile = sprintf "%s/%s.zip" buildToolsDirectory dotTraceCommandLineTools
                use webClient = new WebClient()
                webClient.DownloadFile(url, zipFile)
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, dotTraceDirectory)
                File.Delete zipFile
                trace "JetBrains DotTrace tooling downloaded"
            
        member this.Path = sprintf "%s/%s" dotTraceDirectory path
        member this.Exec arguments = 
            this.Bootstrap()
            execAt Environment.CurrentDirectory this.Path arguments

    let Nuget = new BuildTooling(nugetFile)
    let GitLink = new BuildTooling(Paths.Tool("gitlink/lib/net45/gitlink.exe"))
    let Node = new BuildTooling(Paths.Tool("Node.js/node.exe"))
    let Npm = new BuildTooling(Paths.Tool("Npm/node_modules/npm/cli.js"))
    let XUnit = new BuildTooling(Paths.Tool("xunit.runner.console/tools/xunit.console.exe"))
    let DotTraceProfiler = new ProfilerTooling("ConsoleProfiler.exe")
    let DotTraceReporter = new ProfilerTooling("Reporter.exe")
    let DotTraceSnapshotStats = new ProfilerTooling("SnapshotStat.exe")

    //only used to boostrap fake itself
    let Fake = new BuildTooling("FAKE/tools/FAKE.exe")

    type DotNetRuntime = | Desktop | Core | Both

    type DotNetTooling(exe) =
       member this.Exec arguments =
            this.ExecWithTimeout arguments (TimeSpan.FromMinutes 30.)

        member this.ExecWithTimeout arguments timeout =
            let result = execProcessWithTimeout exe arguments timeout
            if result <> 0 then failwith (sprintf "Failed to run dotnet tooling for %s args: %A" exe arguments)

    let DotNet = new DotNetTooling("dotnet.exe")

    type MsBuildTooling() =

        member this.Build(target, framework:Projects.DotNetFrameworkIdentifier) =
            let solution = Paths.Source "Elasticsearch.sln";
            let setParams defaults =
                { defaults with
                    Verbosity = Some(Quiet)
                    Targets = [target]
                    Properties =
                        [
                            "OutputPathBaseDir", Path.GetFullPath "build\\output"
                            "Optimize", "True"
                            "Configuration", "Release"
                            "TargetFrameworkVersion", framework.MSBuild
                            "DefineConstants", framework.DefineConstants
                        ]
                 }
        
            build setParams solution 

    let MsBuild = new MsBuildTooling()

