#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.IO.Compression.FileSystem.dll"

#load @"Projects.fsx"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake
open Projects


module Paths =

    let Repository = "https://github.com/elastic/elasticsearch-net"

    let BuildFolder = "build"
    let BuildOutput = sprintf "%s/output" BuildFolder

    let ProjectOutputFolder (project:DotNetProject) (framework:DotNetFramework) = 
        sprintf "%s/%s/%s" BuildOutput project.Name framework.Identifier.Nuget
  
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
    
    let ProjFile(project:DotNetProject) =
        match project with 
        | Project p -> sprintf "%s/%s/%s.csproj" SourceFolder project.Name project.Name
        | PrivateProject p ->
            match p with
            | Tests -> sprintf "%s/%s/%s.csproj" SourceFolder project.Name project.Name
            | DocGenerator -> sprintf "%s/CodeGeneration/%s/%s.csproj" SourceFolder project.Name project.Name
            

    let BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f
