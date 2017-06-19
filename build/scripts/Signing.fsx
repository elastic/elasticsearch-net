#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Paths.fsx"
#nowarn "0044" //TODO sort out FAKE 5

open System
open Fake 
    
open Paths
open Projects

module StrongName = 
    let private sn = if isMono then "sn" else Paths.CheckedInTool("sn/sn.exe")
    let private keyFile =  Paths.Keys("keypair.snk");
    let private oficialToken = "96c599bbe3e70f5d"

    let private validate dll name = 
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
        
    let ValidateDllsInNugetPackage () = 
        for p in DotNetProject.AllPublishable do
            for f in DotNetFramework.All do 
                let name = p.Name
                let folder = Paths.IncrementalOutputFolder p f
                let dll = sprintf "%s/%s.dll" folder name
                match fileExists dll with
                | true -> validate dll name 
                | _ -> failwithf "Attemped to verify signature of %s but it was not found!" dll