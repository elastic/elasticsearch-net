namespace Scripts

open System 
open FSharp.Data
open Projects
open Versioning
open Tooling

module ShadowDependencies =

    let private assemblyRewriter = "assembly-rewriter"
    let private keyFile = Paths.Keys "keypair.snk"
    let Rewrite majorVersion framework projects =
        let project = projects |> Seq.head
        let folder = Paths.ProjectOutputFolder project framework
        
        let dllFullPath name = sprintf "%s/%s.dll" folder name
        let outputName (p: DotNetProject) = match p.Name = project.Name with | true -> p.Name | _ -> p.InternalName 
        let fullOutput (p: DotNetProject) = dllFullPath (p.Versioned (outputName p) majorVersion)
        let dlls =
            projects
            |> Seq.map (fun p -> sprintf @"-i ""%s"" -o ""%s"" "  (dllFullPath p.Name) (fullOutput p))
            |> Seq.fold (+) " "
            
        Tooling.DotNet.Exec [assemblyRewriter; dlls] |> ignore
        
        let mergedOutFile = fullOutput project
        let ilMergeArgs = [
            "/internalize"; 
            (sprintf "/lib:%s" folder); 
            (sprintf "/keyfile:%s" keyFile); 
            (sprintf "/out:%s" mergedOutFile)
        ]
        let mergeDlls = 
            projects 
            |> Seq.filter (fun p -> p.Name = project.Name || not <| (DotNetProject.AllPublishable |> Seq.contains p)) 
            |> Seq.map fullOutput
        match project.NeedsMerge with 
        | true -> Tooling.ILRepack.Exec (ilMergeArgs |> List.append (mergeDlls |> Seq.toList)) |> ignore
        | _ -> Tooling.ILRepack.Exec (ilMergeArgs |> List.append [mergeDlls |> Seq.head]) |> ignore

    let ShadowDependencies (ArtifactsVersion(version)) = 
        let fw = DotNetFramework.All
        let projects = DotNetProject.AllPublishable
        let currentMajor = sprintf "%i" <| version.Full.Major
        for f in fw do
            for p in projects do
                if p.VersionedMergeDependencies <> [] then Rewrite (Some currentMajor) f p.VersionedMergeDependencies
                if p.MergeDependencies <> [] then Rewrite None f p.MergeDependencies
    
         