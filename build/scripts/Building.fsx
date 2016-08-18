#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Paths.fsx"

open Fake 

open Paths
open Projects
open Tooling;

type Build() = 

    static let runningRelease = getBuildParam "target" = "release"

    static let compileCore() =
        DotNetProject.AllPublishable
        |> Seq.iter(fun p -> 
            let path = Paths.ProjectJson p.Name
            let o = Paths.ProjectOutputFolder p DotNetFramework.NetStandard1_3
            DotNet.Exec ["restore"; path; "--verbosity Warning"]
            DotNet.Exec ["build"; path; "--configuration Release"; "-o"; o; "-f"; DotNetFramework.NetStandard1_3.Identifier.MSBuild]
        )

    static let compileDesktop target =
        MsBuild.Build(target, DotNetFramework.Net45.Identifier)
        MsBuild.Build(target, DotNetFramework.Net46.Identifier)

    static let gitLink() =
        DotNetProject.AllPublishable
        |> Seq.iter(fun p ->
            let projectName = (p.Name |> directoryInfo).Name
            let link framework = 
                //TC sets these so gitlink does not need to recreate
                let branch = environVarOrNone "GITBRANCH"
                let commit = environVarOrNone "GITCOMMIT"
                let args = Seq.empty
                           |> match branch with
                              | Some b -> Seq.append ["-b"; b] 
                              | None -> Seq.append Seq.empty
                           |> match commit with
                              | Some c -> Seq.append ["-s"; c] 
                              | None -> Seq.append Seq.empty
                           |> Seq.append ["."; "-u"; Paths.Repository; "-d"; (Paths.ProjectOutputFolder p framework); "-include"; projectName]
                GitLink.Exec args 
                |> ignore
            link DotNetFramework.Net45
            link DotNetFramework.Net46
            link DotNetFramework.NetStandard1_3
        )
        
    static let compile target = 
        compileDesktop target
        //we only need this output when doing a release otherwise depend on test to validate the build
        if runningRelease then compileCore()
        if not isMono && runningRelease then gitLink()

    static member QuickCompile() = compile "Build"

    static member Compile() = compile "Rebuild"

    static member Clean() =
        CleanDir Paths.BuildOutput
        DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))