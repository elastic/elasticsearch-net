// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"
#load @"InheritDoc.fsx"
open Fake 
open System
open InheritDoc


// Properties
let buildDir = "build/output/"
let nugetOutDir = "build/output/_packages"

// Default target
Target "Build" (fun _ -> traceHeader "STARTING BUILD")

Target "Clean" (fun _ -> 
    CleanDir buildDir
)

Target "BuildApp" (fun _ ->
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
    !! "build/output/Nest/Nest.xml" |> Seq.iter(fun f -> PatchXmlDoc f)
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
    let sn = "build/tools/sn/sn.exe"
    ExecProcess(fun p ->
      p.FileName <- sn
      p.Arguments <- sprintf @"-k %s" keyFile
    ) (TimeSpan.FromMinutes 5.0) |> ignore
 
    ExecProcess(fun p ->
      p.FileName <- sn
      p.Arguments <- sprintf @"-p %s %s" keyFile "build/keys/public.snk"
    ) (TimeSpan.FromMinutes 5.0) |> ignore

Target "CreateKeysIfAbsent" (fun _ ->
    if not (fileExists keyFile) then createKeys()
)

let validateSignedAssembly = fun name ->
    let sn = "build/tools/sn/sn.exe"
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
    
    let dir = sprintf "%s/%s/" buildDir name
    let version = "1.0.0-c4"
    NuGetPack (fun p ->
      {p with 
        Version = version
        WorkingDir = dir 
        OutputPath = dir
      })
      (sprintf @"build\%s.nuspec" name)

    MoveFile nugetOutDir (buildDir + (sprintf "%s/%s.%s.nupkg" name name version)) 

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
  let v = (getBuildParamOrDefault "version" "0.1.0")
  let version = SemVerHelper.parse v
  let assemblyVersion = sprintf "%i.%i.0.0" version.Major version.Minor 

  trace (sprintf "%s %s" v assemblyVersion)
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

// Dependencies
"Clean" 
  ==> "CreateKeysIfAbsent"
  ==> "BuildApp"
  ==> "Test"
  ==> "Build"

"Build"
  ==> "Release"

"DocsPreview"
"CreateKeysIfAbsent"
"Version"
// start build
RunTargetOrDefault "Build"