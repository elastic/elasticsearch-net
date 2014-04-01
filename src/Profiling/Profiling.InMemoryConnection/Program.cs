using System;
using Elasticsearch.Net.Connection;
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
			var client = new ElasticClient(settings);
			int calls = 10000;
			Console.WriteLine("done");
			Console.ReadLine();
			//using (var pbar = new ProgressBar(ticks, "Doing loads of in memory calls"))
			{
				for (int i = calls; i > 0; i--)
				{
					client.Index(new Doc() {Id = "asdasd" + i, Name = "Name"});
				}
			}
			Console.WriteLine("done");
			Console.ReadLine();
		}
	}
}
