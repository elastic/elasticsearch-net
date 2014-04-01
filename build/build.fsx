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
    //Compile each csproj and output it seperately in build/output/PROJECTNAME
    !! "src/**/*.csproj"
      |> Seq.map(fun f -> (f, buildDir + directoryInfo(f).Name.Replace(".csproj", "")))
      |> Seq.iter(fun (f,d) -> MSBuildRelease d "Build" (seq { yield f }) |> ignore)
    
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
let validateSignedAssembly = fun name ->
    let sn = "build/tools/sn/sn.exe"
    ExecProcess(fun p ->
      p.FileName <- sn
      p.Arguments <- sprintf @"-v build\output\%s.dll" name
    ) (TimeSpan.FromMinutes 5.0)

let signAssembly = fun name ->
    let ilmerge = "build/tools/ilmerge/ILMerge.exe"
    let platform =  @"/targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
    let i = sprintf "build\\output\\%s\\%s" name name
    let o = sprintf "build\\output\\%s" name
    
    let dll = i + ".dll"
    let outdll = o + ".dll"
    let pdb = o + ".pdb"

    let signExitCode = (
      ExecProcess(fun p ->
        p.FileName <- ilmerge
        p.Arguments <- (sprintf "%s /keyfile:%s /out:%s %s" 
          dll keyFile outdll platform)
      ) (TimeSpan.FromMinutes 5.0)
    )

    let validateExitCode = validateSignedAssembly name
    match (signExitCode, validateExitCode) with
      | (0,0) ->
        MoveFile (DirectoryName dll) outdll 
        MoveFile (DirectoryName dll) pdb 
      | _ ->
        failwithf "Failed to sign {0}" name

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
    ) (TimeSpan.FromMinutes (if action = "preview" then 300.0 else 5.0))

Target "Version" (fun _ ->
  let v = (getBuildParamOrDefault "version" "0.1.0")
  let version = SemVerHelper.parse v
  let assemblyVersion = sprintf "%i.%i.0.0" version.Major version.Minor 

  trace (sprintf "%s %s" v assemblyVersion)
)


Target "Release" (fun _ -> 
    if not <| fileExists keyFile 
      then failwithf "{0} does not exist to sign the assemblies" keyFile 
    
    //signAssembly("Elasticsearch.Net")
    //signAssembly("Elasticsearch.Net.Connection.Thrift")
    //signAssembly("Nest")
    
    nugetPack("Elasticsearch.Net")
    nugetPack("Elasticsearch.Net.Connection.Thrift")
    nugetPack("Nest")
   
)

Target "Docs" (fun _ -> buildDocs "build" |> ignore)
Target "DocsPreview" (fun _ -> 
  buildDocs "plugin install livereload" |> ignore
  buildDocs "preview" |> ignore
)

// Dependencies
"Clean" 
  ==> "BuildApp"
  ==> "Test"
  ==> "Build"

"Build"
  ==> "Release"

"DocsPreview"
"Version"
// start build
RunTargetOrDefault "Build"