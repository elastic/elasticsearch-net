#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake

#load @"Paths.fsx"

open System
open System.IO
open System.Diagnostics
open Paths

module Profiler =
    let private profiledApp = sprintf "%s/%s" (Paths.BinFolder("Performance/Profiling")) "Profiling.exe"
    let private snapShotOutput = Paths.Output("ProfilingSnapshot.dtp")
    let private snapShotStatsOutput = Paths.Output("ProfilingSnapshotStats.html")
    let private profileOutput = Paths.Output("ProfilingReport.xml")
    let private patternInput = Paths.Build("profiling/pattern.xml")

    let Run() = 
        // Profile, extract Stats, create Report
        Tooling.DotTraceProfiler.Exec [@"/app=" + profiledApp; "/profiling_type=sampling"; snapShotOutput; @"/timeout=600"; @"/use_api"; @"/transparent_exit_code"]
        Tooling.DotTraceSnapshotStats.Exec [snapShotOutput; snapShotStatsOutput; @"/full"]
        Tooling.DotTraceReporter.Exec [@"/reporting"; snapShotOutput; patternInput; profileOutput]
    
module Benchmarker =
   let private benchmarkingApp = sprintf "%s/%s" (Paths.BinFolder("Performance/Benchmarking")) "Benchmarking.exe" 

   let Run() =
        Tooling.execProcessWithTimeout benchmarkingApp ["-i false"; "-t 5"] (TimeSpan.FromMinutes 10.) |> ignore
