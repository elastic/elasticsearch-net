using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.ConnectionPool;
using Nest;

namespace Profiling.Indexing
{
	public class RunResults
	{
		public string TesterName { get; set; }

		public double RatePerSecond { get; set; }
		
		public double ElapsedMillisecond { get; set; }

		public int IndexedDocuments { get; set; }

		public IEnumerable<int> EsTimings { get; set; }

		public Metrics Before { get; set; }
		public Metrics After { get; set; }
	}

	public class Metrics
	{
		public static Metrics Create()
		{
			var process = Process.GetCurrentProcess();
			var baseThreadCount = process.Threads.Count;
			var baseMemorySize = process.VirtualMemorySize64;
			return new Metrics
			{
				ThreadCount = baseThreadCount,
				MemorySize = baseMemorySize
			};
		}

		public long MemorySize { get; set; }

		public int ThreadCount { get; set; }
	}

	public abstract class Tester
	{
		public static string INDEX_PREFIX = "proto-load-test-";
		
		// Total number of messages to send to elasticsearch
		const int NUM_MESSAGES = 250000;

		// Number of messages to buffer before sending via bulk API
		const int BUFFER_SIZE = 1000;
		
		// Number of messages sent by all ThriftTester instances
		protected static int NumSent;
		private string Type { get; set; }

		public IElasticClient Client { get; private set; }

		public string IndexName { get; private set; }

		public Tester()
		{
			this.Type = this.GetType().Name.ToLowerInvariant();
			this.IndexName = INDEX_PREFIX + this.Type + "-" + Guid.NewGuid().ToString();
			this.Client = this.CreateClient(this.IndexName);
		}

		public abstract IElasticClient CreateClient(string defaultIndex);

		public RunResults RunTests(int numMessages = NUM_MESSAGES, int bufferSize = BUFFER_SIZE)
		{
			var before = Metrics.Create();

			this.Connect();

			var indexResult = this.GenerateAndIndex(numMessages, bufferSize);
			var elapsed = indexResult.Elapsed;

			var rate = numMessages / ((double)elapsed / 1000);
			
			var after = Metrics.Create();

			return new RunResults
			{
				TesterName = this.Type, Before = before, After = after,
				ElapsedMillisecond =  elapsed,
				RatePerSecond = rate,
				IndexedDocuments = numMessages,
				EsTimings = indexResult.EsTimings
			};
		}

		protected ConnectionSettings CreateSettings(string indexName, int port)
		{
			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			
			var uri = new UriBuilder("http", host, port).Uri;
			var connectionPool = new SniffingConnectionPool(new[] {uri});
			return new ConnectionSettings(connectionPool, indexName)
				.SniffOnStartup()
				.ThrowOnElasticsearchServerExceptions();
		}

		protected void Connect()
		{
			var result = this.Client.RootNodeInfo();
			if (result.IsValid) return;
			var status = result.ConnectionStatus;
			var message = string.Format("Could not connect to {0}:\r\n{1}", status.RequestUrl, status);
			throw new ApplicationException(message);
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
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.Settings(s => s
					.Add("refresh_interval", "30s")
					.Add("index.store.type", "mmapfs")
					.Add("index.store.throttle.type", "none")
					.Add("indices.store.throttle.type", "none") 
					.Add("index.number_of_shards", 6)
					.Add("index.number_of_replicas", 0)
					.Add("indices.memory.index_buffer_size", "10%")
					.Add("index.translog.flush_threshold_size", "4g")
					.Add("index.translog.flush_threshold_ops", 500000)
					.Add("index.merge.scheduler.max_thread_count", 3)
					.Add("index.merge.scheduler.max_merge_count", 6)
				)
				.AddMapping<Message>(p=>p.MapFromAttributes())
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
				)
				.ToArray();
			Task.WaitAll(array);
			sw.Stop();

			Console.WriteLine();
			Client.UpdateSettings(u => u
				.Index(this.IndexName)
				.RefreshInterval("1s")
			);

			return new IndexResults
			{
				Elapsed = sw.ElapsedMilliseconds,
				EsTimings = array.Select(a=>a.Result.Took).ToList()
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
							throw new ApplicationException(ta.Result.ConnectionStatus.ToString());
					});
				tasks.Add(t);
			}
			Task.WaitAll(tasks.ToArray());
		}

		public void SearchUsingMultipleClients(string indexName, int port, int numberOfSearches)
		{
			var tasks = new List<Task>();
			for (var p = 0; p < numberOfSearches; p++)
			{
				var t = Client.SearchAsync<Message>(s => s.MatchAll())
					.ContinueWith(ta =>
					{
						if (!ta.Result.IsValid)
							throw new ApplicationException(ta.Result.ConnectionStatus.ToString());
					});
				tasks.Add(t);
			}

			Task.WaitAll(tasks.ToArray());
		}
	}
}
