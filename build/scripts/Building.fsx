#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net45"
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

    type private GlobalJson = JsonProvider<"../../global.json", InferTypesFromValues=false>
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version
    if isMono then setProcessEnvironVar "TRAVIS" "true"
    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS" 

    let private sln = "src/Elasticsearch.sln"
    
    let private compileCore incremental =
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let runningSdkVersion = DotNetCli.getVersion()
        if (runningSdkVersion <> pinnedSdkVersion) then failwithf "Attempting to run with dotnet.exe with %s but global.json mandates %s" runningSdkVersion pinnedSdkVersion
        let incrementalFramework = DotNetFramework.NetStandard1_3
        let sourceLink = if not incremental && not isMono && runningRelease then "1" else ""
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

    let private rewriteNamespace nest f = 
        trace "Rewriting namespaces"
        let folder = Paths.ProjectOutputFolder nest f
        let nestDll = sprintf "%s/%s.dll" folder nest.Name
        let nestRewrittenDll = sprintf "%s/%s-rewriten.dll" folder nest.Name
        use resolver = new CustomResolver(folder)
        let readerParams = ReaderParameters( AssemblyResolver = resolver, ReadWrite = true );
        use nestAssembly = AssemblyDefinition.ReadAssembly(nestDll, readerParams);
        
        for item in nestAssembly.MainModule.Types do
            if item.Namespace.StartsWith("Newtonsoft.Json") then
                item.Namespace <- item.Namespace.Replace("Newtonsoft.Json", "Nest.Json")
                
        // Touch custom attribute arguments 
        // Cecil does not update the types referenced within these attributes automatically,
        // so enumerate them to ensure namespace renaming is reflected in these references.
        let touchAttributes (attributes:Mono.Collections.Generic.Collection<CustomAttribute>) = 
            for attr in attributes do
                if attr.HasConstructorArguments then
                    for constArg in attr.ConstructorArguments do
                        if constArg.Type.Name = "Type" then ignore()    

        // rewrite explicitly implemented interface definitions defined
        // in Newtonsoft.Json
        let rewriteName (method:IMemberDefinition) =
            if method.Name.Contains("Newtonsoft.Json") then
                method.Name <- method.Name.Replace("Newtonsoft.Json", "Nest.Json")
             
        // recurse through all types and nested types   
        let rec rewriteTypes (types:Mono.Collections.Generic.Collection<TypeDefinition>) =
            for t in types do
                touchAttributes t.CustomAttributes
                for prop in t.Properties do 
                    touchAttributes prop.CustomAttributes
                    rewriteName prop
                    if prop.GetMethod <> null then rewriteName prop.GetMethod
                    if prop.SetMethod <> null then rewriteName prop.SetMethod
                for method in t.Methods do 
                    touchAttributes method.CustomAttributes
                    rewriteName method
                    for over in method.Overrides do rewriteName method
                for field in t.Fields do touchAttributes field.CustomAttributes
                for interf in t.Interfaces do touchAttributes interf.CustomAttributes
                for event in t.Events do touchAttributes event.CustomAttributes
                if t.HasNestedTypes then rewriteTypes t.NestedTypes
                
        nestAssembly.MainModule.Types |> rewriteTypes
        
        let resources = nestAssembly.MainModule.Resources
        for i = resources.Count-1 downto 0 do
            let resource = resources.[i]
            // remove the Newtonsoft signing key
            if resource.Name = "Newtonsoft.Json.Dynamic.snk" then resources.Remove(resource) |> ignore

        let key = File.ReadAllBytes(Paths.Keys("keypair.snk"))
        let kp = StrongNameKeyPair(key)
        let wp = WriterParameters ( StrongNameKeyPair = kp);
        nestAssembly.Write(wp) |> ignore;
        trace "Finished rewriting namespaces"

    let private ilRepackInternal() =
        let fw = DotNetFramework.All
        for f in fw do 
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
            rewriteNamespace nest f |> ignore
    
    let ILRepack() = 
        //ilrepack on mono crashes pretty hard on my machine
        match isMono with
        | true -> ignore()
        | false -> ilRepackInternal()
         