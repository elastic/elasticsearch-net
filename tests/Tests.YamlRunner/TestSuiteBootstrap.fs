// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.TestSuiteBootstrap

open System
open System.Linq
open Elastic.Transport
open Elasticsearch.Net.Specification.CatApi
open Elasticsearch.Net.Specification.ClusterApi
open Elasticsearch.Net.Specification.IndicesApi
open Tests.YamlRunner.Models
open System.Collections.Generic

let DefaultSetup : Operation list = [Actions("Setup", fun (client, suite) ->
    let firstFailure (responses:DynamicResponse seq) =
            responses
            |> Seq.filter (fun r -> not r.Success && r.HttpStatusCode <> Nullable.op_Implicit 404)
            |> Seq.tryHead

    let isXPackName name =
        match name with
        | ".watches"
        | "logstash-index-template"
        | ".logstash-management"
        | "security_audit_log"
        | ".slm-history"
        | ".async-search"
        | "saml-service-provider"
        | "ilm-history"
        | "logs"
        | "logs-settings"
        | "logs-mappings"
        | "metrics"
        | "data-streams-mappings"
        | "metrics-mappings"
        | "metrics-mappings"
        | "synthetics"
        | "synthetics-settings"
        | "synthetics-mappings"
        | ".snapshot-blob-cache"
        | ".deprecation-indexing-template" -> false
        | ".deprecation-indexing-mappings" -> false
        | ".deprecation-indexing-settings" -> false
        | s when s.StartsWith(".monitoring") -> false
        | s when s.StartsWith(".watch") -> false
        | s when s.StartsWith(".data-frame") -> false
        | s when s.StartsWith(".ml-") -> false
        | s when s.StartsWith(".transform-") -> false
        | _ -> true
            
    let getAndDeleteFilter (setup:_ -> DynamicResponse) (delete:(_ -> DynamicResponse)) filter = 
        setup().Body
        |> Seq.map (fun kv ->
            match filter with
            | Some filter ->
                match filter kv.Key kv.Value with
                | false -> Some <| delete(kv.Key, kv.Value)
                | _ -> None
            | None -> Some <| delete(kv.Key, kv.Value)
        )
        |> Seq.choose id
        |> Seq.toList
        
    let getAndDelete setup delete = getAndDeleteFilter setup delete None
    
    let wipeRollupJobs () = 
        let getJobs = client.Rollup.GetJob<DynamicResponse> "_all"
        let deleteJobs =
            getJobs.Get<string[]> "jobs.config.id"
            |> Seq.collect (fun jobId -> [
                 client.Rollup.StopJob<DynamicResponse>(jobId)
                 client.Rollup.DeleteJob<DynamicResponse>(jobId)
            ])
            |> Seq.toList
        [getJobs] @ deleteJobs
        
    let waitForPendingTasks (filter:string) =
        let call () =   
            client.Cat.Tasks<StringResponse>(CatTasksRequestParameters(Detailed=true)).Body.Split("\n")
            |> Array.filter (fun l -> l.Contains(filter, StringComparison.OrdinalIgnoreCase))
            |> Array.length
        let start = DateTime.UtcNow
        let e = start.AddSeconds(30.)
        while (call() > 0 && DateTime.UtcNow < e) do ignore()
        
        
    let waitForPendingRollupTasks () = waitForPendingTasks "xpack/rollup/job"
    
    let deleteAllSLMPolicies () =
        getAndDelete
           (fun _ -> client.SnapshotLifecycleManagement.GetSnapshotLifecycle<DynamicResponse>())
           (fun (name, _) -> client.SnapshotLifecycleManagement.DeleteSnapshotLifecycle<DynamicResponse> name)
    
    let wipeSnapshots () = 
        getAndDelete
           (fun _ -> client.Snapshot.GetRepository<DynamicResponse>())
           (fun (name, value) ->
                if value.Get<string> "type" = "fs" then 
                    client.Snapshot.Delete<DynamicResponse> (name, "*") |> ignore
                client.Snapshot.DeleteRepository<DynamicResponse> name
           )
    
    let wipeDataStreams () = 
        client.Indices.DeleteDataStreamForAll<DynamicResponse> "*" 
    
    let wipeAllIndices () =
        let dp = DeleteIndexRequestParameters()
        dp.SetQueryString("expand_wildcards", "all")
        client.Indices.Delete<DynamicResponse>("*,-.ds-.watcher-history-*,-.ds-ilm-history-*", dp)
        
    let deleteTemplates () =
        client.Cat.Templates<StringResponse>("*", CatTemplatesRequestParameters(Headers=["name"].ToArray()))
            .Body.Split("\n")
            |> Seq.map(fun line -> line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            |> Seq.filter(fun line -> line.Length = 1)
            |> Seq.map(fun tokens -> tokens.[0].Trim())
            |> Seq.filter isXPackName
            //TODO template does not accept comma separated list but is documented as such
            |> Seq.map(fun template ->
                let result = client.Indices.DeleteTemplateForAll<DynamicResponse>(template)
                match result.Success with
                | true -> result
                | false -> client.Indices.DeleteTemplateV2ForAll<DynamicResponse>(template)
            )
            |> Seq.toList
            
    let deleteComponentTemplates () =
        let result = client.Cluster.GetComponentTemplate<DynamicResponse>()
        let names = result.Get<string[]>("component_templates.name")
        names
        |> Seq.filter isXPackName
        |> Seq.map client.Cluster.DeleteComponentTemplate<DynamicResponse>
        |> Seq.toList
        
    let wipeTemplateForXPack () = deleteTemplates() @ deleteComponentTemplates()
    
    let wipeClusterSettings () =
        let settings = client.Cluster.GetSettings<DynamicResponse>(ClusterGetSettingsRequestParameters(FlatSettings=true))
        let payload =
            [ 
                "transient", dict [ for v in settings.Get<DynamicDictionary>("transient").Keys -> v, null ];
                "persistent", dict [ for v in settings.Get<DynamicDictionary>("persistent").Keys -> v, null ];
            ]
            |> dict
        if payload.["transient"].Values.Count > 0 || payload.["transient"].Values.Count > 0 then
            client.Cluster.PutSettings<DynamicResponse>(PostData.Serializable(payload))
        else 
            settings
    
    let deleteAllILMPolicies () =
        let preserved = [
            "ilm-history-ilm-policy";
            "slm-history-ilm-policy";
            "watch-history-ilm-policy"; 
            "ml-size-based-ilm-policy"; 
            "logs"; 
            "metrics"
        ]
        getAndDeleteFilter
           (fun _ -> client.Snapshot.GetRepository<DynamicResponse>())
           (fun (name, value) ->
                if value.Get<string> "type" = "fs" then 
                    client.Snapshot.Delete<DynamicResponse> (name, "*") |> ignore
                client.Snapshot.DeleteRepository<DynamicResponse> name
           )
           (Some (fun name value -> preserved |> List.contains name ))
           
    let deleteAllAutoFollowPatterns () = 
        let result = client.CrossClusterReplication.GetAutoFollowPattern<DynamicResponse>()
        let names = result.Get<string[]>("patterns.name")
        names
        |> Seq.map client.CrossClusterReplication.DeleteAutoFollowPattern<DynamicResponse>
        |> Seq.toList
        
    let deleteAllTasks () = 
        let getJobs = client.Tasks.List<DynamicResponse> ()
        let cancelJobs = 
            getJobs.Get<DynamicDictionary> "nodes"
            |> Seq.collect(fun kv -> kv.Value.Get<DynamicDictionary> "tasks")
            |> Seq.map (fun kv ->
                match kv.Value.Get<bool> "cancellable" with
                | true -> Some <| client.Tasks.Cancel<DynamicResponse>(kv.Key)
                | _ -> None
            )
            |> Seq.choose id
            |> Seq.toList
        
        [getJobs] @ cancelJobs
    
    let stopTransforms () = 
        let transforms = client.Transform.Get<DynamicResponse> "_all"
        let stopTransforms =
            transforms.Get<string[]> "transforms.id"
            |> Seq.collect (fun id -> [
                 client.Transform.Stop<DynamicResponse> id
                 client.Transform.Delete<DynamicResponse> id
            ])
            |> Seq.toList
        [transforms] @ stopTransforms
        
    let closeMLJobs () = 
        let closeJobs = client.MachineLearning.CloseJob<DynamicResponse>("_all", PostData.Empty)
        let getJobs = client.MachineLearning.GetJobs<DynamicResponse> "_all"
        let deleteJobs =
            getJobs.Get<string[]> "jobs.job_id"
            |> Seq.map (fun jobId -> client.MachineLearning.DeleteJob<DynamicResponse>(jobId))
            |> Seq.toList
        [closeJobs; getJobs] @ deleteJobs
        
    let deleteMLDatafeeds () =
        let stopFeeds = client.MachineLearning.StopDatafeed<DynamicResponse>("_all", null)
        let getFeeds = client.MachineLearning.GetDatafeeds<DynamicResponse> ()
        let deleteFeeds =
            getFeeds.Get<string[]> "datafeeds.datafeed_id"
            |> Seq.map (fun jobId -> client.MachineLearning.DeleteDatafeed<DynamicResponse>(jobId))
            |> Seq.toList
        [stopFeeds; getFeeds] @ deleteFeeds
        
    let waitForClusterStateUpdatesToFinish () =
        let call () =   
            client.Cluster.PendingTasks<DynamicResponse>().Get<string[]>("tasks.source")
            |> Array.length
        let start = DateTime.UtcNow
        let e = start.AddSeconds(30.)
        while (call() > 0 && DateTime.UtcNow < e) do ignore()
    
    firstFailure <| seq {
        if suite = Platinum then
            yield! wipeRollupJobs()
            waitForPendingRollupTasks()
            yield! deleteAllSLMPolicies()
        
        yield! wipeSnapshots()
        
        if suite = Platinum then
            yield wipeDataStreams()
        
        yield wipeAllIndices()
        
        yield! wipeTemplateForXPack()
        
        yield wipeClusterSettings()
        
        if suite = Platinum then
            yield! deleteAllILMPolicies()
            yield! deleteAllAutoFollowPatterns()
            yield! deleteAllTasks()
         
        // deleting feeds before jobs is important
        yield! deleteMLDatafeeds()
            
        yield client.IndexLifecycleManagement.RemovePolicy<DynamicResponse>("_all")
        
        yield! closeMLJobs()
            
        yield! deleteAllTasks()
        
        yield! stopTransforms()
            
        if suite = Platinum then
            let data = PostData.String @"{""password"":""x-pack-test-password"", ""roles"":[""superuser""]}"
            yield client.Security.PutUser<DynamicResponse>("x_pack_rest_user", data)
        
        waitForClusterStateUpdatesToFinish()
    }
    
)]
