using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Nest;

namespace ProtocolLoadTest
{
	class Program
	{
		const string INDEX_PREFIX = "proto-load-test-";
		const int HTTP_PORT = 9200;
		const int THRIFT_PORT = 9500;

		// Total number of messages to send to ElasticSearch
		const int NUM_MESSAGES = 250000;

		// Number of messages to buffer before sending via bulk API
		const int BUFFER_SIZE = 1000;

		static void Main(string[] args)
		{
			var process = Process.GetCurrentProcess();
			
			double notTaskRate = RunTest<TrueAsyncTester>(HTTP_PORT);
			var threadCountNoTasks = process.Threads.Count;
			var memorySizeNoTasks = process.VirtualMemorySize64;

			double httpRate = RunTest<HttpTester>(HTTP_PORT);
			var threadCountHttp = process.Threads.Count;
			var memorySizeHttp = process.VirtualMemorySize64;

			//double thriftRate = RunTest<ThriftTester>(THRIFT_PORT);

			Console.WriteLine();
			Console.WriteLine("HTTP (IndexManyAsync): {0:0,0}/s {1} Threads {2} Virual memory"
				, httpRate, threadCountHttp, memorySizeHttp);
			//Console.WriteLine("HTTP (IndexMany wrapped TaskFactory.StartNew): {0:0,0}/s", manualAsyncHttpRate);
			Console.WriteLine("HTTP (IndexManyAsyc using NoTasksHttpConnection): {0:0,0}/s {1} Threads {2} Virual memory"
				, notTaskRate, threadCountNoTasks, memorySizeNoTasks);
			//Console.WriteLine("Thrift: {0:0,0}/s", thriftRate);

			Console.ReadLine();
		}

		private static double RunTest<T>(int port) where T : ITester
		{
			string type = typeof(T).Name.ToLower();
			Console.WriteLine("Starting {0} test", type);

			// Recreate index up-front, so this process doesn't interfere with perf figures
			RecreateIndex(type);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			var tester = Activator.CreateInstance<T>();

			tester.Run(INDEX_PREFIX + type, port, NUM_MESSAGES, BUFFER_SIZE);

			sw.Stop();
			double rate = NUM_MESSAGES / ((double)sw.ElapsedMilliseconds / 1000);

			Console.WriteLine("{0} test completed in {1}ms ({2:0,0}/s)", type, sw.ElapsedMilliseconds, rate);

			// Close the index so we don't interfere with the next test
			CloseIndex(type);

			return rate;
		}

		private static void RecreateIndex(string suffix)
		{
			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			string indexName = INDEX_PREFIX + suffix;

			var connSettings = new ConnectionSettings(new Uri("http://"+host+":9200"))
				.SetDefaultIndex(indexName);

			var client = new ElasticClient(connSettings);

			var result = client.GetRootNodeInfo();
			if (!result.IsValid)
			{
				Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
					connSettings.Host, result.ConnectionStatus.Error.OriginalException.Message);
				Console.Read();
				return;
			}

			client.DeleteIndex(indexName);

			var indexSettings = new IndexSettings();
			indexSettings.NumberOfReplicas = 1;
			indexSettings.NumberOfShards = 5;
			indexSettings.Add("index.refresh_interval", "-1");

			var createResponse = client.CreateIndex(indexName, indexSettings);
			client.MapFromAttributes<Message>();
		}

		private static void CloseIndex(string suffix)
		{
			string indexName = INDEX_PREFIX + suffix;

			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";

			var connSettings = new ConnectionSettings(new Uri("http://" + host + ":9200"))
				.SetDefaultIndex(indexName);

			var client = new ElasticClient(connSettings);

			var result = client.GetRootNodeInfo();
			if (!result.IsValid)
			{
				Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
					connSettings.Host, result.ConnectionStatus.Error.OriginalException.Message);
				Console.Read();
				return;
			}

			client.CloseIndex(indexName);
		}
	}
}
