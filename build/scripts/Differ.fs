namespace Scripts

open System
open System.IO
open Fake.Core
open Fake.IO
open Commandline

module Differ =

    let Run args =
       let differ = "assembly-differ"
       let args = args.RemainingArguments |> String.concat " "
       let command = sprintf @"%s %s -o ../../%s" differ args Paths.BuildOutput
       Environment.setEnvironVar "NUGET" Tooling.nugetFile
       Tooling.DotNet.ExecIn Paths.TargetsFolder [command] |> ignore
