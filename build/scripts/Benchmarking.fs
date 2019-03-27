namespace Scripts

open Fake
open System.IO
open Paths
open Commandline

module Benchmarker =

    let private testsProjectDirectory = Path.GetFullPath(Paths.TestsSource("Tests.Benchmarking"))

    let Run() =
        let runInteractive = not Commandline.nonInteractive
        let url = getBuildParam "elasticsearch"
        let username = getBuildParam "username"
        let password = getBuildParam "password"
        let hasUrl = not <| isNullOrEmpty url
        let hasCredentials = not <| (isNullOrEmpty username && isNullOrEmpty password)
        let runCommandPrefix = "run -f netcoreapp2.1 -c Release"
        let runCommand =
            match (runInteractive, hasUrl, hasCredentials) with
            | (false, true, true) -> sprintf "%s -- --all \"%s\" \"%s\" \"%s\"" runCommandPrefix url username password
            | (false, true, false) -> sprintf "%s -- --all \"%s\"" runCommandPrefix url
            | (false, _, _) -> sprintf "%s -- --all" runCommandPrefix 
            | (true, _, _) -> runCommandPrefix
        
        DotNetCli.RunCommand(fun p -> { p with WorkingDir = testsProjectDirectory }) runCommand
