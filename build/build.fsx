// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"
open Fake 
open System

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
let ilmerge = "build/tools/ilmerge/ILMerge.exe"
let platform =  @"/targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
let signAssembly = fun (dll, outDll) ->
    ExecProcess(fun p ->
      p.FileName <- ilmerge
      p.Arguments <- (sprintf "\"build\\output\\%s\" /keyfile:\"%s\" /out:\"build\\output\\%s\" %s" dll keyFile outDll platform)
    ) (TimeSpan.FromMinutes 5.0)

Target "Release" (fun _ -> 
    if signAssembly("Nest\\Nest.dll", "Nest.dll") <> 0 then failwithf "Failed to sign Nest.dll"
    CreateDir nugetOutDir

    NuGetPack (fun p ->
      {p with 
        Version = "1.0.0.0"
        WorkingDir = buildDir + "/Nest/"
        OutputPath = buildDir + "/Nest/"
      })
      "build\NEST.nuspec"

    MoveFile nugetOutDir (buildDir + (sprintf "Nest/NEST.%s.nupkg" "1.0.0.0")) 
)

// Dependencies
"Clean" 
  ==> "BuildApp"
  ==> "Test"
  ==> "Build"

//"Build"
//  ==> "Release"

// start build
RunTargetOrDefault "Build"