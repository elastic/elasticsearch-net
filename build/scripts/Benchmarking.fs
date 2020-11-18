// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System.IO
open Commandline

module Benchmarker =

    let private testsProjectDirectory = Path.GetFullPath(Paths.TestsSource("Tests.Benchmarking"))

    let Run args =
        
        let url = match args.CommandArguments with | Benchmark b -> Some b.Endpoint | _ -> None
        let username = match args.CommandArguments with | Benchmark b -> b.Username | _ -> None
        let password = match args.CommandArguments with | Benchmark b -> b.Password | _ -> None
        let runInteractive = not args.NonInteractive
        let credentials  = (username, password)
        let runCommandPrefix = "run -f net5.0 -c Release"
        let runCommand =
            match (runInteractive, url, credentials) with
            | (false, Some url, (Some username, Some password)) -> sprintf "%s -- --all \"%s\" \"%s\" \"%s\"" runCommandPrefix url username password
            | (false, Some url, _) -> sprintf "%s -- --all \"%s\"" runCommandPrefix url
            | (false, _, _) -> sprintf "%s -- --all" runCommandPrefix 
            | (true, _, _) -> runCommandPrefix
            
        Tooling.DotNet.ExecIn testsProjectDirectory [runCommand] |> ignore
