// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

module Paths =

    let OwnerName = "elastic"
    let RepositoryName = "elasticsearch-net"
    let Repository = sprintf "https://github.com/%s/%s/" OwnerName RepositoryName

    let BuildFolder = "build"
    let TargetsFolder = "build/scripts"
    
    let BuildOutput = sprintf "%s/output" BuildFolder
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    
    let InplaceBuildOutput project tfm = 
        sprintf "src/%s/bin/Release/%s" project tfm
    let InplaceTestOutput project tfm = 
        sprintf "tests/%s/bin/Release/%s" project tfm
    let MagicDocumentationFile  = 
        "src/Elasticsearch.Net/obj/Release/netstandard2.1/Elasticsearch.Net.csprojAssemblyReference.cache" 
  
    let Tool tool = sprintf "packages/build/%s" tool
    let CheckedInToolsFolder = "build/tools"
    let KeysFolder = sprintf "%s/keys" BuildFolder
    let NugetOutput = sprintf "%s" BuildOutput
    let SourceFolder = "src"
    
    let Solution = "Elasticsearch.sln"
    
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let TestsSource(folder) = sprintf "tests/%s"  folder
    
    let ProjFile project = sprintf "%s/%s/%s.csproj" SourceFolder project project
    let TestProjFile project = sprintf "tests/%s/%s.csproj" project project

    let BinFolder (folder:string) = 
        let f = folder.Replace(@"\", "/")
        sprintf "%s/%s/bin/Release" SourceFolder f
        
        
