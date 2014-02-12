using System;
using Nest;

namespace Profiling.InMemoryConnection
{
	class Program
	{
		private class Doc
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}

		static void Main(string[] args)
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"),"test-index");
			var client = new ElasticClient(settings, new Nest.InMemoryConnection(settings));
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
