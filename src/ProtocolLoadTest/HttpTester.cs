using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace ProtocolLoadTest
{
    internal class HttpTester : Tester, ITester
    {
        public void Run(string indexName, int port, int numMessages, int bufferSize)
        {
			var settings = this.CreateSettings(indexName, port);
            var client = new ElasticClient(settings);
            
            Connect(client, settings);

			GenerateAndIndex(client, indexName, numMessages, bufferSize);
        }
		public void SearchUsingSingleClient(string indexName, int port, int numMessages, int bufferSize)
		{
			var settings = this.CreateSettings(indexName, port);
            var client = new ElasticClient(settings);

			var tasks = new List<Task>();
			for (var p = 0;p<1000;p++)
			{
				var t = client.SearchAsync<Message>(s=>s.MatchAll())
					.ContinueWith(ta=>
					{
						if (!ta.Result.IsValid)
							throw new ApplicationException(ta.Result.ConnectionStatus.ToString());
					});
				tasks.Add(t);
			}
			Task.WaitAll(tasks.ToArray());
		}
		public void SearchUsingMultipleClients(string indexName, int port, int numMessages, int bufferSize)
		{
			var settings = this.CreateSettings(indexName, port);
			var tasks = new List<Task>();
			for (var p = 0; p < 1000; p++)
			{
				var client = new ElasticClient(settings);
				var t = client.SearchAsync<Message>(s => s.MatchAll())
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
