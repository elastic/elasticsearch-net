using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Benchmarking;
using Elasticsearch.Net;
using Nest;

namespace Benchmarking
{
    public abstract class Tester
	{
		public static string IndexPrefix = "benchmark-test-";

		// Total number of messages to send to elasticsearch
		const int NumberOfMessages = 250000;

		// Number of messages to buffer before sending via bulk API
		const int BufferSize = 1000;

		// Number of messages sent by all Tester instances
		protected static int NumSent;

		private string Type { get; }

		public IElasticClient Client { get; }

		public string IndexName { get; }

		protected Tester(int port)
		{
			if (port == default(int))
				throw new ArgumentException("invalid port number. ensure the benchmarking cluster is running", nameof(port));

			this.Type = this.GetType().Name.ToLowerInvariant();
			this.IndexName = IndexPrefix + this.Type + "-" + Guid.NewGuid();
			this.Port = port;
			this.Client = this.CreateClient();
		}

		public int Port { get; }

		public abstract IElasticClient CreateClient();

		public Results RunTests(int numMessages = NumberOfMessages, int bufferSize = BufferSize)
		{
			var before = Metrics.Create();

			this.Connect();

			var indexResult = this.GenerateAndIndex(numMessages, bufferSize);
			var elapsed = indexResult.Elapsed;

			var rate = numMessages / (elapsed / 1000);

			var after = Metrics.Create();

			return new Results
			{
				TesterName = this.Type,
				Before = before,
				After = after,
				ElapsedMillisecond = elapsed,
				RatePerSecond = rate,
				IndexedDocuments = numMessages,
				EsTimings = indexResult.EsTimings
			};
		}

		protected ConnectionSettings CreateSettings()
		{
			var host = Process.GetProcessesByName("fiddler").Any() 
				? "ipv4.fiddler"
				: "localhost";

			var uri = new UriBuilder("http", host, Port).Uri;
			var connectionPool = new SniffingConnectionPool(new [] { uri });

			return new ConnectionSettings(connectionPool)
				.DefaultIndex(IndexName)
				.SniffOnStartup()
				.ThrowExceptions();
		}

		protected void Connect()
		{
			var result = this.Client.RootNodeInfo();
			if (result.IsValid) return;
			var status = result.ApiCall;
			throw new Exception($"Could not connect to {status.Uri}:\r\n{status}");
		}

		protected class IndexResults
		{
			public double Elapsed { get; set; }
			public IEnumerable<int> EsTimings { get; set; }
		}

		protected IndexResults GenerateAndIndex(int numMessages, int bufferSize)
		{
			//settings from  http://benchmarks.elasticsearch.org/
			var msgGenerator = new MessageGenerator();
			var partitionedMessages = msgGenerator.Generate(numMessages).Partition(bufferSize);
			this.Client.CreateIndex(this.IndexName, c => c
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

			var sw = Stopwatch.StartNew();
			Interlocked.Exchange(ref NumSent, 0);
			Task<IBulkResponse>[] array = partitionedMessages
				.Select(messages => Client.IndexManyAsync(messages, this.IndexName)
					.ContinueWith(tt =>
					{
						Interlocked.Add(ref NumSent, bufferSize);
						Console.Write("\r{2}: {0:0,0} msgs es-time: {1}      ",
							NumSent, tt.Result.Took, this.Type);
						return tt.Result;
					})
				).ToArray();

			Task.WaitAll(array);
			sw.Stop();

			Console.WriteLine();

			Client.UpdateIndexSettings(this.IndexName, u => u
				.IndexSettings(i => i
					.RefreshInterval("1s")
				)
			);

			return new IndexResults
			{
				Elapsed = sw.ElapsedMilliseconds,
				EsTimings = array.Select(a => a.Result.Took).ToList()
			};
		}

		public void SearchUsingSingleClient(int numberOfSearches)
		{
			var tasks = new List<Task>();
			for (var p = 0; p < numberOfSearches; p++)
			{
				var t = this.Client.SearchAsync<Message>(s => s.MatchAll())
					.ContinueWith(ta =>
					{
						if (!ta.Result.IsValid)
							throw new Exception(ta.Result.ApiCall.ToString());
					});

				tasks.Add(t);
			}

			Task.WaitAll(tasks.ToArray());
		}
	}
}