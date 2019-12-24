namespace Scripts

open Fake.Core
open Commandline

module Differ =    
    let Run args =
       Tooling.DotNet.Exec ["tool"; "restore"]       
       let differ = "assembly-differ"
       let args = args |> String.concat " "
       let command = sprintf @"%s %s -o ../../%s" differ args Paths.BuildOutput
       Tooling.DotNet.ExecIn Paths.TargetsFolder [command] |> ignore
