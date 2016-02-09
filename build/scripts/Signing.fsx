#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
#load @"Projects.fsx"
open System
open Fake 
open Paths
open Projects

type Sign() = 
    static let sn = if isMono then "sn" else Paths.CheckedInTool("sn/sn.exe")
    static let keyFile =  Paths.Keys("keypair.snk");
    static let oficialToken = "96c599bbe3e70f5d"

    static let validate dll name = 
        let out = (ExecProcessAndReturnMessages(fun p ->
                    p.FileName <- sn
                    p.Arguments <- sprintf @"-v %s" dll
                  ) (TimeSpan.FromMinutes 5.0))
        
        let valid = (out.ExitCode, out.Messages.FindIndex(fun s -> s.Contains("is valid")))
        match valid with
        | (0, i) when i >= 0 -> trace (sprintf "%s was signed correctly" name) 
        | (_, _) -> failwithf "{0} was not validly signed"
        
        let out = (ExecProcessAndReturnMessages(fun p ->
                    p.FileName <- sn
                    p.Arguments <- sprintf @"-T %s" dll
                  ) (TimeSpan.FromMinutes 5.0))
        
        let tokenMessage = (out.Messages.Find(fun s -> s.Contains("Public key token is")));
        let token = (tokenMessage.Replace("Public key token is", "")).Trim();
    
        let valid = (out.ExitCode, token)
        match valid with
        | (0, t) when t = oficialToken  -> 
          trace (sprintf "%s was signed with official key token %s" name t) 
        | (_, t) -> traceFAKE "%s was not signed with the official token: %s but %s" name oficialToken t
        
    static member CreateKeys () = 
        ExecProcess(fun p ->
          p.FileName <- sn
          p.Arguments <- sprintf @"-k %s" keyFile
        ) (TimeSpan.FromMinutes 5.0) |> ignore
         
        ExecProcess(fun p ->
          p.FileName <- sn
          p.Arguments <- sprintf @"-p %s %s" keyFile (Paths.Keys("public.snk"))
        ) (TimeSpan.FromMinutes 5.0) |> ignore

    static member ValidateNugetDllAreSignedCorrectly() = 
        for p in DotNetProject.All do
            let name = p.ProjectName.Nuget
            let outputFolder = Paths.Output(name)
            match p with
            | Project p -> 
                (outputFolder |> directoryInfo).GetDirectories()
                |> Seq.iter(fun frameworkDir ->
                    let frameworkName = frameworkDir.Name
                    let dll = sprintf "%s/%s/%s.dll" outputFolder frameworkName name
                    if fileExists dll then validate dll name
                )

    static member CreateKeysIfAbsent() =
        if not (directoryExists Paths.KeysFolder) then CreateDir Paths.KeysFolder
        if not (fileExists keyFile) then Sign.CreateKeys()

