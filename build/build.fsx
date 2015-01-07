// include Fake lib
#r @"FakeLib.dll"
#load @"InheritDoc.fsx"
open Fake 
open System
open InheritDoc
open SemVerHelper
open AssemblyInfoFile
open System.Text.RegularExpressions
open System.Linq

// Properties
let buildDir = "build/output/"
let nugetOutDir = "build/output/_packages"

// Default target
Target "Build" (fun _ -> traceHeader "STARTING BUILD")

Target "Clean" (fun _ -> 
    CleanDir buildDir
)

let gitLink = fun _ ->
    let exe = "build/tools/gitlink/lib/net45/GitLink.exe"
    ExecProcess(fun p ->
      p.FileName <- exe
      p.Arguments <- sprintf @". -u https://github.com/elasticsearch/elasticsearch-net -b develop" 
    ) (TimeSpan.FromMinutes 5.0) |> ignore
 
Target "BuildApp" (fun _ ->
    let binDirs = !! "src/**/bin/**"
                  |> Seq.map DirectoryName
                  |> Seq.distinct
                  |> Seq.filter (fun f -> (f.EndsWith("Debug") || f.EndsWith("Release")) && not (f.Contains "CodeGeneration"))

    CleanDirs binDirs

    //Override the prebuild event because it just calls a fake task BuildApp depends on anyways
    let msbuildProperties = [
      ("Configuration","Release"); 
      ("PreBuildEvent","echo"); 
    ]

    MSBuild null "Rebuild" msbuildProperties (seq { yield "src/Elasticsearch.sln" }) 
    gitLink()

    //moves all the release builds to build/output/PROJECTNAME
    !! "src/**/*.csproj"
      |> Seq.map(fun f -> (f, buildDir + directoryInfo(f).Name.Replace(".csproj", "")))
      |> Seq.iter(fun (f,d) -> 
        CreateDir d
        CopyDir d (directoryInfo(f).Parent.FullName + @"\bin\Release") allFiles
      )
    
    //Scan for xml docs and patch them to replace <inheritdoc /> with the documentation
    //from their interfaces
    //!! "build/output/Nest/Nest.xml" |> Seq.iter(fun f -> PatchXmlDoc f)
)

Target "Test" (fun _ ->
    !! (buildDir + "/**/*.Tests.Unit.dll") 
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = buildDir + "TestResults.xml" }
         )
)

let keyFile = "build/keys/keypair.snk"

let createKeys = fun _ ->
    let sn = if isMono then "sn" else "build/tools/sn/sn.exe"
    ExecProcess(fun p ->
      p.FileName <- sn
      p.Arguments <- sprintf @"-k %s" keyFile
    ) (TimeSpan.FromMinutes 5.0) |> ignore
 
    ExecProcess(fun p ->
      p.FileName <- sn
      p.Arguments <- sprintf @"-p %s %s" keyFile "build/keys/public.snk"
    ) (TimeSpan.FromMinutes 5.0) |> ignore

Target "CreateKeysIfAbsent" (fun _ ->
    if not (directoryExists "build/keys") then CreateDir "build/keys"
    if not (fileExists keyFile) then createKeys()
)

let getFileVersion = fun _ ->
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

let fileVersion = getFileVersion()

//CI builds need to be one minor ahead of the whatever we find in our develop branch
let patchedFileVersion = 
    match fileVersion with
    | f when f.Contains("-ci") ->
        let v = regex_replace "-ci.+$" "" f
        let prerelease = regex_replace "^.+-(ci.+)$" "$1" f
        let version = SemVerHelper.parse v
        sprintf "%d.%d.0-%s" version.Major (version.Minor + 1) prerelease
    | _ -> fileVersion

let validateSignedAssembly = fun name ->
    let sn = if isMono then "sn" else "build/tools/sn/sn.exe"
    let out = (ExecProcessAndReturnMessages(fun p ->
                p.FileName <- sn
                p.Arguments <- sprintf @"-v build\output\%s\%s.dll" name name
              ) (TimeSpan.FromMinutes 5.0))
    
    let valid = (out.ExitCode, out.Messages.FindIndex(fun s -> s.Contains("is valid")))

    match valid with
    | (0, i) when i >= 0 -> trace (sprintf "%s was signed correctly" name) 
    | (_, _) -> failwithf "{0} was not validly signed"
    
    let out = (ExecProcessAndReturnMessages(fun p ->
                p.FileName <- sn
                p.Arguments <- sprintf @"-T build\output\%s\%s.dll" name name
              ) (TimeSpan.FromMinutes 5.0))
    
    let tokenMessage = (out.Messages.Find(fun s -> s.Contains("Public key token is")));
    let token = (tokenMessage.Replace("Public key token is", "")).Trim();

    let valid = (out.ExitCode, token)
    let oficialToken = "96c599bbe3e70f5d"
    match valid with
    | (0, t) when t = oficialToken  -> 
      trace (sprintf "%s was signed with official key token %s" name t) 
    | (_, t) -> traceFAKE "%s was not signed with the official token: %s but %s" name oficialToken t

let nugetPack = fun name ->
    CreateDir nugetOutDir
    let package = (sprintf @"build\%s.nuspec" name)
    let packageContents = ReadFileAsString package
    let re = @"(?<start>\<version\>|""(Elasticsearch.Net|Nest)"" version="")[^""><]+(?<end>\<\/version\>|"")"
    let replacedContents = regex_replace re (sprintf "${start}%s${end}" patchedFileVersion) packageContents
    WriteStringToFile false package replacedContents

    let dir = sprintf "%s/%s/" buildDir name
    let nugetOutFile = buildDir + (sprintf "%s/%s.%s.nupkg" name name patchedFileVersion);
    NuGetPack (fun p ->
      {p with 
        Version = patchedFileVersion
        WorkingDir = "build" 
        OutputPath = dir
      })
      package

    MoveFile nugetOutDir nugetOutFile

let buildDocs = fun action ->
    let node = @"build\tools\Node.js\node.exe"
    let wintersmith = @"..\build\tools\node_modules\wintersmith\bin\wintersmith"
    ExecProcess (fun p ->
        p.WorkingDirectory <- "docs"  
        p.FileName <- node
        p.Arguments <- sprintf "\"%s\" %s" wintersmith action
      ) 
      (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0))

let suffix = fun (prerelease: PreRelease) -> sprintf "-%s%i" prerelease.Name prerelease.Number.Value
let getAssemblyVersion = (fun _ ->
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
)

Target "Version" (fun _ ->
  let assemblyVersion = getAssemblyVersion()

  let assemblyDescription = fun (f: string) ->
    let name = f 
    match f.ToLowerInvariant() with
    | f when f = "elasticsearch.net" -> "Elasticsearch.Net - oficial low level elasticsearch client"
    | f when f = "nest" -> "NEST - oficial high level elasticsearch client"
    | f when f = "elasticsearch.net.connection.thrift" -> "Add thrift support to elasticsearch."
    | f when f = "elasticsearch.net.connection.httpclient" -> "IConnection implementation that uses HttpClient (.NET 4.5 only)"
    | f when f = "elasticsearch.net.jsonnet" -> "IElasticsearchSerializer implementation that allows you to use Json.NET with the lowlevel client"
    | _ -> sprintf "%s" name

  !! "src/**/AssemblyInfo.cs"
    |> Seq.iter(fun f -> 
      let name = (directoryInfo f).Parent.Parent.Name
      CreateCSharpAssemblyInfo f [
        Attribute.Title name
        Attribute.Copyright (sprintf "Elasticsearch %i" DateTime.UtcNow.Year)
        Attribute.Description (assemblyDescription name)
        Attribute.Company "Elasticsearch"
        Attribute.Configuration "Release"
        Attribute.Version assemblyVersion
        Attribute.FileVersion patchedFileVersion
        Attribute.InformationalVersion patchedFileVersion
      ]
    )
)


Target "Release" (fun _ -> 
    if not <| fileExists keyFile 
      then failwithf "{0} does not exist to sign the assemblies" keyFile 
    
    nugetPack("Elasticsearch.Net")
    nugetPack("Elasticsearch.Net.Connection.Thrift")
    nugetPack("Elasticsearch.Net.Connection.HttpClient")
    nugetPack("Elasticsearch.Net.JsonNET")
    nugetPack("Nest")
   
    validateSignedAssembly("Elasticsearch.Net")
    validateSignedAssembly("Elasticsearch.Net.Connection.Thrift")
    validateSignedAssembly("Elasticsearch.Net.Connection.HttpClient")
    validateSignedAssembly("Elasticsearch.Net.JsonNET")
    validateSignedAssembly("Nest")
)

Target "Docs" (fun _ -> buildDocs "build" |> ignore)
Target "DocsPreview" (fun _ -> 
  buildDocs "plugin install livereload" |> ignore
  buildDocs "preview" |> ignore
)
Target "Nightly" (fun _ -> 
    trace "build nightly"
)
// Dependencies
"Clean" 
  ==> "CreateKeysIfAbsent"
  =?> ("Version", hasBuildParam "version")
  ==> "BuildApp"
  =?> ("Test", (not (hasBuildParam "skiptests")))
  ==> "Build"

"CreateKeysIfAbsent"
  ==> "Version"
  ==> "Release"
  ==> "Nightly"


"Build"
  ==> "Release"

"DocsPreview"
"BuildApp"
"CreateKeysIfAbsent"
"Version"
// start build
RunTargetOrDefault "Build"
