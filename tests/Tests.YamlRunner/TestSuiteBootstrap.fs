// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.TestSuiteBootstrap

open System
open System.Linq

open Elastic.Transport
open Elasticsearch.Net
open Elasticsearch.Net.Specification.CatApi
open Elasticsearch.Net.Specification.ClusterApi
open Elasticsearch.Net.Specification.IndicesApi
open Tests.YamlRunner.Models

let DefaultSetup : Operation list = [Actions("Setup", fun (client, suite) ->
    let firstFailure (responses:DynamicResponse seq) =
            responses
            |> Seq.filter (fun r -> not r.Success && r.HttpStatusCode <> Nullable.op_Implicit 404)
            |> Seq.tryHead
    
    let deleteAll () =
        let dp = DeleteIndexRequestParameters()
        dp.SetQueryString("expand_wildcards", "open,closed,hidden")
        client.Indices.Delete<DynamicResponse>("*,-.ds-ilm-history-*", dp)
    let templates () =
        client.Cat.Templates<StringResponse>("*", CatTemplatesRequestParameters(Headers=["name";"order"].ToArray()))
            .Body.Split("\n")
            |> Seq.map(fun line -> line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            |> Seq.filter(fun line -> line.Length = 2)
            |> Seq.map(fun tokens -> tokens.[0], Int32.Parse(tokens.[1]))
            //assume templates with order 100 or higher are defaults
            |> Seq.filter(fun (_, order) -> order < 100)
            |> Seq.filter(fun (name, _) -> not(String.IsNullOrWhiteSpace(name)) && not(name.StartsWith(".")) && name <> "security-audit-log")
            //TODO template does not accept comma separated list but is documented as such
            |> Seq.map(fun (template, _) ->
                let result = client.Indices.DeleteTemplateForAll<DynamicResponse>(template)
                match result.Success with
                | true -> result
                | false -> client.Indices.DeleteTemplateV2ForAll<DynamicResponse>(template)
            )
            |> Seq.toList
                
    match suite with
    | Oss ->
        let snapshots =
            client.Cat.Snapshots<StringResponse>(CatSnapshotsRequestParameters(Headers=["id,repository"].ToArray()))
                .Body.Split("\n")
                |> Seq.map(fun line -> line.Split " ")
                |> Seq.filter(fun tokens -> tokens.Length = 2)
                |> Seq.map(fun tokens -> (tokens.[0].Trim(), tokens.[1].Trim()))
                |> Seq.filter(fun (id, repos) -> not(String.IsNullOrWhiteSpace(id)) && not(String.IsNullOrWhiteSpace(repos)))
                //TODO template does not accept comma separated list but is documented as such
                |> Seq.map(fun (id, repos) -> client.Snapshot.Delete<DynamicResponse>(repos, id))
                |> Seq.toList
                
        let deleteRepositories = client.Snapshot.DeleteRepository<DynamicResponse>("*")
        firstFailure <| [deleteAll()] @ templates() @ snapshots @ [deleteRepositories]
        
    | XPack ->
        firstFailure <| seq {
            //delete all templates
            
            yield! templates()
            
            yield client.Watcher.Delete<DynamicResponse>("my_watch")
            
            let deleteNonReserved (setup:_ -> DynamicResponse) (delete:(_ -> DynamicResponse)) = 
                setup().Dictionary
                |> Seq.map (fun kv ->
                    match kv.Value.Get<bool> "metadata._reserved" with
                    | false -> Some <| delete(kv.Key)
                    | _ -> None
                )
                |> Seq.choose id
                |> Seq.toList
            
            yield! //roles
                deleteNonReserved
                   (fun _ -> client.Security.GetRole<DynamicResponse>())
                   (fun role -> client.Security.DeleteRole<DynamicResponse> role)
                   
            yield! //users
                deleteNonReserved
                   (fun _ -> client.Security.GetUser<DynamicResponse>())
                   (fun user -> client.Security.DeleteUser<DynamicResponse> user)
            
            yield! //privileges
                deleteNonReserved
                   (fun _ -> client.Security.GetPrivileges<DynamicResponse>())
                   (fun priv -> client.Security.DeletePrivileges<DynamicResponse>(priv, "_all"))
                
            // deleting feeds before jobs is important
            let mlDataFeeds = 
                let stopFeeds = client.MachineLearning.StopDatafeed<DynamicResponse>("_all", null)
                let getFeeds = client.MachineLearning.GetDatafeeds<DynamicResponse> ()
                let deleteFeeds =
                    getFeeds.Get<string[]> "datafeeds.datafeed_id"
                    |> Seq.map (fun jobId -> client.MachineLearning.DeleteDatafeed<DynamicResponse>(jobId))
                    |> Seq.toList
                [stopFeeds; getFeeds] @ deleteFeeds
            yield! mlDataFeeds
                
            yield client.IndexLifecycleManagement.RemovePolicy<DynamicResponse>("_all")
            
            let mlJobs = 
                let closeJobs = client.MachineLearning.CloseJob<DynamicResponse>("_all", PostData.Empty)
                let getJobs = client.MachineLearning.GetJobs<DynamicResponse> "_all"
                let deleteJobs =
                    getJobs.Get<string[]> "jobs.job_id"
                    |> Seq.map (fun jobId -> client.MachineLearning.DeleteJob<DynamicResponse>(jobId))
                    |> Seq.toList
                [closeJobs; getJobs] @ deleteJobs
            yield! mlJobs
                
            let rollupJobs = 
                let getJobs = client.Rollup.GetJob<DynamicResponse> "_all"
                let deleteJobs =
                    getJobs.Get<string[]> "jobs.config.id"
                    |> Seq.collect (fun jobId -> [
                         client.Rollup.StopJob<DynamicResponse>(jobId)
                         client.Rollup.DeleteJob<DynamicResponse>(jobId)
                    ])
                    |> Seq.toList
                [getJobs] @ deleteJobs
            yield! rollupJobs
                
            let tasks =
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
            yield! tasks
            
            let transforms =
                let transforms = client.Transform.Get<DynamicResponse> "_all"
                let stopTransforms =
                    transforms.Get<string[]> "transforms.id"
                    |> Seq.collect (fun id -> [
                         client.Transform.Stop<DynamicResponse> id
                         client.Transform.Delete<DynamicResponse> id
                    ])
                    |> Seq.toList
                [transforms] @ stopTransforms
            yield! transforms
                
            let yellowStatus = Nullable.op_Implicit WaitForStatus.Yellow
            yield client.Cluster.Health<DynamicResponse>(ClusterHealthRequestParameters(WaitForStatus=yellowStatus))
            
            let indices =
                let dp = DeleteIndexRequestParameters()
                dp.SetQueryString("expand_wildcards", "open,closed,hidden")
                client.Indices.Delete<DynamicResponse>("*,-.ds-ilm-history-*", dp)
            yield indices
            
            let data = PostData.String @"{""password"":""x-pack-test-password"", ""roles"":[""superuser""]}"
            yield client.Security.PutUser<DynamicResponse>("x_pack_rest_user", data)
            
            let refreshAll =
                let rp = RefreshRequestParameters()
                rp.SetQueryString("expand_wildcards", "open,closed,hidden")
                client.Indices.Refresh<DynamicResponse>( "_all", rp)
                
            yield refreshAll
            
            yield client.Cluster.Health<DynamicResponse>(ClusterHealthRequestParameters(WaitForStatus=yellowStatus))
        }
)]

