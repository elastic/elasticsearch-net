using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace Profiling.Indexing
{
	internal abstract class Tester
	{
		// Number of messages sent by all ThriftTester instances
		protected static int NumSent;

		protected ConnectionSettings CreateSettings(string indexName, int port)
		{
			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			var uri = new UriBuilder("http", host, port).Uri;
			return new ConnectionSettings(uri, "indexName");
		}

		protected void Connect(ElasticClient client, ConnectionSettings settings)
		{
			var result = client.RootNodeInfo();
			if (!result.IsValid)
			{
				Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
					result.ConnectionStatus.RequestUrl, result.ConnectionStatus.Error.OriginalException.Message);
				Console.Read();
			}
		}
		
		protected static void GenerateAndIndex(ElasticClient client, string indexName, int numMessages, int bufferSize)
		{
			var msgGenerator = new MessageGenerator();
			var tasks = new List<Task>();
			var partitionedMessages = msgGenerator.Generate(numMessages).Partition(bufferSize);
			client.CreateIndex(indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.Settings(s => s.Add("refresh_interval", "-1"))
				.AddMapping<Message>(p=>p.MapFromAttributes())
			);
			Interlocked.Exchange(ref NumSent, 0);
			foreach (var messages in partitionedMessages)
			{
				var t = client.IndexManyAsync(messages, indexName)
					.ContinueWith(tt =>
					{
						Interlocked.Add(ref NumSent, bufferSize);
						Console.WriteLine("Sent {0:0,0} messages to {1}, {2}", NumSent, indexName, tt.Result.Took);
					})
					;
				tasks.Add(t);
			}
			Task.WaitAll(tasks.ToArray());
			client.UpdateSettings(u => u
				.Index(indexName)
				.RefreshInterval("1s")
			);
		}
	}
}
