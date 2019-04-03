namespace Scripts

open System.Collections.Generic
open System.Linq
open System.IO
open Octokit
open Versioning

module ReleaseNotes =

    let private generateNotes newVersion oldVersion =
        let label = sprintf "v%O" newVersion.Full
        let releaseNotes = sprintf "ReleaseNotes-%O.md" newVersion.Full |> Paths.Output      
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
        writer.WriteLine(sprintf "%s/compare/%O...%O" Paths.Repository oldVersion.Full newVersion.Full)
        writer.WriteLine()
        for closedIssue in closedIssues do
            labelHeaders.[closedIssue.Key] |> sprintf "## %s" |> writer.WriteLine    
            writer.WriteLine()
            for issue in closedIssue.Value do
                sprintf "- #%i %s" issue.Number issue.Title |> writer.WriteLine
            writer.WriteLine()
              
        sprintf "### [View the full list of issues and PRs](%s/issues?utf8=%%E2%%9C%%93&q=label%%3A%s)" Paths.Repository label
        |> writer.WriteLine
                
    let GenerateNotes version =
        match version with
        | NoChange _ -> failwith "Can not generate release notes if no new version was specified"
        | Update (newVersion, oldVersion) -> generateNotes newVersion oldVersion
        

