#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Projects.fsx"
open System
open System.Text.RegularExpressions
open Fake 
open SemVerHelper
open AssemblyInfoFile
open Projects
open System.Diagnostics

type Versioning() = 
    
    static let suffix = fun (prerelease: PreRelease) -> sprintf "-%s%i" prerelease.Name prerelease.Number.Value
    //returns the current version number 
    //when version is passed to script we always use that
    //otherwise we get the current version number and append -ci-datestamp
    static let currentInformationalVersion = 
        let assemblyFileContents = ReadFileAsString @"src/Nest/Properties/AssemblyInfo.cs"
        let re = @"\[assembly\: AssemblyInformationalVersionAttribute\(""([^""]+)""\)\]"
        let matches = Regex.Matches(assemblyFileContents,re)
        regex_replace re "$1" (matches.Item(0).Captures.Item(0).Value)

    static let currentFileVersion = 
        let assemblyFileContents = ReadFileAsString @"src/Nest/Properties/AssemblyInfo.cs"
        let re = @"\[assembly\: AssemblyFileVersionAttribute\(""([^""]+)""\)\]"
        let matches = Regex.Matches(assemblyFileContents,re)
        regex_replace re "$1" (matches.Item(0).Captures.Item(0).Value)

    static let fileVersion = 
        let timestampedVersion = (sprintf "%s-ci%s" currentFileVersion (DateTime.UtcNow.ToString("yyyyMMddHHmmss")))
        let fileVersion = (getBuildParamOrDefault "version" timestampedVersion)
        let fv = if isNullOrEmpty fileVersion then timestampedVersion else fileVersion
        fv
    
    //CI builds need to be one minor ahead of the whatever we find in master
    static member FileVersion = 
        //only increment minor when the current assembly informational version is not already -ci 
        let i = if currentInformationalVersion.Contains("-ci") then 0 else 1
        match fileVersion with
        | f when f.Contains("-ci") ->
            let v = regex_replace "-ci.+$" "" f
            let prerelease = regex_replace "^.+-(ci.+)$" "$1" f
            let version = SemVerHelper.parse v
            (sprintf "%d.%d.0-%s" version.Major (version.Minor + i) prerelease).Trim()
        | _ -> fileVersion.Trim()

    static member AssemblyVersion = 
        let fileVersion = Versioning.FileVersion
        let fv = if fileVersion.Contains("-ci") then (regex_replace "-ci.+$" "" fileVersion) else fileVersion
        traceFAKE "patched fileVersion %s" fileVersion
        let version = SemVerHelper.parse fv
    
        let assemblySuffix = if version.PreRelease.IsSome then suffix version.PreRelease.Value else "";
        let assemblyVersion = sprintf "%i.0.0%s" version.Major assemblySuffix
      
        match (assemblySuffix, version.Minor, version.Patch) with
        | (s, m, p) when s <> "" && s <> "ci" && (m <> 0 || p <> 0)  -> failwithf "Cannot create prereleases for minor or major builds!"
        | ("", _, _) -> traceFAKE "Building fileversion %s for asssembly version %s" fileVersion assemblyVersion
        | _ -> traceFAKE "Building prerelease %s for major assembly version %s " fileVersion assemblyVersion
    
        assemblyVersion

    static member PatchAssemblyInfos() =
        let assemblyVersion = Versioning.AssemblyVersion
        let fileVersion = Versioning.FileVersion
        !! "src/**/AssemblyInfo.cs"
        |> Seq.iter(fun f -> 
            let name = (directoryInfo f).Parent.Parent.Name
            let projectName = DotNetProject.TryFindName name
            let assemblyDescription =
                match projectName with
                | Some p -> p.NugetDescription.Value
                | _ -> ""

            CreateCSharpAssemblyInfo f [
                Attribute.Title name
                Attribute.Copyright (sprintf "Elasticsearch %i" DateTime.UtcNow.Year)
                Attribute.Description assemblyDescription 
                Attribute.Company "Elasticsearch"
                Attribute.Configuration "Release"
                Attribute.Version assemblyVersion
                Attribute.FileVersion fileVersion
                Attribute.InformationalVersion fileVersion
            ]
        )

    static member PatchProjectJsons() =
        !! "src/**/project.json"
        |> Seq.iter(fun f -> 
            RegexReplaceInFileWithEncoding "\"version\"\\s?:\\s?\".*\"" (sprintf "\"version\": \"%s\"" fileVersion) (new System.Text.UTF8Encoding(false)) f
        )

    static member ValidateArtifacts() =
        let assemblyVersion = Versioning.AssemblyVersion
        let fileVersion = Versioning.FileVersion
        let tmp = "build/output/_packages/tmp"
        !! "build/output/_packages/*.nupkg"
        |> Seq.iter(fun f -> 
           Unzip tmp f
           !! (sprintf "%s/**/*.dll" tmp)
           |> Seq.iter(fun f -> 
                let fv = FileVersionInfo.GetVersionInfo(f)
                let a = GetAssemblyVersion f
                traceFAKE "Assembly: %A File: %s Product: %s => %s" a fv.FileVersion fv.ProductVersion f
                if (a.Minor > 0 || a.Revision > 0 || a.Build > 0) then failwith (sprintf "%s assembly version is not sticky to its major component" f)
                if (fv.ProductVersion <> fileVersion) then failwith (sprintf "Expected product info %s to match new version %s " fv.ProductVersion fileVersion)
           )
           DeleteDir tmp
        )