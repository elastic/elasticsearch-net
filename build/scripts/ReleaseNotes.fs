namespace Scripts

open System.Collections.Generic
open System.Linq
open System.IO
open System.Text.RegularExpressions
open System.Text;
open Octokit
open Versioning

module ReleaseNotes =
    let issueNumberRegex(url: string) =
        let pattern = sprintf "\s(?:#|%sissues/)(?<num>\d+)" url
        Regex(pattern, RegexOptions.Multiline ||| RegexOptions.IgnoreCase ||| RegexOptions.CultureInvariant ||| RegexOptions.ExplicitCapture ||| RegexOptions.Compiled)
      
    type GitHubItem(issue: Issue, relatedIssues: int list) =  
        member val Issue = issue
        member val RelatedIssues = relatedIssues
        member this.Title =
            let builder = StringBuilder("#")
                              .Append(issue.Number)
                              .Append(" ")       
            if issue.PullRequest = null then
                builder.AppendFormat("[ISSUE] {0}", issue.Title)
            else
                builder.Append(issue.Title) |> ignore
                if relatedIssues.Length > 0 then
                    relatedIssues
                    |> List.map(fun i -> sprintf "#%i" i)
                    |> String.concat ", "
                    |> sprintf " (%s: %s)" (if relatedIssues.Length = 1 then "issue" else "issues")
                    |> builder.Append
                else builder
            |> ignore                  
            builder.ToString()
            
        member this.Labels = issue.Labels   
        member this.Number = issue.Number
        
    type Config =
        { labels: Map<string,string>
          uncategorized: string }

    let config = {
        labels = Map.ofList <| [
            ("Feature", "Features & Enhancements");
            ("Bug", "Bug Fixes");
            ("Deprecation", "Deprecations");
            ("Uncategorized", "Uncategorized")
        ]
        uncategorized = "Uncategorized"
    };
        
    let groupByLabel (config: Config) (items: List<GitHubItem>) =
        let dict = Dictionary<string, GitHubItem list>()     
        for item in items do
            let mutable categorized = false
            // if an item is categorized with multiple config labels, it'll appear multiple times, once under each label
            for label in config.labels do
                if item.Labels.Any(fun l -> l.Name = label.Key) then
                    let exists,list = dict.TryGetValue(label.Key)
                    match exists with 
                    | true -> dict.[label.Key] <- item :: list
                    | false -> dict.Add(label.Key, [item])
                    categorized <- true
                    
            if categorized = false then
                let exists,list = dict.TryGetValue(config.uncategorized)
                match exists with 
                | true ->                  
                    match List.tryFind(fun (i:GitHubItem)-> i.Number = item.Number) list with 
                    | Some _ -> ()
                    | None -> dict.[config.uncategorized] <- item :: list                         
                | false -> dict.Add(config.uncategorized, [item])
        dict
        
    let filterByPullRequests (issueNumberRegex: Regex) (issues:IReadOnlyList<Issue>): List<GitHubItem> =
        let extractRelatedIssues(issue: Issue) =
            let matches = issueNumberRegex.Matches(issue.Body)
            if matches.Count = 0 then list.Empty
            else         
                matches
                |> Seq.cast<Match>
                |> Seq.filter(fun m -> m.Success)
                |> Seq.map(fun m -> m.Groups.["num"].Value |> int)
                |> Seq.toList
        
        let collectedIssues = List<GitHubItem>()
        let items = List<GitHubItem>()
        
        for issue in issues do
            if issue.PullRequest <> null then
                let relatedIssues = extractRelatedIssues issue
                items.Add(GitHubItem(issue, relatedIssues))
            else
                collectedIssues.Add(GitHubItem(issue, list.Empty))
             
        // remove all issues that are referenced by pull requests            
        for pullRequest in items do
            for relatedIssue in pullRequest.RelatedIssues do
                collectedIssues.RemoveAll(fun i -> i.Issue.Number = relatedIssue) |> ignore
                
        // any remaining issues do not have an associated pull request, so add them
        items.AddRange(collectedIssues)       
        items
        
    let getClosedIssues(label: string, config: Config) =
        let issueNumberRegex = issueNumberRegex Paths.Repository  
        let filter = RepositoryIssueRequest()
        filter.Labels.Add label
        filter.State <- ItemStateFilter.Closed

        let client = GitHubClient(ProductHeaderValue("ReleaseNotesGenerator"))  
        client.Credentials <- Credentials.Anonymous

        client.Issue.GetAllForRepository(Paths.OwnerName, Paths.RepositoryName, filter)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> filterByPullRequests issueNumberRegex
        |> groupByLabel config
    
    let private generateNotes newVersion oldVersion =
        let label = sprintf "v%O" newVersion.Full
        let releaseNotes = sprintf "ReleaseNotes-%O.md" newVersion.Full |> Paths.Output
        
        let closedIssues = getClosedIssues(label, config)
                              
        use file = File.OpenWrite <| releaseNotes
        use writer = new StreamWriter(file)                   
        writer.WriteLine(sprintf "%scompare/%O...%O" Paths.Repository oldVersion.Full newVersion.Full)
        writer.WriteLine()
        for closedIssue in closedIssues do
            config.labels.[closedIssue.Key] |> sprintf "## %s" |> writer.WriteLine    
            writer.WriteLine()
            for issue in closedIssue.Value do
                sprintf "- %s" issue.Title |> writer.WriteLine
            writer.WriteLine()
              
        sprintf "### [View the full list of issues and PRs](%sissues?utf8=%%E2%%9C%%93&q=label%%3A%s)" Paths.Repository label
        |> writer.WriteLine
                
    let GenerateNotes version =
        match version with
        | NoChange _ -> failwith "Can not generate release notes if no new version was specified"
        | Update (newVersion, oldVersion) -> generateNotes newVersion oldVersion
        

