namespace Scripts

open System.IO

open Paths
open Projects
open Tooling
open Versioning
open Fake.Core
open Fake.IO
open Commandline

module Build =

    let Restore() = DotNet.Exec ["restore"; Solution; ] |> ignore
        
    let Compile args (ArtifactsVersion(version)) = 
        let sourceLink = if args.DoSourceLink then "1" else ""
        let props = 
            [ 
                "CurrentVersion", (version.Full.ToString());
                "CurrentAssemblyVersion", (version.Assembly.ToString());
                "CurrentAssemblyFileVersion", (version.AssemblyFile.ToString());
                "DoSourceLink", sourceLink;
                "FakeBuild", "1";
                "OutputPathBaseDir", Path.GetFullPath Paths.BuildOutput;
            ] 
            |> List.map (fun (p,v) -> sprintf "%s=%s" p v)
            |> String.concat ";"
            |> sprintf "/property:%s"
            
        DotNet.Exec ["build"; Solution; "-c"; "Release"; props] |> ignore


    let Clean () =
        printfn "Cleaning known output folders"
        Shell.cleanDir Paths.BuildOutput
        DotNet.Exec ["clean"; Solution; "-c"; "Release"] |> ignore 
        DotNetProject.All |> Seq.iter(fun p -> Shell.cleanDir (Paths.BinFolder p.Name))
        
         