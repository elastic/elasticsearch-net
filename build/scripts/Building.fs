namespace Scripts

open System 
open System.IO
open Fake

open FSharp.Data

open Paths
open Projects
open Tooling
open Versioning

module Build =

    let private runningRelease = hasBuildParam "version" || hasBuildParam "apikey" || getBuildParam "target" = "canary" || getBuildParam "target" = "release"

    type private GlobalJson = JsonProvider<"../../global.json", InferTypesFromValues=false>
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version
    if isMono then setProcessEnvironVar "TRAVIS" "true"
    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS" 

    let private sln = "src/Elasticsearch.sln"
    
    let Restore() =
        DotNetCli.Restore
            (fun p -> 
                { p with 
                    Project = sln
                    TimeOut = TimeSpan.FromMinutes(5.)
                }
            ) |> ignore
        
    let Compile (ArtifactsVersion(version)) = 
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let sourceLink = if not isMono && runningRelease then "1" else ""
        let props = 
            [ 
                "CurrentVersion", (version.Full.ToString());
                "CurrentAssemblyVersion", (version.Assembly.ToString());
                "CurrentAssemblyFileVersion", (version.AssemblyFile.ToString());
                "DoSourceLink", sourceLink;
                "FakeBuild", "1";
                "OutputPathBaseDir", Path.GetFullPath Paths.BuildOutput;
            ] 
            |> List.map (fun (p,v) -> sprintf "%s=%s" p v)
            |> String.concat ";"
            |> sprintf "/property:%s"
        
        DotNetCli.Build
            (fun p -> 
                { p with 
                    Configuration = "Release" 
                    Project = sln
                    TimeOut = TimeSpan.FromMinutes(5.)
                    AdditionalArgs = [props]
                }
            ) |> ignore


    let Clean () =
        tracefn "Cleaning known output folders"
        CleanDir Paths.BuildOutput
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(5.) }) "clean src/Elasticsearch.sln -c Release" |> ignore
        DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))
        
         