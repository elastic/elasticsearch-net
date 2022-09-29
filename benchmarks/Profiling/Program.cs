// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using JetBrains.Profiler.Api;

//var req1 = new Elastic.Clients.Elasticsearch.IndexManagement.DeleteRequest("test");

////var list = new List<IndexName>();
////IEnumerable<IndexName> items = new IndexName[] { "a", "b" };

var stream = new MemoryStream(Encoding.UTF8.GetBytes(@"{""status"":""yellow"",""timed_out"":false,""number_of_nodes"":1,""number_of_data_nodes"":1,""active_primary_shards"":6,""active_shards"":6,""relocating_shards"":0,""initializing_shards"":0,""unassigned_shards"":4,""delayed_unassigned_shards"":0,""number_of_pending_tasks"":0,""number_of_in_flight_fetch"":0,""task_max_waiting_in_queue_millis"":0,""active_shards_percent_as_number"":60.0}"));

var data = Enumerable.Range(0, 1000).Select(r => new App.SampleData()).ToList();

var alphaClient = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri("https://localhost:9600"))
	.Authentication(new BasicAuthentication("elastic", "c236sjjbMP3nUGDxU_Z6"))
	.ServerCertificateValidationCallback((a, b, c, d) => true));

//var bulkAll = alphaClient.BulkAll(data, b => b
//				.Index("v8")
//				.BackOffRetries(2)
//				.ContinueAfterDroppedDocuments()
//				.Size(100));

//var observer = bulkAll.Wait(TimeSpan.FromMinutes(1), n => { });

_ = await alphaClient.RequestResponseSerializer.DeserializeAsync<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponse>(stream);

MemoryProfiler.ForceGc();

MemoryProfiler.CollectAllocations(true);

MemoryProfiler.GetSnapshot();

//bulkAll = alphaClient.BulkAll(data, b => b
//				.Index("v8")
//				.BackOffRetries(2)
//				.ContinueAfterDroppedDocuments()
//				.Size(100));

//observer = bulkAll.Wait(TimeSpan.FromMinutes(1), n => { });

var result = await alphaClient.RequestResponseSerializer.DeserializeAsync<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponse>(stream);

MemoryProfiler.GetSnapshot();

Console.WriteLine(result.ClusterName.ToString());

////var req = new DeleteRequest("test");

////var i = Indices.Parse("test");

//var i = Indices.Single("test");

//MemoryProfiler.GetSnapshot();

////if (req.AllowNoIndices.HasValue)
////{

////}

//_ = i.ToString();

//MemoryProfiler.CollectAllocations(false);

////var source = new IndexName[] { "index-01", "index-02" };

////var indices = new Indices(source);
////var indicesList = new IndicesList(source);

////MemoryProfiler.CollectAllocations(true);

////MemoryProfiler.GetSnapshot();

////var indices2 = new Indices(source);

////MemoryProfiler.GetSnapshot();

////var indicesList2 = new IndicesList(source);

////MemoryProfiler.GetSnapshot();

MemoryProfiler.CollectAllocations(false);

////// Ensure no GC between snapshots
////_ = indices2.Values.Count;
////_ = indicesList2.Values.Count;

namespace App
{
	public class SampleData
	{
		public SampleData() => Value = Guid.NewGuid();

		public Guid Value { get; }
	}
}
