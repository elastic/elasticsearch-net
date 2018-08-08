#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#nowarn "0044" //TODO sort out FAKE 5

open System
open Fake

//this is ugly but a direct port of what used to be duplicated in our DOS and bash scripts

let private usage = """
USAGE:

build <target> [params] [skiptests]

Targets:

* build-all
  - default target if non provided. Performs rebuild and tests all TFM's
* clean
  - cleans build output folders
* test [testfilter]
  - incremental build and unit test for .NET 4.5, [testfilter] allows you to do a contains match on the tests to be run.
* release <version>
  - 0 create a release worthy nuget packages for [version] under build\output
* integrate <elasticsearch_versions> [clustername] [testfilter] 
  - run integration tests for <elasticsearch_versions> which is a semicolon separated list of
    elasticsearch versions to test or `latest`. Can filter tests by <clustername> and <testfilter>
* canary [apikey] [feed]
  - create a canary nuget package based on the current version if [feed] and [apikey] are provided
    also pushes to upstream (myget)
* diff <github|nuget|dir|assembly> <version|path 1> <version|path 2> [format]
* cluster <cluster-name> [version]
  - Start a cluster defined in Tests.Core or Tests from the command line and leaves it running
    untill a key is pressed. Handy if you want to run the integration tests numerous times while developing  
* benchmark [url] [username] [password] [non-interactive]
  - Runs a benchmark from Tests.Benchmarking and indexes the results to [url] when provided.
    If non-interactive runs all benchmarks without prompting

NOTE: both the `test` and `integrate` targets can be suffixed with `-all` to force the tests against all suported TFM's

Execution hints can be provided anywhere on the command line
- skiptests : skip running tests as part of the target chain
- skipdocs : skip generating documentation
- seed:<N> : provide a seed to run the tests with.
- random:<K><:B> : sets random K to bool B if if B is omitted will default to true
  K can be: sourceserializer, typedkeys or oldconnection (only valid on windows)
"""

module Commandline =
    type MultiTarget = All | One

    let private args = getBuildParamOrDefault "cmdline" "build" |> split ' '
    
    let skipTests = args |> List.exists (fun x -> x = "skiptests")
    let skipDocs = args |> List.exists (fun x -> x = "skipdocs") || isMono
    let seed = 
        match args |> List.tryFind (fun x -> x.StartsWith("seed:")) with
        | Some t -> t.Replace("seed:", "")
        | _ -> ""
        
    let randomArgs = 
        args 
        |> List.filter (fun x -> (x.StartsWith("random:")))
        |> List.map (fun x -> (x.Replace("random:", "")))
        
    let docsBranch = 
        match args |> List.tryFind (fun x -> x.StartsWith("docs:")) with
        | Some t -> t.Replace("docs:", "")
        | _ -> ""
             
    let private filteredArgs = 
        args 
        |> List.filter (
            fun x -> 
                x <> "skiptests" && 
                x <> "skipdocs" && 
                not (x.StartsWith("seed:")) && 
                not (x.StartsWith("random:")) && 
                not (x.StartsWith("docs:"))
        )

    let multiTarget =
        match (filteredArgs |> List.tryHead) with
        | Some t when t.EndsWith("-one") -> MultiTarget.One
        | _ -> MultiTarget.All

    let target =
        match (filteredArgs |> List.tryHead) with
        | Some t -> t.Replace("-one", "")
        | _ -> "build"

    let validMonoTarget =
        match target with
        | "release"
        | "canary" -> false
        | _ -> true
        
    let needsFullBuild =
        match (target, skipTests) with
        | (_, true) -> true
        //dotnet-xunit needs to a build of its own anyways
        | ("test", _)
        | ("cluster", _)
        | ("integrate", _) -> false
        | _ -> true
        
    let needsClean =
        match (target, skipTests) with
        | ("release", _) -> true
        //dotnet-xunit needs to a build of its own anyways
        | ("test", _)
        | ("cluster", _)
        | ("integrate", _) 
        | ("build", _) -> false
        | _ -> true

    let arguments =
        match filteredArgs with
        | _ :: tail -> target :: tail
        | [] -> [target]
    
    let private (|IsUrl|_|) (candidate:string) =
        match Uri.TryCreate(candidate, UriKind.RelativeOrAbsolute) with
        | true, _ -> Some candidate
        | _ -> None
        
    let private (|IsDiff|_|) (candidate:string) =
        let c = candidate |> toLower
        match c with
        | "github" | "nuget" | "directories" | "assemblies" -> Some c
        | _ -> failwith (sprintf "Unknown diff type: %s" candidate)
        
    let private (|IsProject|_|) (candidate:string) =
        let c = candidate |> toLower
        match c with
        | "nest" | "elasticsearch.net" | "nest.jsonnetserializer" -> Some c
        | _ -> None     
        
    let private (|IsFormat|_|) (candidate:string) =
        let c = candidate |> toLower
        match c with
        | "xml" | "markdown" | "asciidoc" -> Some c
        | _ -> None 

    let parse () =
        setEnvironVar "FAKEBUILD" "1"
        printfn "%A" arguments
        match arguments with
        | [] | ["build"] | ["test"] | ["clean"] | ["benchmark"] | ["profile"] -> ignore()
        | ["release"; version] -> setBuildParam "version" version

        | ["test"; testFilter] -> setBuildParam "testfilter" testFilter

        | ["benchmark"; IsUrl elasticsearch; username; password; "non-interactive"] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "1"
            setBuildParam "username" username
            setBuildParam "password" password
            
        | ["benchmark"; IsUrl elasticsearch; "non-interactive"] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "1"
            
        | ["benchmark"; "non-interactive"] ->
            setBuildParam "nonInteractive" "1"

        | ["benchmark"; IsUrl elasticsearch; username; password] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "0"
            setBuildParam "username" username
            setBuildParam "password" password

        | ["benchmark"; IsUrl elasticsearch] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "0"

        | ["profile"; IsUrl elasticsearch] ->
            setBuildParam "elasticsearch" elasticsearch

        | ["profile"; esVersions] -> 
            setBuildParam "esversions" esVersions

        | ["profile"; esVersions; testFilter] ->
            setBuildParam "esversions" esVersions
            setBuildParam "testfilter" testFilter        
          
        | ["integrate"; esVersions] -> setBuildParam "esversions" esVersions
        | ["integrate"; esVersions; clusterFilter] ->
            setBuildParam "esversions" esVersions
            setBuildParam "clusterfilter" clusterFilter
        | ["integrate"; esVersions; clusterFilter; testFilter] ->
            setBuildParam "esversions" esVersions
            setBuildParam "clusterfilter" clusterFilter
            setBuildParam "testfilter" testFilter

        | ["connectionreuse"; esVersions] ->
            setBuildParam "esversions" esVersions
            setBuildParam "clusterfilter" "ConnectionReuse"
        | ["connectionreuse"; esVersions; numberOfConnections] ->
            setBuildParam "esversions" esVersions
            setBuildParam "clusterfilter" "ConnectionReuse"
            setBuildParam "numberOfConnections" numberOfConnections
 
        | ["diff"; IsDiff diffType; IsProject project; firstVersionOrPath; secondVersionOrPath; IsFormat format] ->
             setBuildParam "diffType" diffType
             setBuildParam "project" project
             setBuildParam "first" firstVersionOrPath
             setBuildParam "second" secondVersionOrPath            
             setBuildParam "format" format            
        | ["diff"; IsDiff diffType; IsProject project; firstVersionOrPath; secondVersionOrPath] ->
             setBuildParam "diffType" diffType
             setBuildParam "project" project
             setBuildParam "first" firstVersionOrPath
             setBuildParam "second" secondVersionOrPath        
        | ["diff"; IsDiff diffType; firstVersionOrPath; secondVersionOrPath; IsFormat format] ->
             setBuildParam "diffType" diffType
             setBuildParam "first" firstVersionOrPath
             setBuildParam "second" secondVersionOrPath            
             setBuildParam "format" format          
        | ["diff"; IsDiff diffType; firstVersionOrPath; secondVersionOrPath] ->
            setBuildParam "diffType" diffType
            setBuildParam "first" firstVersionOrPath
            setBuildParam "second" secondVersionOrPath         
            
        | ["cluster"; clusterName] ->
            setBuildParam "clusterName" clusterName
            setBuildParam "clusterVersion" ""
        | ["cluster"; clusterName; clusterVersion] ->
            setBuildParam "clusterName" clusterName
            setBuildParam "clusterVersion" clusterVersion

        | ["touch"; ] -> ignore()
        | ["temp"; ] -> ignore()
        | ["canary"; ] -> ignore()
        | ["canary"; apiKey ] ->
            setBuildParam "apiKey" apiKey
            setBuildParam "feed" "elasticsearch-net"
        | ["canary"; apiKey; feed ] ->
            setBuildParam "apiKey" apiKey
            setBuildParam "feed" feed
        | _ ->
            traceError usage
            exit 2

        setBuildParam "target" (if target = "connectionreuse" then "integrate" else target)
        traceHeader target
