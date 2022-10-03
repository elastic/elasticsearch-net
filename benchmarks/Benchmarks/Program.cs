// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Nest;
using System.Globalization;
using System.Text;

var config = ManualConfig.Create(DefaultConfig.Instance);
config.SummaryStyle = new SummaryStyle(CultureInfo.CurrentCulture, true, BenchmarkDotNet.Columns.SizeUnit.B, null);
config.AddDiagnoser(MemoryDiagnoser.Default);

BenchmarkRunner.Run<Benchmarks.BulkIngest>(config);

//var thing = new Benchmarks.DescriptorValues();
//thing.Setup();

//var a = new EnrichPutPolicyRequestDescriptorV2<Benchmarks.SampleData>("test");

//a.Match(new Policy { Name = "test-name" });

//var settings = new ElasticsearchClientSettings();
//var serializer = new DefaultRequestResponseSerializer(settings);
//var stream = new MemoryStream();

//serializer.Serialize(a, stream);

//stream.Position = 0;
//var sr = new StreamReader(stream);
//var result = sr.ReadToEnd();

//Console.ReadKey();

namespace Benchmarks
{
	//public class DescriptorValues
	//{
	//	private EnrichPutPolicyRequestDescriptor<SampleData>? _data1;
	//	private EnrichPutPolicyRequestDescriptorV2<SampleData>? _data2;
	//	private EnrichPutPolicyRequestDescriptorV3<SampleData>? _data3;
	//	private EnrichPutPolicyRequestDescriptorV4<SampleData>? _data4;

	//	//private Existing<SampleData>? _basicData1;
	//	//private NewV1<SampleData>? _basicData2;
	//	//private NewV2<SampleData>? _basicData3;

	//	private readonly EnrichPutPolicyRequestDescriptor<SampleData> _existing = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV2<SampleData> _newV1 = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV3<SampleData> _newV2 = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV4<SampleData> _newV3 = new("test");
	//	private readonly Policy? _policy = new() { Name = "TEST" };

	//	private readonly EnrichPutPolicyRequestDescriptor<SampleData> _serializableExisting = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV2<SampleData> _serializableNewV1 = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV3<SampleData> _serializableNewV2 = new("test");
	//	private readonly EnrichPutPolicyRequestDescriptorV4<SampleData> _serializableNewV3 = new("test");

	//	private static readonly ElasticsearchClientSettings Settings = new();

	//	private readonly DefaultRequestResponseSerializer _serializer = new(Settings);
	//	private readonly MemoryStream _stream = new();

	//	[GlobalSetup]
	//	public void Setup()
	//	{
	//		_serializableExisting.GeoMatch(_policy);
	//		_serializableNewV1.GeoMatch(_policy);
	//		_serializableNewV2.GeoMatch(_policy);
	//		_serializableNewV3.GeoMatch(_policy);
	//	}

	//	[Benchmark]
	//	public void Existing() => _data1 = new EnrichPutPolicyRequestDescriptor<SampleData>("name");

	//	[Benchmark]
	//	public void NewV1() => _data2 = new EnrichPutPolicyRequestDescriptorV2<SampleData>("name");

	//	[Benchmark]
	//	public void NewV2() => _data3 = new EnrichPutPolicyRequestDescriptorV3<SampleData>("name");

	//	[Benchmark]
	//	public void NewV3() => _data4 = new EnrichPutPolicyRequestDescriptorV4<SampleData>("name");

	//	//[Benchmark]
	//	//public void Existing() => _basicData1 = new Existing<SampleData>();

	//	//[Benchmark]
	//	//public void NewV1() => _basicData2 = new NewV1<SampleData>();

	//	//[Benchmark]
	//	//public void NewV2() => _basicData3 = new NewV2<SampleData>();

	//	[Benchmark]
	//	public void SetExisting() => _existing.GeoMatch(_policy);

	//	[Benchmark]
	//	public void SetNewV1() => _newV1.GeoMatch(_policy);

	//	[Benchmark]
	//	public void SetNewV2() => _newV2.GeoMatch(_policy);

	//	[Benchmark]
	//	public void SetNewV3() => _newV3.GeoMatch(_policy);

	//	[Benchmark]
	//	public void SerialiseExisting()
	//	{
	//		_stream.Position = 0;
	//		_serializer.Serialize(_serializableExisting, _stream);
	//	}

	//	[Benchmark]
	//	public void SerialiseNewV1()
	//	{
	//		_stream.Position = 0;
	//		_serializer.Serialize(_serializableNewV1, _stream);
	//	}

	//	[Benchmark]
	//	public void SerialiseNewV2()
	//	{
	//		_stream.Position = 0;
	//		_serializer.Serialize(_serializableNewV2, _stream);
	//	}

	//	[Benchmark]
	//	public void SerialiseNewV3()
	//	{
	//		_stream.Position = 0;
	//		_serializer.Serialize(_serializableNewV3, _stream);
	//	}
	//}

	// RESULT OF ABOVE:
	//|            Method |  Mean [ns] | Error [ns] | StdDev [ns] |  Gen 0 |  Gen 1 | Allocated [B] |
	//|------------------ |-----------:|-----------:|------------:|-------:|-------:|--------------:|
	//|          Existing | 116.408 ns |  2.1955 ns |   2.1563 ns | 0.0942 | 0.0002 |         592 B |
	//|             NewV1 | 113.021 ns |  1.8475 ns |   1.8145 ns | 0.0943 | 0.0004 |         592 B |
	//|             NewV2 | 115.728 ns |  2.2291 ns |   2.0851 ns | 0.0905 | 0.0002 |         568 B |
	//|             NewV3 | 112.813 ns |  2.2198 ns |   3.1118 ns | 0.0867 | 0.0002 |         544 B |
	//|       SetExisting |   1.688 ns |  0.0384 ns |   0.0340 ns |      - |      - |             - |
	//|          SetNewV1 |   2.200 ns |  0.0232 ns |   0.0217 ns |      - |      - |             - |
	//|          SetNewV2 |   2.555 ns |  0.0289 ns |   0.0256 ns |      - |      - |             - |
	//|          SetNewV3 |   1.691 ns |  0.0306 ns |   0.0286 ns |      - |      - |             - |
	//| SerialiseExisting | 461.120 ns |  9.0066 ns |   8.4248 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV1 | 472.239 ns |  8.4655 ns |   7.9186 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV2 | 476.059 ns |  9.2350 ns |  10.2647 ns | 0.0687 |      - |         432 B |
	//|    SerialiseNewV3 | 483.717 ns |  9.5424 ns |   8.9260 ns | 0.0687 |      - |         432 B |

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
