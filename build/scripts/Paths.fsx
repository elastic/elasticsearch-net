#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake

open System
open System.IO
open System.Diagnostics

module Paths =

    let BuildFolder = "build"

    let BuildOutput = sprintf "%s/output" BuildFolder
    let ToolsFolder = "packages/build"
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
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f

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

    type NugetTooling(nugetId, path) =
        member this.Path = sprintf "%s/%s" Paths.ToolsFolder path 
        member this.Exec arguments = 
            exec this.Path arguments

    let GitLink = new NugetTooling("GitLink", "gitlink/lib/net45/gitlink.exe")
    let Node = new NugetTooling("node.js", "Node.js/node.exe")
    let private npmCli = "Npm/node_modules/npm/cli.js"
    let Npm = new NugetTooling("npm", npmCli)
    let XUnit = new NugetTooling("xunit.runner.console", "xunit.runner.console/tools/xunit.console.exe")
    
    //only used to boostrap fake itself
    let Fake = new NugetTooling("FAKE", "FAKE/tools/FAKE.exe")
    let private FSharpData = new NugetTooling("FSharp.Data", "Fsharp.Data/lib/net40/Fsharp.Data.dll")

    let execProcess proc arguments =
        let args = arguments |> String.concat " "
        ExecProcess (fun p ->
            p.WorkingDirectory <- "."  
            p.FileName <- proc
            p.Arguments <- args
          ) 
          (TimeSpan.FromMinutes (5.0)) |> ignore

    type NpmTooling(npmId, binJs) =
        let modulePath =  sprintf "%s/node_modules/%s" Paths.ToolsFolder npmId
        let binPath =  sprintf "%s/%s" modulePath binJs
        let npm =  sprintf "%s/%s" Paths.ToolsFolder npmCli
        do
            if doesNotExist modulePath then
                traceFAKE "npm module %s not found installing in %s" npmId modulePath
                if (isMono) then
                    execProcess "npm" ["install"; npmId; "--prefix"; "./packages/build" ]
                else 
                Node.Exec [npm; "install"; npmId; "--prefix"; "./packages/build" ]
        member this.Path = binPath

        member this.Exec arguments =
                if (isMono) then
                    execProcess "node" <| binPath :: arguments
                else
                    exec Node.Path <| binPath :: arguments

    let Notifier = new NpmTooling("node-notifier", "bin.js")

