using System;
using System.Collections.Generic;
using System.Threading;
using Nest;
using System.Threading.Tasks;

namespace ProtocolLoadTest
{
	internal class HttpManualAsyncTester : Tester, ITester
    {
        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
			var settings = this.CreateSettings(indexName, port);
            var client = new ElasticClient(settings);
            
            Connect(client, settings);

			HttpManualAsyncTester.GenerateAndIndex(client, indexName, numMessages, bufferSize);
        }
		protected static void GenerateAndIndex(ElasticClient client, string indexName, int numMessages, int bufferSize)
		{
			// refresh = false is default on elasticsearch's side.
			var bulkParms = new SimpleBulkParameters() { Refresh = false };

			var msgGenerator = new MessageGenerator();
			var tasks = new List<Task>();
			var partitionedMessages = msgGenerator.Generate(numMessages).Partition(bufferSize);
			Interlocked.Exchange(ref NumSent, 0);
			foreach (var messages in partitionedMessages)
			{
				var t = Task.Factory.StartNew(()=> client.IndexMany(messages, indexName, bulkParms), TaskCreationOptions.LongRunning);
				tasks.Add(t);

				Interlocked.Add(ref NumSent, bufferSize);
				if (NumSent % 10000 == 0)
				{
					Console.WriteLine("Sent {0:0,0} messages to {1}", NumSent, indexName);
				}
			}
			Task.WaitAll(tasks.ToArray());
		}
    }
}
