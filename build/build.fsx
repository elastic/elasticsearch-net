// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"
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

Target "BuildApp" (fun _ ->
    let binDirs = !! "src/**/bin/**"
                  |> Seq.map DirectoryName
                  |> Seq.distinct
                  |> Seq.filter (fun f -> (f.EndsWith("Debug") || f.EndsWith("Release")) && not (f.Contains "CodeGeneration"))

    CleanDirs binDirs

    //Override the prebuild event because it just calls a fake task BuildApp depends on anyways
    let msbuildProperties = [
      ("Configuration","Release"); 
      ("PreBuildEvent","ECHO"); 
    ]

    //Compile each csproj and output it seperately in build/output/PROJECTNAME
    !! "src/**/*.csproj"
      |> Seq.map(fun f -> (f, buildDir + directoryInfo(f).Name.Replace(".csproj", "")))
      |> Seq.iter(fun (f,d) -> MSBuild d "Build" msbuildProperties (seq { yield f }) |> ignore)
    
    //Scan for xml docs and patch them to replace <inheritdoc /> with the documentation
    //from their interfaces
    //!! "build/output/Nest/Nest.xml" |> Seq.iter(fun f -> PatchXmlDoc f)

    CopyFile "build/output/Elasticsearch.Net/install.ps1" "build/elasticsearch-init.ps1"

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
    let assemblyFileContents = ReadFileAsString @"src\NEST\Properties\AssemblyInfo.cs"
    let re = @"\[assembly\: AssemblyVersionAttribute\(""([^""]+)""\)\]"
    let matches = Regex.Matches(assemblyFileContents,re)
    let defaultVersion = regex_replace re "$1" (matches.Item(0).Captures.Item(0).Value)
    let timestampedVersion = (sprintf "%s-ci%s" defaultVersion (DateTime.UtcNow.ToString("yyyyMMddHHmmss")))
    let fileVersion = (getBuildParamOrDefault "version" timestampedVersion)
    fileVersion

let fileVersion = getFileVersion()

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
    let replacedContents = regex_replace re (sprintf "${start}%s${end}" fileVersion) packageContents
    WriteStringToFile false package replacedContents

    let dir = sprintf "%s/%s/" buildDir name
    NuGetPack (fun p ->
      {p with 
        Version = fileVersion
        WorkingDir = dir 
        OutputPath = dir
      })
      package

    MoveFile nugetOutDir (buildDir + (sprintf "%s/%s.%s.nupkg" name name fileVersion)) 

let buildDocs = fun action ->
    let node = @"build\tools\Node.js\node.exe"
    let wintersmith = @"..\build\tools\node_modules\wintersmith\bin\wintersmith"
    ExecProcess (fun p ->
        p.WorkingDirectory <- "new_docs"  
        p.FileName <- node
        p.Arguments <- sprintf "\"%s\" %s" wintersmith action
      ) 
      (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0))
   
Target "Version" (fun _ ->
  let version = SemVerHelper.parse fileVersion

  let suffix = fun (prerelease: PreRelease) -> sprintf "-%s%i" prerelease.Name prerelease.Number.Value
  let assemblySuffix = if version.PreRelease.IsSome then suffix version.PreRelease.Value else "";
  let assemblyVersion = sprintf "%i.0.0%s" version.Major assemblySuffix
  
  match (assemblySuffix, version.Minor, version.Patch) with
  | (s, m, p) when s <> "" && (m <> 0 || p <> 0)  -> failwithf "Cannot create prereleases for minor or major builds!"
  | ("", _, _) -> traceFAKE "Building fileversion %s for asssembly version %s" fileVersion assemblyVersion
  | _ -> traceFAKE "Building prerelease %s for major assembly version %s " fileVersion assemblyVersion

  let assemblyDescription = fun (f: string) ->
    let name = f 
    match f.ToLowerInvariant() with
    | f when f = "elasticsearch.net" -> "Elasticsearch.Net - oficial low level elasticsearch client"
    | f when f = "nest" -> "NEST - oficial high level elasticsearch client"
    | f when f = "elasticsearch.net.connection.thrift" -> "Elasticsearc.Net.Connection.Thrift - Add thrift support to elasticsearch."
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
        Attribute.FileVersion fileVersion
        Attribute.InformationalVersion fileVersion
      ]
    )
)


Target "Release" (fun _ -> 
    if not <| fileExists keyFile 
      then failwithf "{0} does not exist to sign the assemblies" keyFile 
    
    nugetPack("Elasticsearch.Net")
    nugetPack("Elasticsearch.Net.Connection.Thrift")
    nugetPack("Nest")
   
    validateSignedAssembly("Elasticsearch.Net")
    validateSignedAssembly("Elasticsearch.Net.Connection.Thrift")
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
  ==> "Test"
  ==> "Build"

"CreateKeysIfAbsent"
  ==> "Version"
  ==> "Release"
  ==> "Nightly"


"Build"
  ==> "Docs"
  ==> "Release"

"DocsPreview"
"BuildApp"
"CreateKeysIfAbsent"
"Version"
// start build
RunTargetOrDefault "Build"