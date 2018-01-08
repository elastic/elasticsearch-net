#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#nowarn "0044" //TODO sort out FAKE 5

#load @"Paths.fsx"
#load @"Tooling.fsx"

open System
open System.IO

open Fake 
open System
open System.IO
open System.Net
open System.Text
open Fake.Git.CommandHelper

open Paths
open Projects
open Tooling

module Differ = 
   
    /// The format of the output
    type Format =
        | Xml
        | Markdown
        | Asciidoc
    
    /// The github project compilation target. Determines how to compile the github commit
    type GitHubTarget =
        | Command of command:string * args:string list * resolveAssembly:(string -> string)
    
    // A github commit to diff
    type GitHubCommit = {
        /// The commit to diff against
        Commit: string;
        /// The compilation target. If not specified, will use the first *.sln found
        CompileTarget : GitHubTarget;
        /// The build output target. If not specified, a diff will be performed on all assemblies in the build output directories
        OutputTarget: string;
    }
    
    /// Diff the build output of two github commits
    type GitHub = {
        /// The github repository url
        Url: Uri;
        /// A temporary directory in which to diff the commits. If the directory already exists, it will use that
        TempDir: string;
        /// The first commit to diff against
        FirstCommit: GitHubCommit;
        /// The second commit to diff against
        SecondCommit: GitHubCommit;
    }
    
    /// Diff the assemblies in two nuget package versions
    type Nuget = {
        /// The nuget package id
        Package: string;
        /// A temporary directory in which to diff the packages. If the directory already exists, will be deleted first.
        TempDir: string;
        /// The first package version to diff against
        FirstVersion: string;
        /// The second package version to diff against
        SecondVersion: string;
        /// The framework version of the package
        FrameworkVersion: string;
        /// The nuget package sources. Defaults to nuget v2 and v3 feeds if empty
        Sources: string list;
    }
    
    /// Diff two different assemblies
    type Assemblies = {
        /// The path to the first assembly
        FirstPath: string;
        /// The path to the second assembly
        SecondPath: string;
    }
    
    /// Diff the assemblies in two different directories
    type Directories = {
        /// The path to the first directory
        FirstDir: string;
        /// The path to the second directory
        SecondDir: string;
    }
    
    /// The diff operation to perform
    type Diff =
        | GitHub of GitHub
        | Nuget of Nuget
        | Assemblies of Assemblies
        | Directories of Directories
        
    type AssemblyDiff = {
         /// The path to the first assembly
         FirstPath: string;
         /// The path to the second assembly
         SecondPath: string;   
    }
    
    let private tempDir = Path.GetTempPath() </> Path.GetRandomFileName()
    
    let private downloadNugetPackages nuget =
        let tempDir = nuget.TempDir </> "nuget"
        DeleteDir tempDir
        CreateDir tempDir
        let versions = [nuget.FirstVersion; nuget.SecondVersion] 
        versions
        |> Seq.map(fun v -> tempDir </> v)
        |> Seq.iter CreateDir
    
        let nugetExe = Tooling.Nuget
    
        let sources = 
            if List.isEmpty nuget.Sources then ["https://www.nuget.org/api/v2/";  "https://api.nuget.org/v3/index.json"]
            else nuget.Sources
            |> List.map (fun s -> sprintf "-Source %s" s)
            |> String.concat " "
    
        let packageVersionPath dir packageVersion =
                let desiredFrameworkVersion = Directory.GetDirectories dir
                                              |> Array.tryFind (fun f -> nuget.FrameworkVersion = Path.GetFileName f)
                match desiredFrameworkVersion with
                | Some f ->  f |> Path.GetFullPath
                | _ -> failwith (sprintf "Nuget package %s, version %s, does not contain framework version %s in %s" 
                                          nuget.Package 
                                          packageVersion 
                                          nuget.FrameworkVersion
                                          dir)
    
        versions
        |> Seq.map(fun v -> 
            let workingDir = tempDir @@ v
            let exitCode =  nugetExe.ExecIn workingDir ["install"; nuget.Package; "-Version"; v; sources; "-ExcludeVersion -NonInteractive"]
            if exitCode <> 0 then failwith (sprintf "Error downloading nuget package version: %s" v)
    
            // assumes DLLs are in the lib folder
            let packageDirs = Directory.GetDirectories workingDir
                              |> Array.filter (fun f -> nuget.Package <> Path.GetFileName f)
                              |> Array.map(fun f -> (f @@ "lib") |> Path.GetFullPath)
    
            let targetPath = packageVersionPath (workingDir @@ nuget.Package @@ "lib") v
    
            // targeting an individual assembly or the directory of assemblies
            let target = 
                let assemblyNamedAfterPackage = 
                    Directory.EnumerateFiles(targetPath, "*.dll")
                    |> Seq.tryPick (fun f -> 
                        let fileName = Path.GetFileNameWithoutExtension f
                        if String.Equals(fileName, nuget.Package, StringComparison.OrdinalIgnoreCase) 
                        then Some f  
                        else None)
                match assemblyNamedAfterPackage with
                | Some a -> a
                | _ -> targetPath
                
            // copy all dependent package assemblies into target dir
            for packageDir in packageDirs do
                let path = packageVersionPath packageDir v
                path |> Directory.GetFiles |> CopyFiles targetPath
    
            target
        )
        |> Seq.toList
    
    let private cloneAndBuildGitRepo (git:GitHub) =    
        let fullTempPath = git.TempDir |> Path.GetFullPath 
        let gitDir = fullTempPath </> "nest-diff"
        let repo = fullTempPath  @@ "github"
    
        if (directoryExists repo |> not) then  
            CreateDir repo 
            directRunGitCommandAndFail repo (sprintf "clone %s ." git.Url.AbsoluteUri)

        let checkoutAndBuild (commit:GitHubCommit) =
            directRunGitCommandAndFail repo "reset --hard"
            directRunGitCommandAndFail repo (sprintf "checkout %s" commit.Commit)
            let outputPath = fullTempPath @@ commit.Commit
    
            let out = match commit.CompileTarget with
                        | Command (c, a, f) ->
                            let failIfError exitCode = 
                                if exitCode > 0 then 
                                    let message = sprintf "Command %s failed" c
                                    traceError message
                                    failwith message
    
                            ExecProcess(fun p ->
                                p.WorkingDirectory <- repo
                                p.FileName <- c
                                p.Arguments <- String.concat " " a
                            ) (TimeSpan.FromMinutes 5.)
                            |> failIfError 
    
                            let buildOutputPath = f(repo)
                            CopyDir outputPath buildOutputPath (fun s -> true)
                            outputPath                          
    
            if isNullOrEmpty commit.OutputTarget then out
            else out @@ commit.OutputTarget
    
        [git.FirstCommit; git.SecondCommit] |> List.map checkoutAndBuild
        
    let private convertToMarkdown path = trace "TODO: Convert to markdown"    
        
    let private convertToAsciidoc path = trace "TODO: Convert to asciidoc"
        
    /// Generates a diff between assembly files, assembly directories, assemblies in nuget packages
    let Generate(diff: Diff, format:Format) =
        let pairAssembliesInDirectories directories =
            let firstDirectory = directories |> Seq.head
            let lastDirectory = directories |> Seq.last
            [ for file in Directory.EnumerateFiles firstDirectory do
                let otherFile = lastDirectory </> Path.GetFileName file 
                if fileExists otherFile then 
                    yield { FirstPath = file; SecondPath = otherFile } ]

        let assemblies = 
            match diff with 
            | GitHub g ->
                let checkouts = cloneAndBuildGitRepo g
                if Seq.forall (fun t -> Path.HasExtension t && Path.GetExtension t = ".dll") checkouts 
                then [{ FirstPath = checkouts.Head; SecondPath = List.last checkouts }]
                else pairAssembliesInDirectories checkouts
            | Nuget n -> 
                let packages = downloadNugetPackages n
                if Seq.forall (fun t -> Path.HasExtension t && Path.GetExtension t = ".dll") packages 
                then [{ FirstPath = packages.Head; SecondPath = List.last packages }]
                else pairAssembliesInDirectories packages
            | Assemblies a -> [{ FirstPath = a.FirstPath; SecondPath = a.SecondPath }]
            | Directories d -> pairAssembliesInDirectories [d.FirstDir; d.SecondDir]
                
        let appendIfNotNullOrEmpty v f b =
            if (String.IsNullOrEmpty v = false) then Printf.bprintf b f v 
            b    
    
        for diff in assemblies do 
            let file = diff.FirstPath |> Path.GetFileNameWithoutExtension
            let path = Paths.Output("diff") </> sprintf "%s.xml" file
            Tooling.JustAssembly.Exec [diff.FirstPath; diff.SecondPath; path]
            match format with
            | Xml -> ()
            | Markdown -> convertToMarkdown path
            | Asciidoc -> convertToAsciidoc path
    
    
