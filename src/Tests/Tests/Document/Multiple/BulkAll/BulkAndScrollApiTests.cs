using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAndScrollApiTests : BulkAllApiTestsBase
	{
		public BulkAndScrollApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		[I] public async Task BulkAllAndScrollAll()
		{
			var index = CreateIndexName();

			const int size = 1000, pages = 100, numberOfDocuments = size * pages, numberOfShards = 10;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			await this.CreateIndexAsync(index, numberOfShards);

			this.BulkAll(index, documents, size, pages, numberOfDocuments);
			this.ScrollAll(index, size, numberOfShards, numberOfDocuments);
		}

		private void ScrollAll(string index, int size, int numberOfShards, int numberOfDocuments)
		{
			var seenDocuments = 0;
			var seenSlices = new ConcurrentBag<int>();
			var scrollObserver = this.Client.ScrollAll<SmallObject>("1m", numberOfShards, s => s
				.MaxDegreeOfParallelism(numberOfShards / 2)
				.Search(search => search
					.Size(size / 2)
					.Index(index)
					.AllTypes()
					.MatchAll()
				)
			).Wait(TimeSpan.FromMinutes(5), r =>
			{
				seenSlices.Add(r.Slice);
				Interlocked.Add(ref seenDocuments, r.SearchResponse.Hits.Count);
			});

			seenDocuments.Should().Be(numberOfDocuments);
			var groups = seenSlices.GroupBy(s => s).ToList();
			groups.Count().Should().Be(numberOfShards);
			groups.Should().OnlyContain(g => g.Count() > 1);
		}

		private void BulkAll(string index, IEnumerable<SmallObject> documents, int size,  int pages, int numberOfDocuments)
		{
			var seenPages = 0;
			//first we setup our cold observable
			var observableBulk = this.Client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
			);
			//we set up an observer
			var bulkObserver = observableBulk.Wait(TimeSpan.FromMinutes(5), b => Interlocked.Increment(ref seenPages));

			seenPages.Should().Be(pages);
			var count = this.Client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		}
	}
}
