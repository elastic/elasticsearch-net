// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System
open System.Runtime.InteropServices
open Fake.Core
open Fake.IO

//this is ugly but a direct port of what used to be duplicated in our DOS and bash scripts
module Commandline =

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
* canary 
  - create a canary nuget package based on the current version.
* cluster <cluster-name> [version]
  - Start a cluster defined in Tests.Core or Tests from the command line and leaves it running
    untill a key is pressed. Handy if you want to run the integration tests numerous times while developing  
* benchmark [non-interactive] [url] [username] [password] 
  - Runs a benchmark from Tests.Benchmarking and indexes the results to [url] when provided.
    If non-interactive runs all benchmarks without prompting
* codegen
  - runs the code generator interactively
* documentation
  - runs the doc generation, without running any tests
  
NOTE: both the `test` and `integrate` targets can be suffixed with `-all` to force the tests against all suported TFM's

Execution hints can be provided anywhere on the command line
- skiptests : skip running tests as part of the target chain
- gendocs : generate documentation
- non-interactive : make targets that run in interactive mode by default to run unassisted.
- docs:<B> : the branch name B to use when generating documentation
- ref:<B> : the reference version B to use when generating documentation
- seed:<N> : provide a seed to run the tests with.
- random:<K><:B> : sets random K to bool B if if B is omitted will default to true
  K can be: sourceserializer, typedkeys or oldconnection (only valid on windows)
"""

    let private (|IsUrl|_|) (candidate:string) =
        match Uri.TryCreate(candidate, UriKind.RelativeOrAbsolute) with
        | true, _ -> Some candidate
        | _ -> None
        
    let private (|IsDiff|_|) (candidate:string) =
        let c = candidate.ToLowerInvariant() 
        match c with
        | "github" | "nuget" | "directories" | "assemblies" -> Some c
        | _ -> failwith (sprintf "Unknown diff type: %s" candidate)
        
    let private (|IsProject|_|) (candidate:string) =
        let c = candidate.ToLowerInvariant()
        match c with
        | "nest" | "elasticsearch.net" | "nest.jsonnetserializer" -> Some c
        | _ -> None     
        
    let private (|IsFormat|_|) (candidate:string) =
        let c = candidate.ToLowerInvariant()
        match c with
        | "xml" | "markdown" | "asciidoc" -> Some c
        | _ -> None 

    type MultiTarget = All | One

    type VersionArguments = { Version: string; OutputLocation: string option }
    type TestArguments = { TrxExport: bool; CodeCoverage: bool; TestFilter: string option; }
    type IntegrationArguments = { TrxExport: bool; TestFilter: string option; ClusterFilter: string option; ElasticsearchVersions: string list; }

    type BenchmarkArguments = { Endpoint: string; Username: string option; Password: string option; }
    type ClusterArguments = { Name: string; Version: string option; }
    type CommandArguments =
        | Unknown
        | SetVersion of VersionArguments
        | Test of TestArguments
        | Integration of IntegrationArguments
        | Benchmark of BenchmarkArguments
        | Cluster of ClusterArguments

    type PassedArguments = {
        NonInteractive: bool;
        SkipTests: bool;
        GenDocs: bool;
        Seed: int;
        RandomArguments: string list;
        DocsBranch: string;
        ReferenceBranch: string;
        RemainingArguments: string list;
        MultiTarget: MultiTarget
        ReleaseBuild: bool;
        Target: string;
        CommandArguments: CommandArguments;
    }

    let notWindows =
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || 
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
        
    let private buildingOnAzurePipeline = Environment.environVarAsBool "TF_BUILD"
        
    let runningOnAzureDevops = Environment.hasEnvironVar "TF_BUILD" 
    let runningOnCi = runningOnAzureDevops || Environment.hasEnvironVar "APPVEYOR_BUILD_VERSION"
    
    let parse (args: string list) =
        
        let filteredArgs = 
            args
            |> List.filter(fun x -> 
               x <> "skiptests" && 
               x <> "gendocs" && 
               x <> "skipdocs" && 
               x <> "non-interactive" && 
               not (x.StartsWith("seed:")) && 
               not (x.StartsWith("random:")) && 
               not (x.StartsWith("docs:")) &&
               not (x.StartsWith("ref:")))
            |> List.map(fun (s:string) ->
                let containsSpace = s.Contains(" ")
                match s with | x when containsSpace -> sprintf "\"%s\"" x | s -> s
            )
            
        let target = 
            match (filteredArgs |> List.tryHead) with
            | Some t -> t.Replace("-one", "")
            | _ -> "build"
        let skipTests = args |> List.exists (fun x -> x = "skiptests") || target = "documentation"
        let skipDocs = args |> List.exists (fun x -> x = "skipdocs")

        let parsed = {
            NonInteractive = args |> List.exists (fun x -> x = "non-interactive")
            SkipTests = skipTests
            GenDocs = not skipDocs && (args |> List.exists (fun x -> x = "gendocs") || target = "build" || target = "documentation") 
            Seed = 
                match args |> List.tryFind (fun x -> x.StartsWith("seed:")) with
                | Some t -> Int32.Parse (t.Replace("seed:", ""))
                | _ -> Random().Next(1, 100_000)
            RandomArguments = 
                args 
                |> List.filter (fun x -> (x.StartsWith("random:")))
                |> List.map (fun x -> (x.Replace("random:", "")))
            DocsBranch = 
                match args |> List.tryFind (fun x -> x.StartsWith("docs:")) with
                | Some t -> t.Replace("docs:", "")
                | _ -> ""
            ReferenceBranch = 
                match args |> List.tryFind (fun x -> x.StartsWith("ref:")) with
                | Some t -> t.Replace("ref:", "")
                | _ -> ""
            RemainingArguments = filteredArgs
            MultiTarget = 
                match (filteredArgs |> List.tryHead) with
                | Some t when t.EndsWith("-one") -> MultiTarget.One
                | _ -> MultiTarget.All
            Target = 
                match (filteredArgs |> List.tryHead) with
                | Some t -> t.Replace("-one", "")
                | _ -> "build"
            ReleaseBuild = 
                match target with
                | "canary"
                | "release" -> true
                | _ -> false
            CommandArguments = Unknown
        }
            
        let arguments =
            match filteredArgs with
            | _ :: tail -> target :: tail
            | [] -> [target]
        
        let split (s:string) = s.Split ',' |> Array.toList 

        match arguments with
        | []
        | ["build"]
        | ["clean"]
        | ["benchmark"]
        | ["codegen"; ] 
        | ["documentation"; ] 
        | ["profile"] -> parsed
        | "rest-spec-tests" :: tail -> { parsed with RemainingArguments = tail }
        
        | ["release"; version] -> { parsed with CommandArguments = SetVersion { Version = version; OutputLocation = None }; }
        | ["release"; version; path] ->
            if (not <| System.IO.Directory.Exists path) then failwithf "'%s' is not an existing directory" (Path.getFullName path)
            { parsed with CommandArguments = SetVersion { Version = version; OutputLocation = Some path }; }
        | ["canary"] ->
            {
                parsed with CommandArguments = Test {
                        TestFilter = None
                        TrxExport = buildingOnAzurePipeline 
                        CodeCoverage = buildingOnAzurePipeline
                }
            }
        
        | ["test"] ->
            {
                parsed with CommandArguments = Test {
                        TestFilter = None
                        TrxExport = buildingOnAzurePipeline 
                        CodeCoverage = false
                }
            }
        | ["test"; testFilter] ->
            {
                parsed with CommandArguments = Test {
                        TestFilter = Some testFilter
                        TrxExport = buildingOnAzurePipeline 
                        CodeCoverage = false
                }
            }

        | ["benchmark"; IsUrl elasticsearch; username; password] ->
            {
                parsed with CommandArguments = Benchmark {
                        Endpoint = elasticsearch;
                        Username = Some username;
                        Password = Some password;
                }
            }
        | ["benchmark"; IsUrl elasticsearch] ->
            {
                parsed with CommandArguments = Benchmark {
                        Endpoint = elasticsearch;
                        Username = None
                        Password = None
                }
            }
          
        | ["integrate"; esVersions] -> 
            {
                parsed with CommandArguments = Integration {
                        TrxExport = buildingOnAzurePipeline
                        ElasticsearchVersions = split esVersions; ClusterFilter = None; TestFilter = None
                }
            }
        | ["integrate"; esVersions; clusterFilter] ->
            {
                parsed with CommandArguments = Integration {
                        TrxExport = buildingOnAzurePipeline
                        ElasticsearchVersions = split esVersions;
                        ClusterFilter = Some clusterFilter;
                        TestFilter = None
                }
            }
        | ["integrate"; esVersions; clusterFilter; testFilter] ->
            {
                parsed with CommandArguments = Integration {
                        TrxExport = buildingOnAzurePipeline
                        ElasticsearchVersions = split esVersions;
                        ClusterFilter = Some clusterFilter
                        TestFilter = Some testFilter
                }
            }
            
        | ["cluster"; clusterName] ->
            {
                parsed with CommandArguments = Cluster { Name = clusterName; Version = None }
            }
        | ["cluster"; clusterName; clusterVersion] ->
            {
                parsed with CommandArguments = Cluster { Name = clusterName; Version = Some clusterVersion }
            }
        | _ ->
            eprintf "%s" usage
            failwith "Please consult printed help text on how to call our build"
