namespace Scripts

open System
open System.IO
open Fake.IO
open Commandline

module ReposTooling =

    let LaunchCluster args =
        let clusterName = Option.defaultValue "" <| match args.CommandArguments with | Cluster c -> Some c.Name | _ -> None
        let clusterVersion = Option.defaultValue "" <|match args.CommandArguments with | Cluster c -> c.Version | _ -> None
        
        let testsProjectDirectory = Path.Combine(Path.GetFullPath(Paths.Output("Tests.ClusterLauncher")), "netcoreapp3.0")
        let tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        
        printfn "%s" testsProjectDirectory
        
        Shell.copyDir tempDir testsProjectDirectory (fun _ -> true)
        
        let command = sprintf "%s %s" clusterName clusterVersion
        let timeout = TimeSpan.FromMinutes(120.)
        let dll = Path.Combine(tempDir, "Tests.ClusterLauncher.dll");
        Tooling.DotNet.ExecInWithTimeout tempDir [dll; command] timeout  |> ignore
        
        Shell.deleteDir tempDir
        
    let GenerateApi () =
        //TODO allow branch name to be passed for CI
        let folder = Path.getDirectory (Paths.ProjFile <| DotNetProject.PrivateProject PrivateProject.ApiGenerator)
        let timeout = TimeSpan.FromMinutes(120.)
        Tooling.DotNet.ExecInWithTimeout folder ["run"; ] timeout  |> ignore
        
    let RestSpecTests args =
        let folder = Path.getDirectory (Paths.ProjFile <| DotNetProject.PrivateProject PrivateProject.RestSpecTestRunner)
        let timeout = TimeSpan.FromMinutes(120.)
        Tooling.DotNet.ExecInWithTimeout folder (["run"; "--" ] @ args) timeout  |> ignore
         
