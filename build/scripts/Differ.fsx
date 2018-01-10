#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#r @"System.Xml.Linq"
#nowarn "0044" //TODO sort out FAKE 5

#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Projects.fsx"

open System
open System.IO

open Fake 
open System
open System.IO
open System.Linq
open System.Net
open System.Text
open System.Text.RegularExpressions
open System.Xml
open System.Xml.Linq
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
        | Command of command:string * args:string list * resolveAssemblies:(string -> string)
    
    // A github commit to diff
    type GitHubCommit = {
        /// The commit to diff against
        Commit: string;
        /// The compilation target.
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
        
    /// The two assemblies to diff
    type AssemblyDiff = {
         /// The path to the first assembly
         FirstPath: string;
         /// The path to the second assembly
         SecondPath: string;   
    }
    
    let private downloadNugetPackages nuget =
        let tempDir = nuget.TempDir </> "nuget"
        DeleteDir tempDir
        CreateDir tempDir
        let versions = [nuget.FirstVersion; nuget.SecondVersion] 
        
        versions
        |> Seq.map(fun v -> tempDir </> v)
        |> Seq.iter CreateDir
    
        let sources = 
            if List.isEmpty nuget.Sources then ["https://www.nuget.org/api/v2/";  "https://api.nuget.org/v3/index.json"]
            else nuget.Sources
            |> List.map (fun s -> sprintf "-Source %s" s)
            |> String.concat " "
    
        let packageVersionPath dir packageVersion =
                let desiredFrameworkVersion = Directory.GetDirectories dir
                                              |> Array.tryFind (fun f -> nuget.FrameworkVersion = Path.GetFileName f)
                match desiredFrameworkVersion with
                | Some f -> f |> Path.GetFullPath
                | _ -> failwith (sprintf "Nuget package %s, version %s, does not contain framework version %s in %s" 
                                          nuget.Package 
                                          packageVersion 
                                          nuget.FrameworkVersion
                                          dir)
    
        versions
        |> Seq.map(fun v -> 
            let workingDir = tempDir @@ v
            let exitCode =  Tooling.Nuget.ExecIn workingDir ["install"; nuget.Package; "-Version"; v; sources; "-ExcludeVersion -NonInteractive"]
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
                            ) (TimeSpan.FromMinutes 10.)
                            |> failIfError 
    
                            let buildOutputPath = f repo
                            CopyDir outputPath buildOutputPath (fun s -> true)
                            outputPath                          
    
            if isNullOrEmpty commit.OutputTarget then out
            else out @@ commit.OutputTarget
    
        [git.FirstCommit; git.SecondCommit] |> List.map checkoutAndBuild
        
    type DiffType = 
        | Deleted
        | Modified
        | New
        
    let private convertDiffType = function
        | "Deleted" -> Deleted
        | "Modified" -> Modified
        | "New" -> New
        | d -> failwithf "unknown diff type: %s" d
        
    let private attributeValue name (element:XElement) = 
        let attribute = element.Attribute(XName.op_Implicit name)
        if attribute <> null then attribute.Value else ""
        
    let private elements name (element:XContainer) = element.Elements(XName.op_Implicit name)
    
    let private descendents name (element:XContainer) = element.Descendants(XName.op_Implicit name)
    
    let private convertToMarkdown (path:string) first second =
        let name = path |> Path.GetFileNameWithoutExtension
        try
            let doc = XDocument.Load path
            let output = Path.ChangeExtension(path, "md")
            DeleteFile output
            use file = File.OpenWrite <| output
            use writer = new StreamWriter(file)                      
            writer.WriteLine(sprintf "# Breaking changes for %s between %s and %s" name first second)
            writer.WriteLine()
                    
            for element in (doc |> descendents "Type") do
                let typeName = element |> attributeValue "Name" |> replace (sprintf "%s." name) ""
                let diffType  = element |> attributeValue "DiffType" |> convertDiffType
                match diffType with
                | Deleted -> writer.WriteLine(sprintf "## `%s` is deleted" typeName)
                | New -> writer.WriteLine(sprintf "## `%s` is added" typeName)
                | Modified -> 
                    let members = Seq.append (element |> elements "Method") (element |> elements "Property")               
                    if Seq.isEmpty members |> not then          
                        writer.WriteLine(sprintf "## `%s`" typeName)
                        for m in members do
                            let memberName = m |> attributeValue "Name"
                            if isNotNullOrEmpty memberName then
                                let diffType  = m |> attributeValue "DiffType"                         
                                if isNotNullOrEmpty diffType then                    
                                    match convertDiffType diffType with
                                            | Deleted -> writer.WriteLine(sprintf "### `%s` is deleted" memberName)
                                            | New -> writer.WriteLine(sprintf "### `%s` is added" memberName)
                                            | Modified -> 
                                                match (m.Descendants(XName.op_Implicit "DiffItem") |> Seq.tryHead) with
                                                | Some diffItem ->
                                                    writer.WriteLine(sprintf "### `%s`" memberName)                                                                                      
                                                    let diffDescription = diffItem.Value
                                                    writer.WriteLine(Regex.Replace(diffDescription, "changed from (.*?) to (.*).", "changed from `$1` to `$2`."))                                         
                                                | None -> ()
        with
        | :? XmlException -> ignore()

    let private convertToAsciidoc path first second = 
        let name = path |> Path.GetFileNameWithoutExtension
        try
            let doc = XDocument.Load path
            let output = Path.ChangeExtension(path, "asciidoc")
            DeleteFile output
            use file = File.OpenWrite <| output
            use writer = new StreamWriter(file)                
            writer.WriteLine(name |> replace "." "-" |> sprintf "[[%s-breaking-changes]]")
            writer.WriteLine(sprintf "== Breaking changes for %s between %s and %s" name first second)
            writer.WriteLine()
                    
            for element in (doc |> descendents "Type") do
                let typeName = element |> attributeValue "Name" |> replace (sprintf "%s." name) ""
                let diffType  = element |> attributeValue "DiffType" |> convertDiffType
                match diffType with
                | Deleted -> writer.WriteLine(sprintf "[float]%s=== `%s` is deleted" Environment.NewLine typeName)
                | New -> writer.WriteLine(sprintf "[float]%s=== `%s` is added" Environment.NewLine typeName)
                | Modified -> 
                    let members = Seq.append (element |> elements "Method") (element |> elements "Property")        
                    if Seq.isEmpty members |> not then          
                        writer.WriteLine(sprintf "[float]%s=== `%s`" Environment.NewLine typeName)
                        for m in members do
                            let memberName = m |> attributeValue "Name"
                            if isNotNullOrEmpty memberName then
                                let diffType  = m |> attributeValue "DiffType"                         
                                if isNotNullOrEmpty diffType then                    
                                    match convertDiffType diffType with
                                            | Deleted -> writer.WriteLine(sprintf "[float]%s==== `%s` is deleted" Environment.NewLine memberName)
                                            | New -> writer.WriteLine(sprintf "[float]%s==== `%s` is added" Environment.NewLine memberName)
                                            | Modified -> 
                                                match (m.Descendants(XName.op_Implicit "DiffItem") |> Seq.tryHead) with
                                                | Some diffItem ->
                                                    writer.WriteLine(sprintf "[float]%s==== `%s`" Environment.NewLine memberName)                                                                                      
                                                    let diffDescription = diffItem.Value
                                                    writer.WriteLine(Regex.Replace(diffDescription, "changed from (.*?) to (.*).", "changed from `$1` to `$2`."))                                         
                                                | None -> ()
        with
        | :? XmlException -> ignore()
        
    /// Generates a diff between assemblies
    let Generate(diffType:string, project:string, first:string, second:string, format:string) =
        let tempDir = Path.GetTempPath() </> "nest-diff"
        
        let targetProject = 
            match project |> toLower with
            | "nest" -> (Project Nest).Name 
            | "nest.jsonnetserializer" -> (Project NestJsonNetSerializer).Name 
            | "elasticsearch.net" -> (Project ElasticsearchNet).Name 
            | _ -> ""
            
        let targetFormat =
            match format |> toLower with
            | "xml" -> Xml
            | "markdown" -> Markdown
            | "asciidoc" -> Asciidoc
            | _ -> Xml
        
        let diff = 
            match diffType with
            | "github" -> 
                let commit = {
                    Commit = ""
                    CompileTarget = Command(
                                            "build.bat", 
                                            ["skiptests"], 
                                            fun o -> 
                                                let outputDir = o @@ @"build\output\Nest\net46"
                                                if directoryExists outputDir && Directory.EnumerateFileSystemEntries(outputDir).Any() then outputDir
                                                else o @@ @"src\Nest\bin\Release\net46"
                                            )
                    OutputTarget = if isNotNullOrEmpty targetProject then sprintf "%s.dll" targetProject else targetProject
                }
                GitHub {
                    Url = new Uri(Paths.Repository)
                    TempDir = tempDir
                    FirstCommit = { commit with Commit = first }            
                    SecondCommit = { commit with Commit = second }            
                }
            | "nuget" ->
                Nuget {
                    Package = if isNullOrEmpty targetProject then "NEST" else targetProject
                    TempDir = tempDir
                    FirstVersion = first
                    SecondVersion = second
                    FrameworkVersion = "net46"
                    Sources = []        
                }
            | "directories" -> Directories { FirstDir = first; SecondDir = second }          
            | "assemblies" -> Assemblies { FirstPath = first; SecondPath = second }
            | d -> failwith (sprintf "Unknown diff type: %s" d)  
    
        let pairAssembliesInDirectories directories =
            let firstDirectory = directories |> Seq.head
            let lastDirectory = directories |> Seq.last
            [ for file in Directory.EnumerateFiles(firstDirectory, "*.dll") do
                let otherFile = lastDirectory </> Path.GetFileName file 
                if fileExists otherFile then yield { FirstPath = file; SecondPath = otherFile } ]

        /// returns a list of AssemblyDiff pairs
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
                
        for diff in assemblies do 
            let file = diff.FirstPath |> Path.GetFileNameWithoutExtension
            let outputFile = Paths.Output("Diffs") </> sprintf "%s.xml" file |> Path.GetFullPath
            let outputDir = outputFile |> Path.GetDirectoryName
            if directoryExists outputDir |> not then CreateDir outputDir      
            Tooling.JustAssembly.Exec [diff.FirstPath; diff.SecondPath; outputFile]
            match targetFormat with
            | Xml -> ()
            | Markdown -> convertToMarkdown outputFile first second
            | Asciidoc -> convertToAsciidoc outputFile first second
    
    
