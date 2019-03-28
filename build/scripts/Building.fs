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
    
    let private compileCore =
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let sourceLink = if not isMono && runningRelease then "1" else ""
        let props = 
            [ 
                "CurrentVersion", (Versioning.CurrentVersion.ToString());
                "CurrentAssemblyVersion", (Versioning.CurrentAssemblyVersion.ToString());
                "CurrentAssemblyFileVersion", (Versioning.CurrentAssemblyFileVersion.ToString());
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

    let Restore() =
        DotNetCli.Restore
            (fun p -> 
                { p with 
                    Project = sln
                    TimeOut = TimeSpan.FromMinutes(5.)
                }
            ) |> ignore
        
    let Compile() = compileCore 

    let Clean() =
        tracefn "Cleaning known output folders"
        CleanDir Paths.BuildOutput
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(5.) }) "clean src/Elasticsearch.sln -c Release" |> ignore
        DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))
        
    let private assemblyRewriter = Paths.PaketDotNetGlobalTool "assembly-rewriter" @"tools\netcoreapp2.1\any\assembly-rewriter.dll"
    let private keyFile = Paths.Keys "keypair.snk"
    let Rewrite version framework projects =
        let project = projects |> Seq.head
        let folder = Paths.ProjectOutputFolder project framework
        
        let dllFullPath name = sprintf "%s/%s.dll" folder name
        let outputName (p: DotNetProject) = match p.Name = project.Name with | true -> p.Name | _ -> p.InternalName 
        let fullOutput (p: DotNetProject) = dllFullPath (p.Versioned (outputName p) version)
        let dlls =
            projects
            |> Seq.map (fun p -> sprintf @"-i ""%s"" -o ""%s"" "  (dllFullPath p.Name) (fullOutput p))
            |> Seq.fold (+) " "
            
        let mergeCommand = sprintf @"%s %s" assemblyRewriter dlls
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) mergeCommand |> ignore
        
        let mergedOutFile = fullOutput project
        let ilMergeArgs = [
            "/internalize"; 
            (sprintf "/lib:%s" folder); 
            (sprintf "/keyfile:%s" keyFile); 
            (sprintf "/out:%s" mergedOutFile)
        ]
        let mergeDlls = 
            projects 
            |> Seq.filter (fun p -> p.Name = project.Name || not <| (DotNetProject.AllPublishable |> Seq.contains p)) 
            |> Seq.map fullOutput
        match project.NeedsMerge with 
        | true -> Tooling.ILRepack.Exec (ilMergeArgs |> Seq.append mergeDlls) |> ignore
        | _ -> Tooling.ILRepack.Exec (ilMergeArgs |> Seq.append [mergeDlls |> Seq.head]) |> ignore

    let private ilRepackInternal() =
        let fw = DotNetFramework.All
        let projects = DotNetProject.AllPublishable
        let currentMajor = sprintf "%i" <| Versioning.CurrentVersion.Major
        for f in fw do
            for p in projects do
                if p.VersionedMergeDependencies <> [] then Rewrite (Some currentMajor) f p.VersionedMergeDependencies
                if p.MergeDependencies <> [] then Rewrite None f p.MergeDependencies
    
    let ShadowDependencies() = 
        //ilrepack on mono crashes pretty hard on my machine
        match isMono with
        | true -> ignore()
        | false -> ilRepackInternal()
         