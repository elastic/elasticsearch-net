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

NOTE: both the `test` and `integrate` targets can be suffixed with `-all` to force the tests against all suported TFM's

Execution hints can be provided anywhere on the command line
- skiptests : skip running tests as part of the target chain
- source_serialization : force tests to use a client with custom source serialization
- seed:<N> : provide a seed to run the tests with.
"""

module Commandline =
    type MultiTarget = All | One

    let private args = getBuildParamOrDefault "cmdline" "build" |> split ' '
    
    let skipTests = args |> List.exists (fun x -> x = "skiptests")
    let forceSourceSerialization = args |> List.exists (fun x -> x = "source_serialization")
    let seed = 
        match args |> List.tryFind (fun x -> x.StartsWith("seed:")) with
        | Some t -> t.Replace("seed:", "")
        | _ -> ""
        
    let private filteredArgs = 
        args 
        |> List.filter (
            fun x -> 
                x <> "skiptests" && x <> "source_serialization" && not (x.StartsWith("seed:"))
        )

    let multiTarget =
        match (filteredArgs |> List.tryHead) with
        | Some t when t.EndsWith("-all") -> MultiTarget.All
        | _ -> MultiTarget.One

    let target =
        match (filteredArgs |> List.tryHead) with
        | Some t -> t.Replace("-all", "")
        | _ -> "build"

    let needsFullBuild =
        match (target, skipTests) with
        | (_, true) -> true
        //dotnet-xunit needs to a build of its own anyways
        | ("test", _)
        | ("integrate", _) -> false
        | _ -> true
        
    let needsClean =
        match (target, skipTests) with
        | (_, true) -> true
        //dotnet-xunit needs to a build of its own anyways
        | ("test", _)
        | ("integrate", _) -> false
        | _ -> true

    let arguments =
        match filteredArgs with
        | _ :: tail -> target :: tail
        | [] -> [target]
    
    let private (|IsUrl|_|) (candidate:string) =
        match Uri.TryCreate(candidate, UriKind.RelativeOrAbsolute) with
        | true, _ -> Some candidate
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

        | ["benchmark"; IsUrl elasticsearch; username; password] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "0"
            setBuildParam "username" username
            setBuildParam "password" password

        | ["benchmark"; IsUrl elasticsearch] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "0"

        | ["benchmark"; IsUrl elasticsearch; "non-interactive"] ->
            setBuildParam "elasticsearch" elasticsearch
            setBuildParam "nonInteractive" "1"
            
        | ["benchmark"; "non-interactive"] ->
            setBuildParam "nonInteractive" "1"

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
