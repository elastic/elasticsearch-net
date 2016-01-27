#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
#load @"Projects.fsx"
open System
open Fake 
open Paths
open Projects

let gitLink = fun f ->
    let exe = Paths.Tool("gitlink/lib/net45/GitLink.exe")
    ExecProcess(fun p ->
      p.FileName <- exe
      p.Arguments <- sprintf @". -u %s" Paths.Repository
    ) (TimeSpan.FromMinutes 5.0) |> ignore

type Build() = 
    //Override the prebuild event because it just calls a fake task BuildApp depends on anyways
    static let msbuildProperties = [
      ("Configuration","Release"); 
      ("PreBuildEvent","echo");
    ]

    static member QuickCompile() = 
        let projects = !! Paths.Source("*/project.json") 
                       |> Seq.map DirectoryName

        projects
        |> Seq.iter(fun project -> 
            let path = (Paths.Quote project)
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["restore"; path]
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["build"; path; "--configuration Release --quiet";]
           )

    static member BuildFailure errors =
        raise (BuildException("The project build failed.", errors |> List.ofSeq))

    static member Compile() =
        let projects = !! Paths.Source("*/project.json") 
                       |> Seq.map DirectoryName

        projects
        |> Seq.iter(fun project -> 

            //eventhough this says desktop it still builds all the tfm's it just hints wich installed dnx version to use
            let path = (Paths.Quote project)
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["restore"; path]
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["build"; path; "--configuration Release";]
           )

        projects
        |> Seq.iter(fun project ->
            let projectName = (project |> directoryInfo).Name
            let outputFolder = Paths.Output(projectName)
            let srcFolder = Paths.BinFolder(projectName)
            CopyDir outputFolder srcFolder allFiles
        )

        if not isMono then gitLink()







