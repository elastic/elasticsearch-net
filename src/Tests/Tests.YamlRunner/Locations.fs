module Tests.YamlRunner.Locations
    let private rootListingUrl = "https://github.com/elastic/elasticsearch"
    
    
    type YamlTestType = OpenSource | XPack
    
    let private openSourceResourcePath = "rest-api-spec/src/main/resources"
    let private xpackResourcesPath = "x-pack/plugin/src/test/resources"
    
    let private opensourceTests branch = sprintf "%s/tree/%s/%s/rest-api-spec/test" rootListingUrl branch openSourceResourcePath
    let private xpackTests branch = sprintf "%s/tree/%s/%s/rest-api-spec/test" rootListingUrl branch xpackResourcesPath
    
    let TestGithubRootUrl testType branch =
        match testType with
        | OpenSource -> opensourceTests branch
        | XPack -> xpackTests branch
    
