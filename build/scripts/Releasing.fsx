#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
#load @"Projects.fsx"
#load @"Versioning.fsx"
#load @"Building.fsx"
open System
open System.IO
open Fake 
open Paths
open Projects
open Versioning
open Building
open FSharp.Data

type Release() = 
    static let nugetPack = fun (projectName: ProjectName) ->
        let name = projectName.Nuget;
        CreateDir Paths.NugetOutput
        let package = (sprintf @"build\%s.nuspec" name)
        let packageContents = ReadFileAsString package
        let re = @"(?<start>\<version\>|""(Elasticsearch.Net|Nest)"" version="")[^""><]+(?<end>\<\/version\>|"")"
        let replacedContents = regex_replace re (sprintf "${start}%s${end}" Versioning.FileVersion) packageContents
        WriteStringToFile false package replacedContents
    
        let dir = sprintf "%s/%s/" Paths.BuildOutput name
        let nugetOutFile =  Paths.Output(sprintf "%s/%s.%s.nupkg" name name Versioning.FileVersion);
        NuGetPack (fun p ->
          {p with 
            Version = Versioning.FileVersion
            WorkingDir = "build" 
            OutputPath = dir
          })
          package
        traceFAKE "%s" dir
        MoveFile Paths.NugetOutput nugetOutFile

    static member PackAll() =
        DotNetProject.All
        |> Seq.map (fun p -> p.ProjectName)
        |> Seq.iter(fun p -> nugetPack p)

    static member PackAllDnx() =
        let projects = !! "src/Nest/project.json" 
                       ++ "src/Elasticsearch.Net/project.json"

        // update versions
        Versioning.PatchProjectJsons()

        // build nuget packages
        projects
        |> Seq.map DirectoryName
        |> Seq.iter(fun project -> 
            //even though this says desktop it still packs all the tfm's it just hints wich installed dnx version to use
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["pack"; (Paths.Quote project); "--configuration Release";])

        projects
        |> Seq.iter(fun project ->
            let projectName = (project |> DirectoryName |> directoryInfo).Name
            let srcFolder = Paths.BinFolder(projectName)
            let package = sprintf "%s/%s.%s.nupkg" srcFolder projectName Versioning.FileVersion

            // unzip package
            let unzippedDir = sprintf "%s/%s" srcFolder projectName
            ZipHelper.Unzip unzippedDir package
                        
            // rename NEST package id
            if (projectName.Equals("Nest", StringComparison.InvariantCultureIgnoreCase))
            then
                let nuspec = sprintf "%s/Nest.nuspec" unzippedDir
                FileHelper.RegexReplaceInFileWithEncoding 
                    "<id>Nest</id>" 
                    "<id>NEST</id>" 
                    System.Text.Encoding.UTF8 
                    nuspec

                // TODO: Make this more generic in limiting to major version based
                // on the major version of the version specified.
                FileHelper.RegexReplaceInFileWithEncoding 
                    "<dependency id=\"Newtonsoft.Json\" version=\".*\" />" 
                    "<dependency id=\"Newtonsoft.Json\" version=\"[8,9)\" />"  
                    System.Text.Encoding.UTF8 
                    nuspec

            // Include PDB for each target framework
            let frameworkDirs = (sprintf "%s/lib" unzippedDir |> directoryInfo).GetDirectories()
            for frameworkDir in frameworkDirs do
                let frameworkPdbDir = sprintf "%s/%s" srcFolder frameworkDir.Name
                gitLink frameworkPdbDir projectName
                let pdb = sprintf "%s.pdb" projectName
                let frameworkPdbFile = sprintf "%s/%s" frameworkPdbDir pdb
                if fileExists frameworkPdbFile
                then CopyFile (sprintf "%s/%s" frameworkDir.FullName pdb) frameworkPdbFile

            // re-zip package
            ZipHelper.Zip unzippedDir package !!(sprintf "%s/**/*.*" unzippedDir)
            DeleteDir unzippedDir

            if (directoryExists Paths.NugetOutput = false) then CreateDir Paths.NugetOutput

            // move to nuget output
            MoveFile Paths.NugetOutput package
        )

    static member PublishCanaryBuild accessKey feed = 
        !! "build/output/_packages/*-ci*.nupkg"
        |> Seq.iter(fun f -> 
            let source = "https://www.myget.org/F/"+ feed + "/api/v2/package"
            let success = Tooling.execProcess (Tooling.NugetFile()) ["push"; f; accessKey; "-source"; source] 
            match success with
            | 0 -> traceFAKE "publish to myget succeeded" |> ignore
            | _ -> failwith "publish to myget failed" |> ignore
        )

    static member PatchReleaseNotes() =
        !! "src/**/project.json"
        |> Seq.iter(fun f -> 
            RegexReplaceInFileWithEncoding 
                "\"releaseNotes\"\\s?:\\s?\".*\"" 
                (sprintf "\"releaseNotes\": \"See https://github.com/elastic/elasticsearch-net/releases/tag/%s\"" Versioning.FileVersion) 
                (new System.Text.UTF8Encoding(false)) f
        )