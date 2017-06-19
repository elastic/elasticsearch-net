#I @"../../packages/build/FAKE/tools"
#r "../../packages/build/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "../../packages/build/NEST/lib/net45/Nest.dll"
#r "../../packages/build/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
#r "../../packages/build/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r @"FakeLib.dll"
#r @"System.Xml.Linq.dll"
#nowarn "0044" //TODO sort out FAKE 5

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open System
open System.IO
open System.Diagnostics
open System.Xml
open System.Xml.Serialization
open System.Xml.Linq

open FSharp.Data

open Fake
open Git.Branches
open Nest
open Elasticsearch.Net
open Newtonsoft.Json

open Paths
open Projects
open Tooling

module Profiler =

    type Profile = XmlProvider<"../../build/profiling/profile-example.xml">

    type Function(id:string, fqn:string, totalTime:int, ownTime:int, calls:int, instances:int) =
        member val Id = id with get, set
        member val Fqn = fqn with get, set
        member val TotalTime = totalTime with get, set
        member val OwnTime = ownTime with get, set
        member val Calls = calls with get, set
        member val Instances = instances with get, set
    
    type Report(name:string, date:DateTime, commit:string, functions:Function list) = 
        member val Name = name with get, set
        member val Date = date with get, set
        member val Commit = commit with get, set
        member val Functions = functions with get, set

    let private project = "Tests"
    let private profiledApp = sprintf "%s/%s/%s.exe" (Paths.Output("v4.6")) project project
    let private snapShotOutput = Paths.Output("ProfilingSnapshot.dtp")
    let private snapShotStatsOutput = Paths.Output("ProfilingSnapshotStats.html")
    let private profileOutput = Paths.Output("ProfilingReport.xml")
    let private patternInput = Paths.Build("profiling/pattern.xml")

    let private element name children =
        XElement(XName.Get name, (children:XObject seq)) :> XObject

    let private attribute name value =
        XAttribute(XName.Get name, value) :> XObject

    let Run() = 
        let date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffz")
        Tooling.execProcessWithTimeout profiledApp ["Profile";"Class";getBuildParam "testfilter"] (TimeSpan.FromMinutes 30.) |> ignore
        trace "Profiling finished."

        let performanceOutput = Paths.Output("profiling/performance") |> directoryInfo
        let memoryOutput = Paths.Output("profiling/memory") |> directoryInfo

        if (directoryExists memoryOutput.FullName) then
            // DotMemory has a bug whereby it needs to open the dmrs zips, but is restricted to only open *.dmrs files.
            // In this case, we simply remove the zip extension
            for snapshot in Directory.EnumerateFiles(memoryOutput.FullName, "*.dmrs.zip", SearchOption.AllDirectories) do
                Rename (snapshot.Replace(".zip", "")) snapshot

        if (directoryExists performanceOutput.FullName) then
            for snapshot in Directory.EnumerateFiles(performanceOutput.FullName, "*.dtp", SearchOption.AllDirectories) do
                let snapshotPath = snapshot |> fileInfo
                let snapShotName = snapshotPath.Directory.FullName
                                        .Replace(performanceOutput.FullName, "")
                                        .Replace(@"\", ".")
                                        .TrimStart('.')
                let snapShotFileName = snapShotName + ".xml"                     
                let snapshotReport = combinePaths snapshotPath.Directory.FullName snapShotFileName

                Tooling.DotTraceSnapshotStats.Exec [snapshotPath.FullName; (combinePaths Paths.BuildOutput (snapShotName + ".html")); @"/full"]
                Tooling.DotTraceReporter.Exec [@"/reporting"; snapshotPath.FullName; patternInput; snapshotReport]

                let report = XDocument.Load snapshotReport
                let reportElem = report.Element(XName.Get "Report")
                let commit = getSHA1 "." "HEAD"
                reportElem.Add (attribute "Name" snapShotName)
                reportElem.Add (attribute "Date" date)
                reportElem.Add (attribute "Commit" commit)
                report.Save snapshotReport

                if fileExists profileOutput = false then
                    let reports = element "Reports" [ reportElem ]
                    let doc = new XDocument([| reports |])
                    doc.Save profileOutput
                else
                    let reports = XDocument.Load profileOutput
                    let reportsElem = reports.Element(XName.Get "Reports")
                    reportsElem.Add reportElem
                    reports.Save profileOutput

    let IndexResults url =
        if (String.IsNullOrEmpty url = false) && (fileExists profileOutput) then
            trace "Indexing profile results into Elasticsearch"
            let uri = new Uri(url)
            let client = new ElasticClient(uri)
            use file = File.OpenRead profileOutput
            let profile = Profile.Load file
            
            for report in profile.Reports do
                let functions = report.Functions
                                |> Seq.map(fun f -> 
                                    new Function(f.Id, f.Fqn, f.TotalTime, f.OwnTime, f.Calls, f.Instances))
                                |> Seq.toList

                let reportDoc = new Report(report.Name, report.Date, report.Commit, functions)

                let indexName = IndexName.op_Implicit("reports")
                let typeName = TypeName.op_Implicit("report")
                let indexExists = client.IndexExists(Indices.op_Implicit(indexName)).Exists

                if indexExists = false then
                    let createIndex = client.CreateIndex(indexName, fun c -> 
                        c.Mappings(fun m -> 
                                m.Map<Report>(fun mm ->
                                    mm.AutoMap()
                                      .Properties(fun p -> 
                                          p.Nested<Function>(fun n ->
                                              n.AutoMap().Name(PropertyName.op_Implicit("functions")) :> INestedProperty
                                          ) :> IPromise<IProperties>
                                      ) :> ITypeMapping
                                ) :> IPromise<IMappings>
                            ) :> ICreateIndexRequest
                        )

                    if createIndex.IsValid = false then
                        raise (Exception("Unable to create index into Elasticsearch"))


                let indexRequest = new IndexRequest<Report>(indexName, typeName)
                indexRequest.Document <- reportDoc

                let indexResponse = client.Index(indexRequest)

                if indexResponse.IsValid = false then
                    raise (Exception("Unable to index report into Elasticsearch"))
