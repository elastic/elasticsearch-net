#I @"../tools/FAKE/tools"
#r @"FakeLib.dll"
open Fake

type PathInformation(buildFolder) =
    member this.BuildFolder = buildFolder
    member this.BuildOutput = sprintf "%s/output" buildFolder
    member this.ToolsFolder = sprintf "%s/tools" buildFolder
    member this.KeysFolder = sprintf "%s/keys" buildFolder
    member this.NugetOutput = sprintf "%s/_packages" this.BuildOutput
    member this.SourceFolder = "src"
    
    member this.Repository = "https://github.com/elasticsearch/elasticsearch-net"

    member this.MsBuildOutput() =
        !! "src/**/bin/**"
        |> Seq.map DirectoryName
        |> Seq.distinct
        |> Seq.filter (fun f -> (f.EndsWith("Debug") || f.StartsWith("Release")) && not (f.Contains "CodeGeneration")) 

    member this.Tool(tool) = sprintf "%s/%s" this.ToolsFolder tool
    member this.Keys(keyFile) = sprintf "%s/%s" this.KeysFolder keyFile
    member this.Output(folder) = sprintf "%s/%s" this.BuildOutput folder
    member this.Source(folder) = sprintf "%s/%s" this.SourceFolder folder
    member this.BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" this.SourceFolder f

let Paths = new PathInformation("build");