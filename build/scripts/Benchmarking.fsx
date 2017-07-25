#r "../../packages/build/NEST/lib/net45/Nest.dll"
#r "../../packages/build/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
#r "../../packages/build/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r "../../packages/build/FSharp.Data/lib/net40/FSharp.Data.dll"
#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#nowarn "0044" //TODO sort out FAKE 5

open Fake

#load @"Paths.fsx"

open System
open System.IO
open System.Linq
open System.Diagnostics
open Paths

open FSharp.Data

open Nest
open Elasticsearch.Net
open Newtonsoft.Json
open Git.Branches

module Benchmarker =

    let pipelineName = "benchmark-pipeline"
    let indexName = IndexName.op_Implicit("benchmark-reports")
    let typeName = TypeName.op_Implicit("report")
    
    type Memory(gen0Collections:int, gen1Collections: int, gen2Collections: int, totalOperations:int64, bytesAllocatedPerOperation:int64) =
        member val Gen0Collections=gen0Collections with get, set
        member val Gen1Collections=gen1Collections with get, set
        member val Gen2Collections=gen2Collections with get, set
        member val TotalOperations=totalOperations with get, set
        member val BytesAllocatedPerOperation=bytesAllocatedPerOperation with get, set

    type ChronometerFrequency(hertz:double) =
        member val Hertz=hertz with get, set
   
    type HostEnvironmentInfo(benchmarkDotNetCaption:string, benchmarkDotNetVersion:string, osVersion: string, processorName:string,
                             processorCount:int, runtimeVersion:string, architecture:string, hasAttachedDebugger:bool, hasRyuJit:bool,
                             configuration:string, jitModules:string, dotnetCliVersion:string, chronometerFrequency:ChronometerFrequency,
                             hardwareTimerKind:string) =
        member val BenchmarkDotNetCaption=benchmarkDotNetCaption with get, set
        member val BenchmarkDotNetVersion=benchmarkDotNetVersion with get, set
        member val OsVersion=osVersion with get, set
        member val ProcessorName=processorName with get, set
        member val ProcessorCount=processorCount with get, set
        member val RuntimeVersion=runtimeVersion with get, set
        member val Architecture=architecture with get, set
        member val HasAttachedDebugger=hasAttachedDebugger with get, set
        member val HasRyuJit=hasRyuJit with get, set
        member val Configuration=configuration with get, set
        member val JitModules=jitModules with get, set
        member val DotNetCliVersion=dotnetCliVersion with get, set
        member val ChronometerFrequency=chronometerFrequency with get, set
        member val HardwareTimerKind=hardwareTimerKind with get, set

    type ConfidenceInterval(n:int, mean: double, standardError:double, level:int, margin:double, lower:double, upper:double) =
        member val N=n with get, set
        member val Mean=mean with get, set
        member val StandardError=standardError with get, set
        member val Level=level with get, set
        member val Margin=margin with get, set
        member val Lower=lower with get, set
        member val Upper=upper with get, set

    type Percentiles (p0:double, p25:double, p50:double, p67:double, p80:double, p85:double, p90:double, p95:double, p100:double) =
        member val P0=p0 with get, set
        member val P25=p25 with get, set
        member val P50=p50 with get, set
        member val P67=p67 with get, set
        member val P80=p80 with get, set
        member val P85=p85 with get, set
        member val P90=p90 with get, set
        member val P95=p95 with get, set
        member val P100=p100 with get, set

    type Statistics(n:int, min:double, lowerFence:double, q1:double, median:double, mean:double, q3:double, upperFence:double, max:double,
                    interquartileRange:double, outliers:double list, standardError:double, variance:double, standardDeviation:double,
                    skewness:double, kurtosis:double, confidenceInterval:ConfidenceInterval, percentiles:Percentiles) =
        member val N=n with get, set
        member val Min=min with get, set
        member val LowerFence=lowerFence with get, set
        member val Q1=q1 with get, set
        member val Median=median with get, set
        member val Mean=mean with get, set
        member val Q3=q3 with get, set
        member val UpperFence=upperFence with get, set
        member val Max=max with get, set
        member val InterquartileRange=interquartileRange with get, set
        member val Outliers=outliers with get, set
        member val StandardError=standardError with get, set
        member val Variance=variance with get, set
        member val StandardDeviation=standardDeviation with get, set
        member val Skewness=skewness with get, set
        member val Kurtosis=kurtosis with get, set
        member val ConfidenceInterval=confidenceInterval with get, set
        member val Percentiles=percentiles with get, set

    type Benchmark(displayInfo:string, namespyce:string, tipe:string, method:string, methodTitle:string, parameters:string,
                   statistics:Statistics, memory:Memory) = 
        member val DisplayInfo=displayInfo with get, set
        member val Namespace=namespyce with get, set
        member val Type=tipe with get, set
        member val Method=method with get, set
        member val MethodTitle=methodTitle with get, set
        member val Parameters=parameters with get, set
        member val Statistics=statistics with get, set
        member val Memory=memory with get, set

    type Report(title: string, totalTime:TimeSpan, date:DateTime, commit:string, host:HostEnvironmentInfo, benchmarks:Benchmark list) =
        member val Title = title with get, set
        member val TotalTime = totalTime with get, set
        member val Date = date with get, set
        member val Commit = commit with get, set
        member val HostEnvironmentInfo = host with get, set
        member val Benchmarks = benchmarks with get, set

    let private testsProjectDirectory = Path.GetFullPath(Paths.Source("Tests"))
    let private benchmarkOutput = Path.GetFullPath(Paths.Output("benchmarks")) |> directoryInfo
    let private copyToOutput file = CopyFile benchmarkOutput.FullName file

    let Run(runInteractive:bool) =

        ensureDirExists benchmarkOutput

        let projectJson = testsProjectDirectory @@ "project.json"

        // running benchmarks can timeout so clean up any generated benchmark files
        try
            if runInteractive then
                DotNetCli.RunCommand(fun p ->
                    { p with
                        WorkingDir = testsProjectDirectory
                    }) "run -f net46 -c Release Benchmark"
             else
                DotNetCli.RunCommand(fun p ->
                    { p with
                        WorkingDir = testsProjectDirectory
                    }) "run -f net46 -c Release Benchmark non-interactive"
        finally
            let benchmarkOutputFiles =
                let output = combinePaths testsProjectDirectory "BenchmarkDotNet.Artifacts"
                Directory.EnumerateFiles(output, "*.*", SearchOption.AllDirectories)
                |> Seq.toList

            for file in benchmarkOutputFiles do copyToOutput file
            DeleteFiles benchmarkOutputFiles

    let IndexResult (client:ElasticClient, file:string, date:DateTime, commit:string, indexName, typeName) =

        trace ("Indexing report " + file + " into Elasticsearch")

        let document = JsonConvert.DeserializeObject<Report>(File.ReadAllText(file))
        document.Date <- date
        document.Commit <- commit

        let indexRequest = new IndexRequest<Report>(indexName, typeName)
        indexRequest.Document <- document
        indexRequest.Pipeline <- pipelineName

        let indexResponse = client.Index(indexRequest)

        if indexResponse.IsValid = false then
                raise (Exception("Unable to index report into Elasticsearch: " + indexResponse.ServerError.Error.ToString()))

    let IndexResults url =
        if (String.IsNullOrEmpty url = false) then
            trace "Indexing benchmark results into Elasticsearch"
            
            let date = DateTime.UtcNow
            let commit = getSHA1 "." "HEAD"

            let benchmarkJsonFiles =
                Directory.EnumerateFiles(benchmarkOutput.FullName, "*-custom.json", SearchOption.AllDirectories)
                |> Seq.toList

            let uri = new Uri(url)
            let client = new ElasticClient(uri)

            let indexExists = client.IndexExists(Indices.op_Implicit(indexName)).Exists

            if indexExists = false then
                let createIndex = client.CreateIndex(indexName, fun c -> 
                    c.Mappings(fun m -> 
                            m.Map<Report>(fun mm ->
                                mm.AutoMap()
                                  .Properties(fun p -> 
                                      p.Nested<Benchmark>(fun n ->
                                          n.AutoMap().Name(PropertyName.op_Implicit("benchmarks")) :> INestedProperty
                                      ) :> IPromise<IProperties>
                                  ) :> ITypeMapping
                            ) :> IPromise<IMappings>
                        ) :> ICreateIndexRequest
                    )

                if createIndex.IsValid = false then
                    raise (Exception("Unable to create index into Elasticsearch"))

            let processor = new GrokProcessor();
            processor.Field <- new Field("_ingest._value.displayInfo")
            processor.Patterns <- ["%{WORD:_ingest._value.class}.%{DATA:_ingest._value.method}: Job-%{WORD:_ingest._value.jobName}\\(Jit=%{WORD:_ingest._value.jit}, Runtime=%{WORD:_ingest._value.clr}, LaunchCount=%{NUMBER:_ingest._value.launchCount}, RunStrategy=%{WORD:_ingest._value.runStrategy}, TargetCount=%{NUMBER:_ingest._value.targetCount}, UnrollFactor=%{NUMBER:_ingest._value.unrollFactor}, WarmupCount=%{NUMBER:_ingest._value.warmupCount}\\)"]
            
            let forEachProcessor = new ForeachProcessor()
            forEachProcessor.Field <- new Field("benchmarks")
            forEachProcessor.Processor <- processor

            let request = new PutPipelineRequest(Id.op_Implicit(pipelineName))
            request.Description <- "Grok benchmark settings"
            request.Processors <- [forEachProcessor]

            let createPipeline = client.PutPipeline(request)

            if createPipeline.IsValid = false then
                raise (Exception("Unable to create pipeline"))
                  
            for file in benchmarkJsonFiles do IndexResult (client, file, date, commit, indexName, typeName)

            trace "Indexed benchmark results into Elasticsearch"