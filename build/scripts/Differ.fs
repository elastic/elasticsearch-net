namespace Scripts

open System
open System.IO
open Fake.Core
open Fake.IO
open Commandline

module Differ =

    let Run args =
       let differ = Paths.PaketDotNetGlobalTool "assembly-differ" @"tools\netcoreapp2.1\any\assembly-differ.dll"
       let args = args.RemainingArguments |> String.concat " "
       let command = sprintf @"%s %s" differ args
       Environment.setEnvironVar "NUGET" Tooling.nugetFile
       Tooling.DotNet.Exec [command] |> ignore
