#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System
open System.IO
open System.Linq
open System.Text
open System.Xml.Linq
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Fake

open Paths
open Projects
open Tooling
open Versioning

module Release = 
    let NugetPack() = 
        DotNetProject.AllPublishable
        |> Seq.iter(fun p ->
            CreateDir Paths.NugetOutput

            let name = p.Name;
            let nuspec = (sprintf @"build\%s.nuspec" name)
            let nugetOutFile =  Paths.Output(sprintf "%s.%s.nupkg" name (Versioning.CurrentVersion.ToString()))

            let nextMajorVersion = 
                let nextMajor = Versioning.CurrentVersion.Major + 1
                sprintf "%i" nextMajor

            let year = sprintf "%i" DateTime.UtcNow.Year

            let jsonDotNetCurrentVersion = 
                let xName n = XName.op_Implicit n
                use stream = File.OpenRead <| Paths.ProjFile p
                let doc = XDocument.Load(stream)
                let packageReference = 
                    doc.Descendants(xName "PackageReference")
                       .FirstOrDefault(fun e -> e.Attribute(xName "Include").Value = "Newtonsoft.Json")
                if (packageReference <> null) then packageReference.Attribute(xName "Version").Value
                else String.Empty
                
            let jsonDotNetNextVersion = 
                match jsonDotNetCurrentVersion with
                | "" -> String.Empty
                | version -> 
                    let semanticVersion = SemVerHelper.parse version
                    sprintf "%i" (semanticVersion.Major + 1) 

            let properties =
                let addKeyValue (e:Expr<string>) (builder:StringBuilder) =
                    // the binding for this tuple looks like key/value should 
                    // be round the other way (but it's correct as is)...
                    let (value,key) = 
                        match e with
                        | PropertyGet (eo, pi, li) -> (pi.Name, (pi.GetValue(e) |> string))
                        | ValueWithName (obj,ty,nm) -> ((obj |> string), nm)
                        | _ -> failwith (sprintf "%A is not a let-bound value. %A" e (e.GetType()))
                        
                    if (isNotNullOrEmpty value) then builder.AppendFormat("{0}=\"{1}\";", key, value)
                    else builder
                new StringBuilder()
                |> addKeyValue <@nextMajorVersion@>
                |> addKeyValue <@year@>
                |> addKeyValue <@jsonDotNetCurrentVersion@>
                |> addKeyValue <@jsonDotNetNextVersion@>
                |> toText
            
            Tooling.Nuget.Exec [ "pack"; nuspec; 
                                 "-version"; Versioning.CurrentVersion.ToString(); 
                                 "-outputdirectory"; Paths.BuildOutput; 
                                 "-properties"; properties; 
                               ] |> ignore
            traceFAKE "%s" Paths.BuildOutput
            MoveFile Paths.NugetOutput nugetOutFile
        )

    let PublishCanaryBuild accessKey feed = 
        !! "build/output/_packages/*-ci*.nupkg"
        |> Seq.iter(fun f -> 
            let source = "https://www.myget.org/F/" + feed + "/api/v2/package"
            let success = Tooling.Nuget.Exec ["push"; f; accessKey; "-source"; source] 
            match success with
            | 0 -> traceFAKE "publish to myget succeeded" |> ignore
            | _ -> failwith "publish to myget failed" |> ignore
        )
