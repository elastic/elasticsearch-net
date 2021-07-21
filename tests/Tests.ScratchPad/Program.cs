// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Elasticsearch.Net;
using Elasticsearch.Net.Diagnostics;
using Nest;
using Tests.Core.Client;
using Tests.Domain;

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

				var client = new ElasticClient();

				client.Search<Project>();

				client.LowLevel.Search<SearchResponse<Project>>(PostData.Serializable(new SearchRequest()));


				void WriteToConsole<T>(string eventName, T data)
				{
					var a = Activity.Current;
					Console.WriteLine($"{eventName?.PadRight(30)} {a.Id?.PadRight(32)} {a.ParentId?.PadRight(32)} {data?.ToString().PadRight(10)}");
				}
				if (value.Name == DiagnosticSources.AuditTrailEvents.SourceName)
					value.Subscribe(new AuditDiagnosticObserver(v => WriteToConsole(v.Key, v.Value)));

				if (value.Name == DiagnosticSources.RequestPipeline.SourceName)
					value.Subscribe(new RequestPipelineDiagnosticObserver(
						v => WriteToConsole(v.Key, v.Value),
						v => WriteToConsole(v.Key, v.Value))
					);

				if (value.Name == DiagnosticSources.HttpConnection.SourceName)
					value.Subscribe(new HttpConnectionDiagnosticObserver(
						v => WriteToConsole(v.Key, v.Value),
						v => WriteToConsole(v.Key, v.Value)
					));

				if (value.Name == DiagnosticSources.Serializer.SourceName)
					value.Subscribe(new SerializerDiagnosticObserver(v => WriteToConsole(v.Key, v.Value)));
			}
		}

		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);
		private static readonly byte[] Response = TestClient.DefaultInMemoryClient.ConnectionSettings.RequestResponseSerializer.SerializeToBytes(ReturnBulkResponse(Projects));

		private static readonly IElasticClient Client =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Response, 200, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);


		private static void Main(string[] args)
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9200"), new Uri("http://localhost:9201") });
			//var pool = new CloudConnectionPool("7140:dXMtY2VudHJhbDEuZ2NwLmZvdW5kaXQubm8kNzQyZTBjYmEzYmU4NDYxMzhkMWY2YzkwMmRlNzliZDEkODc4YjM3MjA1YTFkNDM0ODllNGZiYWI1OTZmODc0MjQ=", new BasicAuthenticationCredentials("elastic", "BT7ldQjnFkCNOxCi7dcGoR7x"));
			var client = new ElasticClient(new ConnectionSettings(pool).RequestTimeout(TimeSpan.FromSeconds(60)).DisablePing());

			try
			{
				ClusterHealthResponse response;
				for (var i = 0; i < 20; i++)
				{
					response = client.Cluster.Health();
					Console.WriteLine($"{response.IsValid}");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}


		private static object BulkItemResponse(Project project) => new
		{
			index = new
			{
				_index = "nest-52cfd7aa",
				_type = "_doc",
				_id = project.Name,
				_version = 1,
				_shards = new
				{
					total = 2,
					successful = 1,
					failed = 0
				},
				created = true,
				status = 201
			}
		};

		private static object ReturnBulkResponse(IList<Project> projects) => new
		{
			took = 276,
			errors = false,
			items = projects
				.Select(p => BulkItemResponse(p))
				.ToArray()
		};

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
