using System;
using Nest;
using Elasticsearch.Net;

namespace Profiling.InMemoryConnection
{
	class Program
	{
		private class Doc
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}
		/// <summary>
		/// Mainly used to profile the serialization performance and memory usage
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"),"test-index");
			var client = new ElasticClient(settings, new Elasticsearch.Net.Connection.InMemoryConnection(settings));
			int calls = 100000;
			var steps = calls/10;
			var ticks = calls / steps;
			//using (var pbar = new ProgressBar(ticks, "Doing loads of in memory calls"))
			{
				for (int i = calls; i > 0; i--)
				{
					client.Index(new Doc() {Id = "asdasd", Name = "Name"});
					//int m = (i%steps); 
					//if (m == 0)
					//	pbar.Tick();
				}
			}
			Console.WriteLine("done");
			Console.ReadLine();
		}
	}
}
