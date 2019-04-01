﻿namespace Scripts

open System
open System.Reflection
open System.Diagnostics
open System.IO
open FSharp.Data

open Commandline
open Fake.Core
open Fake.IO.Globbing.Operators
open Fake.IO
open Fake.Core
open Fake.IO

module Versioning = 
    // We used to rely on AssemblyInfo.cs from NEST to read and write the current version.
    // Since that file is now generated by the dotnet tooling and GitVersion and similar tooling all still have
    // active issues related to dotnet core, we now just burn this info in global.json
    
    let parse (v:string) = SemVer.parse(v)

    //Versions in form of e.g 6.1.0 is inferred as datetime so we bake the json shape into the provider like this
    type private GlobalJson = JsonProvider<""" { "sdk": { "version":"x" }, "version": "x" } """ >
    let globalJson = GlobalJson.Load("../../../../../global.json");
    let writeVersionIntoGlobalJson version = 
        let newGlobalJson = GlobalJson.Root (GlobalJson.Sdk(globalJson.Sdk.Version), version.ToString())
        use tw = new StreamWriter("global.json")
        newGlobalJson.JsonValue.WriteTo(tw, JsonSaveOptions.None)
        printfn "Written (%s) to global.json as the current version will use this version from now on as current in the build" (version.ToString()) 

    let GlobalJsonVersion = parse globalJson.Version
    
    let private getVersion (args:Commandline.PassedArguments) =
        match (args.Target, args.CommandArguments) with
        | (_, SetVersion v) ->
            match v.Version with
            | v when String.IsNullOrEmpty v -> None
            | v -> Some <| parse v
        | ("canary", _) ->
            let v = GlobalJsonVersion
            let timestampedVersion = (sprintf "ci%s" (DateTime.UtcNow.ToString("yyyyMMddTHHmmss")))
            let canaryVersion = parse ((sprintf "%d.%d.0-%s" v.Major (v.Minor + 1u) timestampedVersion).Trim())
            Some canaryVersion
        | _ -> None
    
    
    type AnchoredVersion = { Full: SemVerInfo; Assembly:SemVerInfo; AssemblyFile:SemVerInfo }
    type BuildVersions =
        | Update of New: AnchoredVersion * Old: AnchoredVersion 
        | NoChange of Current: AnchoredVersion
        
    type ArtifactsVersion = ArtifactsVersion of AnchoredVersion

    let AnchoredVersion version = 
        let av v = parse (sprintf "%s.0.0" (v.Major.ToString()))
        let fv v = parse (sprintf "%s.%s.%s.0" (v.Major.ToString()) (v.Minor.ToString()) (v.Patch.ToString()))
        { Full = version; Assembly = av version; AssemblyFile = fv version }
    
    let BuildVersioning args =
        let currentVersion = GlobalJsonVersion
        let buildVersion = getVersion args
        match buildVersion with
        | None -> NoChange(Current = AnchoredVersion currentVersion)
        | Some v -> Update(New = AnchoredVersion v, Old = AnchoredVersion currentVersion)
        
    let Validate target version = 
        match (target, version) with
        | ("release", version) ->
            match version with
            | NoChange _ -> failwithf "cannot run release because no explicit version number was passed on the command line"
            | Update (newVersion, currentVersion) -> 
                // fail if current is greater or equal to the new version
                if (currentVersion >= newVersion) then
                    failwithf "Can not release %O its lower then current %O" newVersion.Full currentVersion.Full
                writeVersionIntoGlobalJson newVersion
        | _ -> ignore()
    
    let ArtifactsVersion buildVersions =
        match buildVersions with
        | NoChange n -> ArtifactsVersion n
        | Update (newVersion, _) -> ArtifactsVersion newVersion
    
    let private sn () =
            match isMono with 
            | true -> "sn"
            | false ->
                let programFiles = Environment.environVar "PROGRAMFILES(X86)"
                if not (Directory.Exists programFiles) then failwith "Can not locate 64 bit program files"
                let windowsSdks =  ["v10.0A"; "v8.1A"; "v8.1"; "v8.0"; "v7.0A";]
                let dotNetVersion = ["4.7.2"; "4.7.1"; "4.7"; "4.6.2"; "4.6.1"; "4.0"]
                let combinations = List.allPairs windowsSdks dotNetVersion
                let winFolder w = Path.Combine(programFiles, "Microsoft SDKs", "Windows", w, "bin")
                let sdkFolder w d = 
                    let folder = sprintf "NETFX %s Tools" d
                    Path.Combine(winFolder w, folder)
                let snExe w d = Path.Combine(sdkFolder w d, "sn.exe")
                let sn = combinations |> List.map (fun (w, d) -> snExe w d) |> List.tryFind File.exists
                match sn with
                | Some sn -> sn
                | None -> failwithf "Could not locate sn.exe"

    let private oficialToken = "96c599bbe3e70f5d"

    let private validate dll name =
        let sn = sn ()
        let out = Tooling.read sn ["-v"; dll;]
        
        let valid = (out.ExitCode, out.Output |> Seq.findIndex(fun s -> s.Line.Contains("is valid")))
        match valid with
        | (Some 0, i) when i >= 0 -> printfn "%s was signed correctly" name 
        | (_, _) -> failwithf "{0} was not validly signed"
        
        let out = Tooling.read sn ["-T"; dll;]
        
        let tokenMessage = (out.Output |> Seq.find(fun s -> s.Line.Contains("Public key token is")));
        
        let token = (tokenMessage.Line.Replace("Public key token is", "")).Trim();
    
        let valid = (out.ExitCode, token)
        match valid with
        | (Some 0, t) when t = oficialToken  -> printfn "%s was signed with official key token %s" name t
        | (_, t) -> printfn "%s was not signed with the official token: %s but %s" name oficialToken t
        
    let private validateDllStrongName dll name =
        match File.Exists dll with
        | true -> validate dll name 
        | _ -> failwithf "Attemped to verify signature of %s but it was not found!" dll

    let ValidateArtifacts (ArtifactsVersion(version)) =
        let fileVersion = version.AssemblyFile
        let tmp = "build/output/_packages/tmp"
        !! "build/output/_packages/*.nupkg"
        |> Seq.iter(fun f -> 
           Zip.unzip tmp f
           !! (sprintf "%s/**/*.dll" tmp)
           |> Seq.iter(fun f -> 
                let fv = FileVersionInfo.GetVersionInfo(f)
                let a = AssemblyName.GetAssemblyName(f).Version
                printfn "Assembly: %A File: %s Product: %s => %s" a fv.FileVersion fv.ProductVersion f
                if (a.Minor > 0 || a.Revision > 0 || a.Build > 0) then failwith (sprintf "%s assembly version is not sticky to its major component" f)
                if (parse (fv.ProductVersion) <> version.Full) then
                    failwith (sprintf "Expected product info %s to match new version %O " fv.ProductVersion fileVersion)

                validateDllStrongName f f
           )
           Directory.delete tmp
        )