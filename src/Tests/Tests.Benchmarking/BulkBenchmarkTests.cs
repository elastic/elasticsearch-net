using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
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
		private static readonly IElasticClient Client = TestClient.FixedInMemoryClient(Response);

		[GlobalSetup]
		public void Setup() { }

		[Benchmark(Description = "NEST Bulk()")]
		public BulkResponse NestBulk() => Client.Bulk(b => b.IndexMany(Projects));



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
