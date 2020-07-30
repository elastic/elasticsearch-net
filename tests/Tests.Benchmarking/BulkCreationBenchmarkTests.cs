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
	[BenchmarkConfig(10)]
	public class BulkCreationBenchmarkTests
	{
		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);
		private static readonly byte[] Response = TestClient.DefaultInMemoryClient.ConnectionSettings.RequestResponseSerializer.SerializeToBytes(ReturnBulkResponse(Projects));

		private static readonly IElasticClient Client =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Response, 200, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);

		[GlobalSetup]
		public void Setup() { }

		[Benchmark(Description = "Descriptors.V8")]
		public Nest8.BulkDescriptor CreateUsingDecriptorsV8() => new Nest8.BulkDescriptor().IndexMany(Projects);

		[Benchmark(Description = "Descriptors")]
		public BulkDescriptor CreateUsingDecriptors() => new BulkDescriptor().IndexMany(Projects);

		[Benchmark(Description = "BulkCollection.AddRange")]
		public BulkRequest SynchronizedCollection()
		{
			var ops = new BulkOperationsCollection<IBulkOperation>();
			ops.AddRange(Projects.Select(p=> new BulkCreateOperation<Project>(p)));
			return new BulkRequest
			{
				Operations = ops
			};
		}

		[Benchmark(Description = "BulkCollection.const")]
		public BulkRequest SynchronizedCollectionConstructor()
		{
			var ops = new BulkOperationsCollection<IBulkOperation>(
				Projects.Select(p=> new BulkCreateOperation<Project>(p))
			);
			return new BulkRequest
			{
				Operations = ops
			};
		}

		[Benchmark(Description = "new List")]
		public BulkRequest NewList() =>
			new BulkRequest
			{
				Operations = new List<IBulkOperation>(
					Projects.Select(p=> new BulkCreateOperation<Project>(p))
				)
			};

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
