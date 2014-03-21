// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"
open Fake 

// Properties
let buildDir = "./build/output/"

// Default target
Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

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

// Dependencies
"Clean" 
  ==> "BuildApp"
  ==> "Test"
  ==> "Default"

// start build
RunTargetOrDefault "Default"