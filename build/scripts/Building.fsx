#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net40"
#I @"../../packages/build/Mono.Cecil/lib/net40"
#r @"FakeLib.dll"
#r @"Mono.Cecil.dll"
#r @"FSharp.Data.dll"
#nowarn "0044" //TODO sort out FAKE 5

#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System 
open System.IO
open System.Reflection
open Fake 
open FSharp.Data 
open Mono.Cecil

open Paths
open Projects
open Tooling
open Versioning

module Build =

    let private runningRelease = hasBuildParam "version" || hasBuildParam "apikey" || getBuildParam "target" = "canary" || getBuildParam "target" = "release"

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
        tracefn "Cleaning known output folders"
        CleanDir Paths.BuildOutput
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) "clean src/Elasticsearch.sln -c Release" |> ignore
        DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))

    type CustomResolver(folder) = 
        inherit DefaultAssemblyResolver()
        member this.Folder = folder;

        override this.Resolve name = 
            try
                base.Resolve name
            with
            | ex -> 
                AssemblyDefinition.ReadAssembly(Path.Combine(folder, "Elasticsearch.Net.dll"));

    let RewriteNamespace nest f = 
        let folder = Paths.ProjectOutputFolder nest f
        let nestDll = sprintf "%s/%s.dll" folder nest.Name
        let nestRewrittenDll = sprintf "%s/%s-rewriten.dll" folder nest.Name
        use resolver = new CustomResolver(folder)
        let readerParams = ReaderParameters( AssemblyResolver = resolver, ReadWrite = true );
        use nestAssembly = AssemblyDefinition.ReadAssembly(nestDll, readerParams);
        

        for item in nestAssembly.MainModule.Types do
            item.Namespace <- 
                match item.Namespace.StartsWith("Newtonsoft.Json") with
                | false -> item.Namespace
                | true -> item.Namespace.Replace("Newtonsoft.Json", "Nest.Json")

        let key = File.ReadAllBytes(Paths.Keys("keypair.snk"))
        let kp = new StrongNameKeyPair(key)
        let wp = new WriterParameters ( StrongNameKeyPair = kp);
        nestAssembly.Write(wp) |> ignore;

    let ILRepack() = 
        for f in DotNetFramework.All do 
            let nest = Project Project.Nest
            let folder = Paths.ProjectOutputFolder nest f
            let nestDll = sprintf "%s/%s.dll" folder nest.Name
            let nestMergedDll = sprintf "%s/%s-merged.dll" folder nest.Name
            let jsonDll = sprintf "%s/Newtonsoft.Json.dll" folder
            let keyFile = Paths.Keys("keypair.snk");
            let options = 
                [ 
                    "/keyfile:", keyFile;
                    "/internalize", "";
                    "/lib:", folder;
                    "/out:", nestDll;
                ] 
                |> List.map (fun (p,v) -> sprintf "%s%s" p v)
            
            let args = [nestDll; jsonDll;] |> List.append options;
            
            Tooling.ILRepack.Exec args |> ignore
            RewriteNamespace nest f |> ignore
    