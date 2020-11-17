// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System
open System.IO
open Fake.IO
open Commandline

module ReposTooling =

    let LaunchCluster args =
        let clusterName = Option.defaultValue "" <| match args.CommandArguments with | Cluster c -> Some c.Name | _ -> None
        let clusterVersion = Option.defaultValue "" <|match args.CommandArguments with | Cluster c -> c.Version | _ -> None
        
        let testsProjectDirectory = Path.GetFullPath(Paths.InplaceTestOutput "Tests.ClusterLauncher" "net5.0")
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
        let folder = Path.getDirectory (Paths.ProjFile "ApiGenerator")
        let timeout = TimeSpan.FromMinutes(120.)
        // Building to make sure XML docs files are there, faster then relying on the ApiGenerator to emit these
        // from a compilation unit
        Tooling.DotNet.ExecInWithTimeout folder ["run"; "-c"; " Release"; ] timeout  |> ignore
        
    let RestSpecTests args =
        let folder = Path.getDirectory (Paths.TestProjFile "Tests.YamlRunner")
        let timeout = TimeSpan.FromMinutes(120.)
        Tooling.DotNet.ExecInWithTimeout folder (["run"; "--" ] @ args) timeout  |> ignore
    
    
    let restoreOnce = lazy(Tooling.DotNet.Exec ["tool"; "restore"])
    
    let private differ = "assembly-differ"
    let Differ args =
        restoreOnce.Force()
              
        let args = args |> String.concat " "
        let command = sprintf @"%s %s -o ../../%s" differ args Paths.BuildOutput
        Tooling.DotNet.ExecIn Paths.TargetsFolder [command] |> ignore

    let private assemblyRewriter = "assembly-rewriter"
    let Rewriter args =
        restoreOnce.Force()
        Tooling.DotNet.ExecIn "." (List.append [assemblyRewriter] (List.ofSeq args)) |> ignore
        
    let private packageValidator = "nupkg-validator"
    let PackageValidator args =
        restoreOnce.Force()
        Tooling.DotNet.ExecIn "." (List.append [packageValidator] (List.ofSeq args)) |> ignore
         
