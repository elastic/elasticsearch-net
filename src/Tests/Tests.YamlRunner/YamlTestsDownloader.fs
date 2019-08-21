module Tests.YamlRunner.YamlTestsDownloader

open FSharp.Data

let ListFolders testType branch =
    let url = Locations.TestGithubRootUrl testType branch
    let doc = HtmlDocument.Load(url)
    
    doc.CssSelect("td.content a.js-navigation-open")
    |> List.map (fun a -> a.InnerText())
    |> List.filter (fun f -> not <| f.EndsWith(".asciidoc"))
    
let ListFolderFiles branch folder = async { 
    let url = Locations.TestGithubRootUrl testType branch
    let url = sprintf "https://github.com/elastic/elasticsearch/tree/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s" branch folder
    let! doc = HtmlDocument.AsyncLoad(url)
    let url file = sprintf "https://raw.githubusercontent.com/elastic/elasticsearch/%s/rest-api-spec/src/main/resources/rest-api-spec/test/%s/%s" branch folder file
    let yamlFiles =
        doc.CssSelect("td.content a.js-navigation-open")
        |> List.map(fun a -> a.InnerText())
        |> List.filter(fun f -> f.EndsWith(".yml"))
        |> List.map url
    return (folder, yamlFiles)
}


