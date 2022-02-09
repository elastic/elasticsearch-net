// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Nest;
using System.Globalization;
using System.Text;
using System.Text.Json;

var config = ManualConfig.Create(DefaultConfig.Instance);
config.SummaryStyle = new SummaryStyle(CultureInfo.CurrentCulture, true, BenchmarkDotNet.Columns.SizeUnit.B, null);

BenchmarkRunner.Run<Benchmarks.BulkIngest>(config);

namespace Benchmarks
{
	[MemoryDiagnoser]
	public class BulkIngest
	{
		//private static readonly List<SampleData> Data = Enumerable.Range(0, 100).Select(r => new SampleData()).ToList();

		private static readonly ElasticClient NestClient = new(new ConnectionSettings(new Uri("https://localhost:9600"))
			.BasicAuthentication("elastic", "c236sjjbMP3nUGDxU_Z6")
			.ServerCertificateValidationCallback((a, b, c, d) => true)
			.EnableApiVersioningHeader());

		private static readonly ElasticsearchClient AlphaClient = new(new ElasticsearchClientSettings(new Uri("https://localhost:9600"))
			.Authentication(new BasicAuthentication("elastic", "c236sjjbMP3nUGDxU_Z6"))
			.ServerCertificateValidationCallback((a, b, c, d) => true));

		private static readonly MemoryStream Stream = new(Encoding.UTF8.GetBytes(@"{""cluster_name"":""my-test-cluster"",""status"":""yellow"",""timed_out"":false,""number_of_nodes"":1,""number_of_data_nodes"":1,""active_primary_shards"":6,""active_shards"":6,""relocating_shards"":0,""initializing_shards"":0,""unassigned_shards"":4,""delayed_unassigned_shards"":0,""number_of_pending_tasks"":0,""number_of_in_flight_fetch"":0,""task_max_waiting_in_queue_millis"":0,""active_shards_percent_as_number"":60.0}"));

		[Benchmark]
		public void Version7()
		{
			Stream.Position = 0;
			_ = NestClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponse>(Stream);
		}

		[Benchmark]
		public void Version8()
		{
			Stream.Position = 0;
			_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponse>(Stream);
		}

		// [Benchmark]
		// public void Version8_String()
		// {
		// 	Stream.Position = 0;
		// 	_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV2>(Stream);
		// }

		//[Benchmark]
		//public void Version8_String_Converter()
		//{
		//	Stream.Position = 0;
		//	_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV3>(Stream);
		//}

		//[Benchmark]
		//public void Version8_String_ConverterWithBool()
		//{
		//	Stream.Position = 0;
		//	_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV3_BoolFlags>(Stream);
		//}

		//[Benchmark]
		//public void Version8_String_ConverterWithSpan()
		//{
		//	Stream.Position = 0;
		//	_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV3_Span>(Stream);
		//}

		//[Benchmark]
		//public void Version8_SourceWithoutUsingContext()
		//{
		//	Stream.Position = 0;
		//	_ = AlphaClient.RequestResponseSerializer.Deserialize<Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV4>(Stream);
		//}

		//[Benchmark]
		//public void Version8_SourceDirect()
		//{
		//	Stream.Position = 0;
		//	_ = JsonSerializer.Deserialize(Stream, Elastic.Clients.Elasticsearch.Cluster.ClusterHealthResponseV4Context.Default.ClusterHealthResponseV4);
		//}
	}
	
	public class SampleData
	{
		public SampleData() => Value = Guid.NewGuid();

		public Guid Value { get; }
	}
}
