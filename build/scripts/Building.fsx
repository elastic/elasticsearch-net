#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net40"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"

#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System 
open Fake 
open FSharp.Data 

open Paths
open Projects
open Tooling
open Versioning

module Build =

    let private runningRelease = hasBuildParam "version" || hasBuildParam "apikey" || getBuildParam "target" = "canary" || getBuildParam "target" = "release"
    let private quickBuild = not (getBuildParam "target" = "release" || getBuildParam "target" = "canary")

    type private GlobalJson = JsonProvider<"../../global.json">
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version
    if isMono then setProcessEnvironVar "TRAVIS" "true"
    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS" 

    let private sln = sprintf "src/Elasticsearch%s.sln" (if buildingOnTravis then ".DotNetCoreOnly" else "")

    let private compileCore incremental =
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let runningSdkVersion = DotNetCli.getVersion()
        if (runningSdkVersion <> pinnedSdkVersion) then failwithf "Attempting to run with dotnet.exe with %s but global.json mandates %s" runningSdkVersion pinnedSdkVersion
        let incrementalFramework = DotNetFramework.Net45
        let sourceLink = if not incremental && not isMono && runningRelease then "1" else ""
        let props = 
            [ 
                "CurrentVersion", (Versioning.CurrentVersion.ToString());
                "CurrentAssemblyVersion", (Versioning.CurrentAssemblyVersion.ToString());
                "CurrentAssemblyFileVersion", (Versioning.CurrentAssemblyFileVersion.ToString());
                "DoSourceLink", sourceLink;
                "DotNetCoreOnly", if buildingOnTravis then "1" else "";
            ] 
            |> List.map (fun (p,v) -> sprintf "%s=%s" p v)
            |> String.concat ";"
            |> sprintf "/property:%s"
        
        DotNetCli.Build
            (fun p -> 
                { p with 
                    Configuration = "Release" 
                    Project = sln
                    TimeOut = TimeSpan.FromMinutes(3.)
                    AdditionalArgs = if incremental then ["-f"; incrementalFramework.Identifier.Nuget; props] else [props]
                }
            ) |> ignore

    let Restore() =
        DotNetCli.Restore
            (fun p -> 
                { p with 
                    Project = sln
                    TimeOut = TimeSpan.FromMinutes(3.)
                }
            ) |> ignore
        
    let Compile incremental = 
        compileCore incremental

    let Clean() =
        match (quickBuild, getBuildParam "target" = "clean") with
        | (false, _) 
        | (_, true) -> 
            tracefn "Cleaning known output folders"
            CleanDir Paths.BuildOutput
            DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) "clean src/Elasticsearch.sln -c Release" |> ignore
            DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))
        | (_, _) -> 
            tracefn "Skiping clean target only run when calling 'release', 'canary', 'clean' as targets directly"
