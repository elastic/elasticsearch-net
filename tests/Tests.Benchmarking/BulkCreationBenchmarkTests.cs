using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig(10)]
	public class BulkCreationBenchmarkTests
	{
		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);

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

	}
}
