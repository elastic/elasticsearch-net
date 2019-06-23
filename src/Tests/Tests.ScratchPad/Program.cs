using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Nest;
using Tests.ScratchPad.Runners.ApiCalls;

namespace Tests.ScratchPad
{
	public class Program
	{
		private class ListenerObserver : IObserver<DiagnosticListener>
		{
			public void OnCompleted() => Console.WriteLine("Completed");

			public void OnError(Exception error) => Console.Error.WriteLine(error.Message);

			public void OnNext(DiagnosticListener value)
			{
				if (value.Name == "Elasticsearch.Net.RequestPipeline")
				{
					value.Subscribe(new RequestPipelineListener());
				}
				if (value.Name == "Elasticsearch.Net.HttpConnection")
				{
					value.Subscribe(new RequestPipelineListener());
				}
			}
		}
		private class RequestPipelineListener : IObserver<KeyValuePair<string, object>>
		{
			public void OnCompleted() => Console.WriteLine("Completed");

			public void OnError(Exception error) => Console.Error.WriteLine(error.Message);

			public void OnNext(KeyValuePair<string, object> value) => Console.WriteLine($"- {value.Key}: {value.Value}");
		}

		private static async Task Main(string[] args)
		{
			DiagnosticListener.AllListeners.Subscribe(new ListenerObserver());
				
			using (var node = new Elastic.Managed.Ephemeral.EphemeralCluster("7.0.0"))
			{
				node.Start();

				var client = new ElasticClient();

				var x = client.Search<object>(s=>s.AllIndices());

				await Task.Delay(TimeSpan.FromSeconds(7));


			}
		}

		private static void Bench<TBenchmark>() where TBenchmark : RunBase => BenchmarkRunner.Run<TBenchmark>();

		private static void Run<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.Run();
		}

		private static void RunCreateOnce<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.RunCreateOnce();
		}
	}
}
