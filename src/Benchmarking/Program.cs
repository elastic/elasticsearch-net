using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Benchmarking
{
	class Program
	{
		static IEnumerable<Tester> Testers(int port)
		{
			yield return new HttpTester(port);
		}

		static void Main(string[] args)
		{
			var arguments = new Arguments();

			if (!arguments.Parse(args))
				return;

			TestClient.Configuration = new BenchmarkingTestConfiguration();

			using (var cluster = new BenchmarkingCluster())
			{
				var warmup = new HttpTester(cluster.Node.Port).RunTests(10);
				var times = arguments.Times;

				if (arguments.Interactive)
				{
					Console.WriteLine("Warmed up caches to start testing, press any key to start tests");
					Console.ReadKey();
				}
				else
				{
					Console.WriteLine("Warmed up caches to start testing");
				}

				var key = default(ConsoleKeyInfo);
				do
				{
					var results = Testers(cluster.Node.Port).Select(t => t.RunTests()).ToList();
					Console.WriteLine();
					foreach (var result in results)
						result.Write(Console.Out);

					--times;

					if (arguments.Interactive)
					{
						Console.WriteLine("\nPress r to index again or any other key to delete indices created by this tool.\n");
						key = Console.ReadKey();
					}

				} while ((arguments.Interactive && key.KeyChar == 'r') || (!arguments.Interactive && times > 0));

				var client = new ElasticClient();
				client.DeleteIndex(Tester.IndexPrefix + "*");
			}
		}
	}
}
