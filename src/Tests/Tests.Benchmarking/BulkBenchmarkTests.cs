using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Elasticsearch.Net;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig]
	public class BulkBenchmarkTests
	{
		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);
		private static byte[] Response = TestClient.DefaultInMemoryClient.ConnectionSettings.RequestResponseSerializer.SerializeToBytes(ReturnBulkResponse(Projects));

		private static readonly IElasticClient Client =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Response, 200, null, null))
				.DisableDirectStreaming()
				.EnableHttpCompression(false)
			);
		private static readonly Nest7.IElasticClient ClientV7 =
			new Nest7.ElasticClient(new Nest7.ConnectionSettings(
					new Elasticsearch.Net7.InMemoryConnection(Response, 200, null, null))
				.DisableDirectStreaming()
				.EnableHttpCompression(false)
			);

		[GlobalSetup]
		public void Setup() { }

		[Benchmark(Description = "NEST updated Bulk()")]
		public BulkResponse NestUpdatedBulk() => Client.Bulk(b => b.IndexMany(Projects));

		[Benchmark(Description = "NEST current Bulk()")]
		public Nest7.BulkResponse NestCurrentBulk() => ClientV7.Bulk(b => b.IndexMany(Projects));

		private static object BulkItemResponse(Project project) => new
		{
			index = new
			{
				_index = "nest-52cfd7aa",
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
	}
}
