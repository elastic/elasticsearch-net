using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;

namespace ScratchPad
{
	class Program
	{
		static void Main(string[] args)
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9209"),new Uri("http://localhost:9208"),new Uri("http://localhost:9201"),new Uri("http://localhost:9200") }, false);
			var settings = new ConnectionSettings(pool).AuditRequests(true).ProfileRequests(false).ConnectionLimit(2);
			var c = new ElasticClient(settings);

			var sw = Stopwatch.StartNew();
			Action<int> call = (n) =>
			{
				sw.Restart();
				var r = c.Search<object>(s => s.AllIndices());
				var swTotal = sw.Elapsed.ToString();
				sw.Elapsed.Dump($"[{n}] outside timing");
				r.DebugInformation.Dump($"[{n}] request");
			};

			for (var i = 0;i < 10; i++) call(i);
		}

	}
	public static class Dumper{
		public static void Dump(this object value, string section = null)
		{
			if (!string.IsNullOrWhiteSpace(section))
			{
				Console.WriteLine(section);
				Console.WriteLine("------------------");
			}
			var s = value as string;
			Console.WriteLine(s ?? JsonConvert.SerializeObject(value, Formatting.Indented));
		}
	}
}
