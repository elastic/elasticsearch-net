namespace Scripts

open Projects

module Paths =

    let OwnerName = "elastic"
    let RepositoryName = "elasticsearch-net"
    let Repository = sprintf "https://github.com/%s/%s" OwnerName RepositoryName

    let BuildFolder = "build"
    let TargetsFolder = "build/scripts"
    let BuildOutput = sprintf "%s/output" BuildFolder
    
    let ProjectOutputFolder (project:DotNetProject) (framework:DotNetFramework) = 
        sprintf "%s/%s/%s" BuildOutput project.Name framework.Identifier.Nuget
  
    let Tool tool = sprintf "packages/build/%s" tool
    let CheckedInToolsFolder = "build/tools"
    let KeysFolder = sprintf "%s/keys" BuildFolder
    let NugetOutput = sprintf "%s/_packages" BuildOutput
    let SourceFolder = "src"
    
    let Solution = "src/Elasticsearch.sln"
    
    let CheckedInTool(tool) = sprintf "%s/%s" CheckedInToolsFolder tool
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let TestsSource(folder) = sprintf "%s/Tests/%s" SourceFolder folder
    
    let ProjFile(project:DotNetProject) =
        match project with 
        | Project p -> 
            match p with 
            | NestJsonNetSerializer -> sprintf "%s/Auxiliary/%s/%s.csproj" SourceFolder project.Name project.Name
            | ElasticsearchNetVirtual -> sprintf "%s/Auxiliary/%s/%s.csproj" SourceFolder project.Name project.Name
            | _ -> sprintf "%s/%s/%s.csproj" SourceFolder project.Name project.Name
        | PrivateProject p ->
            match p with
            | Tests -> sprintf "%s/Tests/%s/%s.csproj" SourceFolder project.Name project.Name
            | RestSpecTestRunner -> sprintf "%s/Tests/%s/%s.csproj" SourceFolder project.Name project.Name
            | DocGenerator -> sprintf "%s/CodeGeneration/%s/%s.csproj" SourceFolder project.Name project.Name
            | ApiGenerator -> sprintf "%s/CodeGeneration/%s/%s.csproj" SourceFolder project.Name project.Name

    let BinFolder (folder:string) = 
        let f = folder.Replace(@"\", "/")
        sprintf "%s/%s/bin/Release" SourceFolder f
