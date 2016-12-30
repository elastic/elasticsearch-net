#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net40"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"

#load @"Paths.fsx"
#load @"Tooling.fsx"

open System 
open Fake 
open FSharp.Data 

open Paths
open Projects
open Tooling

module Build =

    let private runningRelease = hasBuildParam "version" || hasBuildParam "apikey" || getBuildParam "target" = "canary" || getBuildParam "target" = "release"
    let private quickBuild = getBuildParam "target" = "quick" || getBuildParam "target" = "forever" 

    type private GlobalJson = JsonProvider<"../../global.json">
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version

    let private compileCore() =
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let runningSdkVersion = DotNetCli.getVersion()
        if (runningSdkVersion <> pinnedSdkVersion) then failwithf "Attempting to run with dotnet.exe with %s but global.json mandates %s" runningSdkVersion pinnedSdkVersion

        DotNetProject.AllPublishable
        |> Seq.iter(fun p -> 
            let path = Paths.ProjectJson p.Name
            let o = Paths.ProjectOutputFolder p DotNetFramework.NetStandard1_3
            DotNetCli.Restore 
              (fun p -> 
                   { p with 
                       Project = path
                       TimeOut = TimeSpan.FromMinutes(2.)
                    }
              ) |> ignore
                
            DotNetCli.Build
              (fun p -> 
                   { p with 
                       Configuration = "Release" 
                       Project = path
                       Framework = DotNetFramework.NetStandard1_3.Identifier.MSBuild
                       TimeOut = TimeSpan.FromMinutes(2.)
                       AdditionalArgs = ["-o"; o]
                    }
              ) |> ignore

        )

    let private compileDesktop() =
        Tooling.MsBuild.Rebuild DotNetFramework.Net45.Identifier
        Tooling.MsBuild.Rebuild DotNetFramework.Net46.Identifier

    let private gitLink() =
        DotNetProject.AllPublishable
        |> Seq.iter(fun p ->
            let projectName = (p.Name |> directoryInfo).Name
            let link framework = 
                Tooling.GitLink.Exec ["."; "-u"; Paths.Repository; "-d"; (Paths.ProjectOutputFolder p framework); "-include"; projectName] 
                |> ignore
            link DotNetFramework.Net45
            link DotNetFramework.Net46
            link DotNetFramework.NetStandard1_3
        )
        
    let Compile() = 
        match quickBuild with 
        | true ->  Tooling.MsBuild.Build DotNetFramework.Net45.Identifier
        | false ->  
            compileDesktop()
            compileCore()
            if not isMono && runningRelease then gitLink()

    let Clean() =
        match quickBuild with
        | true -> ignore() 
        | false ->
            CleanDir Paths.BuildOutput
            DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name)) 