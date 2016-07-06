#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"
#load @"Projects.fsx"
open System

open Fake 

open Paths
open Projects

let gitLink pdbDir projectName =
    let exe = Paths.Tool("gitlink/lib/net45/GitLink.exe")
    ExecProcess(fun p ->
     p.FileName <- exe
     p.Arguments <- sprintf @". -u %s -d %s -include %s" Paths.Repository pdbDir projectName
    ) (TimeSpan.FromMinutes 5.0) |> ignore

type Build() = 

    static let allProjects = DotNetProject.All |> Seq.map(fun p -> p.Name)

    static let compileCore projects =
        projects
        |> Seq.iter(fun p -> 
            let path = (Paths.Quote (Paths.ProjectJson p))
            Tooling.DotNet.Exec Tooling.DotNetRuntime.Core Build.BuildFailure p ["restore"; path; "--verbosity Warning"]
            Tooling.DotNet.Exec Tooling.DotNetRuntime.Core Build.BuildFailure p ["build"; path; "--configuration Release"]
           )

    static let compileDesktop projects =
        projects
        |> Seq.iter(fun project ->
            Tooling.MsBuild.Exec (Paths.Net45BinFolder project) "Rebuild" Tooling.DotNetFramework.Net45.Identifier [Paths.CsProj(project)]
            Tooling.MsBuild.Exec (Paths.Net46BinFolder project) "Rebuild" Tooling.DotNetFramework.Net46.Identifier [Paths.CsProj(project)]
           )

    static let copyToOutput projects =
        projects
        |> Seq.iter(fun p ->
            let projectName = (p |> directoryInfo).Name
            let outputFolder = Paths.Output(projectName)
            let binFolder = Paths.BinFolder(projectName)
            if not isMono then
                match projectName with
                | "Nest" 
                | "Elasticsearch.Net" ->
                    gitLink (Paths.Net45BinFolder projectName) projectName
                    gitLink (Paths.Net46BinFolder projectName) projectName
                    gitLink (Paths.NetStandard13BinFolder projectName) projectName
                | _  -> ()
            CopyDir outputFolder binFolder allFiles
        )
        
    static member BuildFailure errors =
        raise (BuildException("The project build failed.", errors |> List.ofSeq))

    static member QuickCompile() = 
        compileDesktop allProjects
        compileCore allProjects

    static member Compile() =
        compileDesktop allProjects
        compileCore allProjects
        copyToOutput allProjects

    static member Clean() =
        CleanDir Paths.BuildOutput
        allProjects |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p))