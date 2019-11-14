namespace Scripts

open System.Collections.Generic
open System.IO
open System.Threading
open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open NuGet.Common
open NuGet.Protocol.Core.Types
open NuGet.Protocol
open NuGet.Configuration
open NuGet.Versioning
open Commandline
open Projects

module Differ =    
    let private nestJsonNetSerializerNugetId = (Project NestJsonNetSerializer).NugetId
    
    let private netStandard20Identifier = DotNetFramework.NetStandard2_0.Identifier.Nuget
    
    let private findPreviousVersionOnNuget (version:string) =         
        let nugetVersion = new NuGetVersion(version)        
        let providers = new List<Lazy<INuGetResourceProvider>>();
        providers.AddRange(Repository.Provider.GetCoreV3());
        let packageSource = new PackageSource("https://api.nuget.org/v3/index.json")
        let sourceRepository = new SourceRepository(packageSource, providers)      
        let metadata = sourceRepository.GetResource<PackageMetadataResource>()
        let searchMetadata =
            metadata.GetMetadataAsync(nestJsonNetSerializerNugetId, false, false, NullLogger.Instance, CancellationToken.None)
            |> Async.AwaitTask
            |> Async.RunSynchronously
            
        let previousPackage = searchMetadata
                              |> Seq.cast<PackageSearchMetadata>
                              |> Seq.sortByDescending (fun p -> p.Version)
                              |> Seq.filter (fun p -> p.Version < nugetVersion)
                              |> Seq.tryHead
                           
        match previousPackage with
        | Some p -> Some(p.Version.ToFullString())
        | None -> None
   
    let private unzipReleasePackages () =
        !! (Fake.IO.Path.combine Paths.NugetOutput "*.nupkg")
        |> Seq.iter(fun f ->
            let name = Path.GetFileNameWithoutExtension(f)
            let directory = Path.Combine(Paths.NugetOutput, name)
            Zip.unzip directory f
        )
        
        let tmp = Fake.IO.Path.combine Paths.NugetOutput "tmp"     
        if Directory.Exists tmp then Directory.delete tmp
        Directory.create tmp
                
        Directory.EnumerateDirectories(Paths.NugetOutput)
        |> Seq.filter (fun d -> d <> tmp)
        |> Seq.iter(fun d ->
            let netstandard20Dlls = System.IO.Path.Combine(d, "lib", netStandard20Identifier)
            Directory.EnumerateFiles(netstandard20Dlls, "*.dll") |> Shell.copyFiles tmp
        )
        
        tmp |> Path.GetFullPath
              
    let Run args =
       Tooling.DotNet.Exec ["tool"; "restore"]       
       let differ = "assembly-differ"
       let args = args.RemainingArguments |> String.concat " "
       let command = sprintf @"%s %s -o ../../%s" differ args Paths.BuildOutput
       Environment.setEnvironVar "NUGET" Tooling.nugetFile
       Tooling.DotNet.ExecIn Paths.TargetsFolder [command] |> ignore
       
    let DiffWithPreviousNugetVersion args =       
        match args.CommandArguments with
        | SetVersion v ->
            let previousVersion = findPreviousVersionOnNuget v.Version           
            match previousVersion with
            | Some p ->           
                let directory = unzipReleasePackages ()             
                let command = [ sprintf "nuget|%s|%s|%s" nestJsonNetSerializerNugetId p netStandard20Identifier;
                                sprintf "directory|%s" directory ]
                               
                System.Console.WriteLine("Running diff of {0}, {1} from Nuget against local {2}", netStandard20Identifier, p, v.Version)             
                Run { args with RemainingArguments = command }
            | None -> failwithf "Could not find previous version on Nuget for version %s" v.Version
        | _ -> failwith "DiffWithPreviousNugetVersion can only be run with SetVersion arguments"
