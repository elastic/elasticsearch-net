#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Projects.fsx"
open System
open System.Text.RegularExpressions
open Fake 
open SemVerHelper
open AssemblyInfoFile
open Projects

type Versioning() = 
    
    static let suffix = fun (prerelease: PreRelease) -> sprintf "-%s%i" prerelease.Name prerelease.Number.Value
    //returns the current version number 
    //when version is passed to script we always use that
    //otherwise we get the current version number and append -ci-datestamp
    static let fileVersion = 
        let assemblyFileContents = ReadFileAsString @"src/Nest/Properties/AssemblyInfo.cs"
        let re = @"\[assembly\: AssemblyFileVersionAttribute\(""([^""]+)""\)\]"
        let matches = Regex.Matches(assemblyFileContents,re)
        let defaultVersion = regex_replace re "$1" (matches.Item(0).Captures.Item(0).Value)
        let timestampedVersion = (sprintf "%s-ci%s" defaultVersion (DateTime.UtcNow.ToString("yyyyMMddHHmmss")))
        trace ("timestamped: " + timestampedVersion)
        let fileVersion = (getBuildParamOrDefault "version" timestampedVersion)
        let fv = if isNullOrEmpty fileVersion then timestampedVersion else fileVersion
        trace ("fileVersion: " + fv)
        fv
    
    //CI builds need to be one minor ahead of the whatever we find in our develop branch
    static member FileVersion = 
        match fileVersion with
        | f when f.Contains("-ci") ->
            let v = regex_replace "-ci.+$" "" f
            let prerelease = regex_replace "^.+-(ci.+)$" "$1" f
            let version = SemVerHelper.parse v
            sprintf "%d.%d.0-%s" version.Major (version.Minor + 1) prerelease
        | _ -> fileVersion

    static member AssemblyVersion = 
        let fileVersion = Versioning.FileVersion
        let fv = if fileVersion.Contains("-ci") then (regex_replace "-ci.+$" "" fileVersion) else fileVersion
        traceFAKE "patched fileVersion %s" fv
        let version = SemVerHelper.parse fv
    
        let assemblySuffix = if version.PreRelease.IsSome then suffix version.PreRelease.Value else "";
        let assemblyVersion = sprintf "%i.0.0%s" version.Major assemblySuffix
      
        match (assemblySuffix, version.Minor, version.Patch) with
        | (s, m, p) when s <> "" && s <> "ci" && (m <> 0 || p <> 0)  -> failwithf "Cannot create prereleases for minor or major builds!"
        | ("", _, _) -> traceFAKE "Building fileversion %s for asssembly version %s" fileVersion assemblyVersion
        | _ -> traceFAKE "Building prerelease %s for major assembly version %s " fileVersion assemblyVersion
    
        assemblyVersion

    static member PatchAssemblyInfos() =
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
                Attribute.Version Versioning.AssemblyVersion
                Attribute.FileVersion Versioning.FileVersion
                Attribute.InformationalVersion Versioning.FileVersion
            ]
        )