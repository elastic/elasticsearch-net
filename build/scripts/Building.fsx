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
      p.Arguments <- sprintf @". -u %s -b develop" Paths.Repository
    ) (TimeSpan.FromMinutes 5.0) |> ignore

type Build() = 
    //Override the prebuild event because it just calls a fake task BuildApp depends on anyways
    static let msbuildProperties = [
      ("Configuration","Release"); 
      ("PreBuildEvent","echo");
    ]

    static let moveToBuildOutput (f: DotNetFramework) =
        for p in DotNetProject.All do
            let outputFolder = Paths.Output(p.ProjectName.Nuget)
            let srcFolder = Paths.BinFolder(p.ProjectName.Location)
            match f with
            | NET40 -> printfn "ignored for now"
            | NET45 ->
                let net45dir = sprintf "%s/net45" outputFolder
                CopyDir net45dir srcFolder allFiles

    static let toTarget (f: DotNetFramework) =
        match f with 
        | NET45 -> "Rebuild"
        | NET40 -> "Rebuild"
//            DotNet40Project.All
//            |> List.map(fun p-> (DotNet40Project p).ProjectName)
//            |> List.map (fun n -> sprintf "%s:Rebuild" n.MSBuild)
//            |> String.concat ";"
    
    static member Compile(framework: DotNetFramework) =
        let f = framework.Identifier
        let properties = msbuildProperties |> List.append [("TargetFrameworkVersion", f.MSBuild)] 
        let target = toTarget framework

        CleanDirs <| Paths.MsBuildOutput
        MSBuild null target properties (seq { yield "src/Elasticsearch.sln" }) |> ignore
        if not isMono then gitLink()
        moveToBuildOutput(framework)

    static member CompileAll() = DotNetFramework.All |> Seq.iter Build.Compile |> ignore

    static member QuickCompile() = 
        let f = DotNetFramework.NET45
        let properties =  ("Verbosity", "Quiet") :: ("TargetFrameworkVersion", f.Identifier.MSBuild) :: msbuildProperties 
        let setParams defaults =
            { defaults with
                Verbosity = Some(Quiet)
                Targets = ["Build"]
                Properties =
                    [
                        "Configuration", "Release"
                        "TargetFrameworkVersion", f.Identifier.MSBuild
                    ]
         }
        build setParams "src/Elasticsearch.sln" |> ignore

    static member BuildFailure errors =
        raise (BuildException("The project build failed.", errors |> List.ofSeq))

    static member CompileDnx() =
        let projects = !! Paths.Source("*/project.json") 
                       |> Seq.map DirectoryName

        projects
        |> Seq.iter(fun project -> 
            //eventhough this says desktop it still builds all the tfm's it just hints wich installed dnx version to use
            Tooling.Dnu.Exec Tooling.DotNetRuntime.Desktop Build.BuildFailure project ["build"; (Paths.Quote project); "--configuration Release"; "--quiet"]
           )

        projects
        |> Seq.iter(fun project ->
            let projectName = (project |> directoryInfo).Name
            let outputFolder = Paths.Output(projectName)
            let srcFolder = Paths.BinFolder(projectName)
            CopyDir outputFolder srcFolder allFiles
        )







