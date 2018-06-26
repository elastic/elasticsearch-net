#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/Octokit/lib/net45"
#r @"FakeLib.dll"
#r "Octokit.dll"
#nowarn "0044" //TODO sort out FAKE 5

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Text
open System.Xml.Linq
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Fake
open Octokit

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
            let nuspec = (sprintf @"build/%s.nuspec" name)
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
        
    let GenerateNotes() = 
        let previousVersion = Versioning.GlobalJsonVersion.ToString()
        let currentVersion = Versioning.CurrentVersion.ToString()
        let label = sprintf "v%s" currentVersion
        let releaseNotes = sprintf "ReleaseNotes-%s.md" currentVersion |> Paths.Output      
        let client = new GitHubClient(new ProductHeaderValue("ReleaseNotesGenerator"))
        client.Credentials <- Credentials.Anonymous
              
        let filter = new RepositoryIssueRequest()
        filter.Labels.Add label
        filter.State <- ItemStateFilter.Closed
        
        let labelHeaders =
           [("Feature", "Features & Enhancements");
            ("Bug", "Bug Fixes");
            ("Deprecation", "Deprecations");
            ("Uncategorized", "Uncategorized");]
           |> Map.ofList
           
        let groupByLabel (issues:IReadOnlyList<Issue>) =
            let dict = new Dictionary<string, Issue list>()     
            for issue in issues do
                let mutable categorized = false
                for labelHeader in labelHeaders do
                    if issue.Labels.Any(fun l -> l.Name = labelHeader.Key) then
                        let exists,list = dict.TryGetValue(labelHeader.Key)
                        match exists with 
                        | true -> dict.[labelHeader.Key] <- issue :: list
                        | false -> dict.Add(labelHeader.Key, [issue])
                        categorized <- true
                        
                if (categorized = false) then
                    let label = "Uncategorized"
                    let exists,list = dict.TryGetValue(label)
                    match exists with 
                    | true ->                  
                        match List.tryFind(fun (i:Issue)-> i.Number = issue.Number) list with 
                        | Some _ -> ()
                        | None -> dict.[label] <- issue :: list                         
                    | false -> dict.Add(label, [issue])
            dict
        
        let closedIssues = client.Issue.GetAllForRepository(Paths.OwnerName, Paths.RepositoryName, filter)
                           |> Async.AwaitTask
                           |> Async.RunSynchronously
                           |> groupByLabel
                              
        use file = File.OpenWrite <| releaseNotes
        use writer = new StreamWriter(file)                   
        writer.WriteLine(sprintf "%s/compare/%s...%s" Paths.Repository previousVersion currentVersion)
        writer.WriteLine()
        for closedIssue in closedIssues do
            labelHeaders.[closedIssue.Key] |> sprintf "## %s" |> writer.WriteLine    
            writer.WriteLine()
            for issue in closedIssue.Value do
                sprintf "- #%i %s" issue.Number issue.Title |> writer.WriteLine
            writer.WriteLine()
              
        sprintf "### [View the full list of issues and PRs](%s/issues?utf8=%%E2%%9C%%93&q=label%%3A%s)" Paths.Repository label
        |> writer.WriteLine
        
                           
                      
        
        
