// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAndScrollApiTests : BulkAllApiTestsBase
	{
		public BulkAndScrollApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

		[I] public async Task BulkAllAndScrollAll()
		{
			var index = CreateIndexName();

			const int size = 1000, pages = 100, numberOfDocuments = size * pages, numberOfShards = 10;
			var documents = CreateLazyStreamOfDocuments(numberOfDocuments);

			await CreateIndexAsync(index, numberOfShards);

			BulkAll(index, documents, size, pages, numberOfDocuments);
			ScrollAll(index, size, numberOfShards, numberOfDocuments);
		}

		private void ScrollAll(string index, int size, int numberOfShards, int numberOfDocuments)
		{
			var seenDocuments = 0;
			var seenSlices = new ConcurrentBag<int>();
			Client.ScrollAll<SmallObject>("1m", numberOfShards, s => s
					.MaxDegreeOfParallelism(numberOfShards / 2)
					.Search(search => search
						.Size(size / 2)
						.Index(index)
						.MatchAll()
					)
				)
				.Wait(TimeSpan.FromMinutes(5), r =>
				{
					seenSlices.Add(r.Slice);
					Interlocked.Add(ref seenDocuments, r.SearchResponse.Hits.Count);
				});

			seenDocuments.Should().Be(numberOfDocuments);
			var groups = seenSlices.GroupBy(s => s).ToList();
			groups.Count.Should().Be(numberOfShards);
			groups.Should().OnlyContain(g => g.Count() > 1);
		}

		private void BulkAll(string index, IEnumerable<SmallObject> documents, int size, int pages, int numberOfDocuments)
		{
			var seenPages = 0;

			var droppedDocuments = new ConcurrentBag<Tuple<BulkResponseItemBase, SmallObject>>();
			//first we setup our cold observable
			var observableBulk = Client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.DroppedDocumentCallback((b, i) => droppedDocuments.Add(Tuple.Create(b, i)))
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
			);
			//we set up an observer
			var bulkObserver = observableBulk.Wait(TimeSpan.FromMinutes(5), b =>
			{
				Interlocked.Increment(ref seenPages);
				foreach (var item in b.Items)
				{
					item.IsValid.Should().BeTrue();
					item.Id.Should().NotBeNullOrEmpty();
				}
			});

			droppedDocuments.Take(10).Should().BeEmpty();
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0, "All buffers are expected to be indexed");
			seenPages.Should().Be(pages, "BulkAll() did not run to completion");
			var count = Client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments, "Target index should have the same document count as source index");
		}
	}
}
