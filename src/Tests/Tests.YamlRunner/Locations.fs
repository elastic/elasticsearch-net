module Tests.YamlRunner.Locations
    let private rootListingUrl = "https://github.com/elastic/elasticsearch"
    let private rootRawUrl = "https://raw.githubusercontent.com/elastic/elasticsearch"
    
    type TestSuite = OpenSource | XPack
    
    let private openSourceResourcePath = "rest-api-spec/src/main/resources"
    let private xpackResourcesPath = "x-pack/plugin/src/test/resources"
    
    let private Path namedSuite revision =
        let path = match namedSuite with | OpenSource -> openSourceResourcePath | XPack -> xpackResourcesPath
        sprintf "%s/%s/rest-api-spec/test" revision  path
        
    let TestGithubRootUrl namedSuite revision = sprintf "%s/tree/%s" rootListingUrl <| Path namedSuite revision
        
    let FolderListUrl namedSuite revision folder =
        let root = TestGithubRootUrl namedSuite revision
        sprintf "%s/%s" root folder
        
    let TestRawUrl namedSuite revision folder file =
        let path = Path namedSuite revision
        sprintf "%s/%s/%s/%s" rootRawUrl path folder file
    
