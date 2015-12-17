#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
open Fake

#load @"Paths.fsx"

open System
open System.IO
open System.Diagnostics
open Paths

module Profiler =
    let private profiledApp = sprintf "%s/%s" (Paths.BinFolder("Profiling")) "Profiling.exe"
    let private snapShotOutput = Paths.Output("ProfilingSnapshot.dtp")
    let private snapShotStatsOutput = Paths.Output("ProfilingSnapshotStats.html")
    let private profileOutput = Paths.Output("ProfilingReport.xml")
    let private patternInput = sprintf "%s/profiling/pattern.xml" Paths.BuildFolder

    let Snapshot() = 
        Tooling.DotTraceProfiler.Exec [@"/app=" + profiledApp; "/profiling_type=tracing"; snapShotOutput; @"/timeout=600"; @"/transparent_exit_code"]
        Tooling.DotTraceSnapshotStats.Exec [snapShotOutput; snapShotStatsOutput; @"/full"]
    
    let Report() =
        Tooling.DotTraceReporter.Exec [@"/reporting"; snapShotOutput; patternInput; profileOutput]


