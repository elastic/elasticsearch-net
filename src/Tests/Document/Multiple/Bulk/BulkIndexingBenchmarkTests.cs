using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Framework;
using Tests.Framework.Benchmarks;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Tests.Framework.Profiling;
using Tests.Framework.Profiling.Memory;
using Tests.Framework.Profiling.Performance;

namespace Tests.Document.Multiple.Bulk
{
	[BenchmarkConfig]
	public class BulkIndexingBenchmarkTests : IDisposable
	{
		private List<IEnumerable<Message>> _messages;
		private readonly BenchmarkCluster _cluster;
		private readonly IElasticClient _client;

		public BulkIndexingBenchmarkTests()
		{
			_cluster = new BenchmarkCluster();
			_client = _cluster.Client;
			_client.CreateIndex("messages", c => c
				.Settings(s => s
					.NumberOfShards(6)
					.NumberOfReplicas(0)
					.RefreshInterval("30s")
					.Setting("index.store.type", "mmapfs")
					.Setting("index.store.throttle.type", "none")
					.Setting("indices.store.throttle.type", "none")
					.Setting("indices.memory.index_buffer_size", "10%")
					.Setting("index.translog.flush_threshold_size", "4g")
					.Setting("index.translog.flush_threshold_ops", 500000)
					.Setting("index.merge.scheduler.max_thread_count", 3)
					.Setting("index.merge.scheduler.max_merge_count", 6)
				)
				.Mappings(m => m.Map<Message>(mm => mm.AutoMap()))
			);
		}

#if NET45
		[Setup]
#else
		[GlobalSetup]
#endif
		[ProfilingSetup]
		public void Setup()
		{
			_messages = Message.Generator.Generate(250000).Partition(1000).ToList();
		}

		[Benchmark]
		[Performance]
		[Memory]
		public void IndexMessages()
		{
			foreach (var messages in _messages)
			{
				_client.IndexMany(messages);
			}
		}

		public void Dispose()
		{
			_cluster.Dispose();
		}
	}
}
