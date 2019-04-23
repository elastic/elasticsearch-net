namespace Scripts

open System
open System.IO
open Fake.Core
open Fake.IO
open Commandline

module Cluster =

    let Run args =
        let clusterName = Option.defaultValue "" <| match args.CommandArguments with | Cluster c -> Some c.Name | _ -> None
        let clusterVersion = Option.defaultValue "" <|match args.CommandArguments with | Cluster c -> c.Version | _ -> None
        
        let testsProjectDirectory = Path.Combine(Path.GetFullPath(Paths.Output("Tests.ClusterLauncher")), "netcoreapp2.1")
        let tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        
        let sourceDir = Paths.Source("Tests/Tests.Configuration");
        let defaultYaml = Path.Combine(sourceDir, "tests.default.yaml");
        let userYaml = Path.Combine(sourceDir, "tests.yaml");
        let e f = File.Exists f;
        match ((e userYaml), (e defaultYaml)) with
        | (true, _) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(userYaml))
        | (_, true) -> Environment.setEnvironVar "NEST_YAML_FILE" (Path.GetFullPath(defaultYaml))
        | _ -> failwithf "Expected to find a tests.default.yaml or tests.yaml in %s" sourceDir
        
        printfn "%s" testsProjectDirectory
        
        Shell.copyDir tempDir testsProjectDirectory (fun s -> true)
        
        let command = sprintf "%s %s" clusterName clusterVersion
        let timeout = TimeSpan.FromMinutes(120.)
        let dll = Path.Combine(tempDir, "Tests.ClusterLauncher.dll");
        Tooling.DotNet.ExecInWithTimeout tempDir [dll; command] timeout  |> ignore
        
        Shell.deleteDir tempDir
         
