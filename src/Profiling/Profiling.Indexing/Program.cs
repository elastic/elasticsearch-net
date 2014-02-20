using System;
using System.Diagnostics;
using System.Linq;
using Nest;

namespace Profiling.Indexing
{
	class Program
	{
		const string INDEX_PREFIX = "proto-load-test-";
		const int HTTP_PORT = 9200;
		const int THRIFT_PORT = 9500;

		// Total number of messages to send to elasticsearch
		const int NUM_MESSAGES = 250000;

		// Number of messages to buffer before sending via bulk API
		const int BUFFER_SIZE = 1000;

		static void Main(string[] args)
		{
			var process = Process.GetCurrentProcess();
			//warmer
			RunTest<HttpTester>(HTTP_PORT, 10);

			double httpRate = RunTest<HttpTester>(HTTP_PORT);
			var threadCountHttp = process.Threads.Count;
			var memorySizeHttp = process.VirtualMemorySize64;
			
			Console.WriteLine();
			Console.WriteLine("HTTP (IndexManyAsync): {0:0,0}/s {1} Threads {2} Virual memory"
				, httpRate, threadCountHttp, memorySizeHttp);

			Console.ReadLine();

			var client = new ElasticClient(new ConnectionSettings(new Uri("http://localhost:9200"), "nest-default-index"));
			client.DeleteIndex(d => d.Index(INDEX_PREFIX + "*"));

		}

		private static double RunTest<T>(int port, int? messages = null) where T : ITester
		{
			string type = typeof(T).Name.ToLower();
			Console.WriteLine("Starting {0} test", type);

			// Recreate index up-front, so this process doesn't interfere with perf figures
			Stopwatch sw = new Stopwatch();
			sw.Start();

			var tester = Activator.CreateInstance<T>();

			tester.Run(INDEX_PREFIX + type + "-" + Guid.NewGuid().ToString(), port, messages ?? NUM_MESSAGES, BUFFER_SIZE);

			sw.Stop();
			double rate = NUM_MESSAGES / ((double)sw.ElapsedMilliseconds / 1000);

			Console.WriteLine("{0} index test completed in {1}ms ({2:0,0}/s)", type, sw.ElapsedMilliseconds, rate);

			//var numberOfSearches = 10000;

			//sw.Restart();
			//tester.SearchUsingSingleClient(INDEX_PREFIX + type, port, numberOfSearches);
			//double singleClientSearchRate = numberOfSearches / ((double)sw.ElapsedMilliseconds / 1000);
			//Console.WriteLine("{0} search single client test completed in {1}ms ({2:0,0}/s)", type, sw.ElapsedMilliseconds, singleClientSearchRate);

			//sw.Restart();
			//tester.SearchUsingMultipleClients(INDEX_PREFIX + type, port, numberOfSearches);
			//double multiClientSearchRate = numberOfSearches / ((double)sw.ElapsedMilliseconds / 1000);
			//Console.WriteLine("{0} search multi client test completed in {1}ms ({2:0,0}/s)", type, sw.ElapsedMilliseconds, multiClientSearchRate);

			//// Close the index so we don't interfere with the next test
			//CloseIndex(type);

			return rate;
		}

		
	}
}
