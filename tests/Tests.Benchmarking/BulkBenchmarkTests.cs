// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig(5)]
	public class BulkBenchmarkTests
	{
		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);
		private static readonly byte[] Response = TestClient.DefaultInMemoryClient.ConnectionSettings.RequestResponseSerializer.SerializeToBytes(ReturnBulkResponse(Projects));

		private static readonly IElasticClient Client =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Response, 200, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);

		private static readonly IElasticClient ClientNoRecyclableMemory =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Response, 200, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);

		private static readonly Nest8.IElasticClient ClientV8 =
			new Nest8.ElasticClient(new Nest8.ConnectionSettings(
					new Elasticsearch.Net8.InMemoryConnection(Response, 200, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);

		[GlobalSetup]
		public void Setup() { }

		[Benchmark(Description = "PR")]
		public BulkResponse NestUpdatedBulk() => Client.Bulk(b => b.IndexMany(Projects));

		[Benchmark(Description = "PR no recyclable")]
		public BulkResponse NoRecyclableMemory() => ClientNoRecyclableMemory.Bulk(b => b.IndexMany(Projects));

		[Benchmark(Description = "8.x")]
		public Nest8.BulkResponse NestCurrentBulk() => ClientV8.Bulk(b => b.IndexMany(Projects));

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
