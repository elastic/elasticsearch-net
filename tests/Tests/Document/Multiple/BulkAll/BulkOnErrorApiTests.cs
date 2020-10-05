// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkOnErrorApiTests : BulkAllApiTestsBase
	{
		private const int Size = 100,
			Pages = 3,
			NumberOfShards = 1,
			FailAfterPage = 2,
			// last page will have some errors we don't need a 100 exceptions printed on console out though :)
			NumberOfDocuments = Size * (Pages - 1) + 2;

		public BulkOnErrorApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

		[I] public async Task WaitThrowsExceptionAndHalts()
		{
			var index = CreateIndexName();
			var documents = await CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var observableBulk = KickOff(index, documents);
			Action bulkObserver = () => observableBulk.Wait(TimeSpan.FromMinutes(5), b => Interlocked.Increment(ref seenPages));
			bulkObserver.Should().Throw<TransportException>()
				.And.Message.Should()
				.StartWith("BulkAll halted after receiving failures that can not");

			seenPages.Should().Be(2);
		}

		[I] public async Task SubscribeHitsOnError()
		{
			var index = CreateIndexName();
			var documents = await CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var observableBulk = KickOff(index, documents);
			Exception ex = null;
			var handle = new ManualResetEvent(false);
			using (observableBulk.Subscribe(
				b =>
				{
					Interlocked.Increment(ref seenPages);
					b.Page.Should().BeLessOrEqualTo(FailAfterPage - 1);
				},
				e =>
				{
					ex = e;
					handle.Set();
				},
				() => handle.Set()
			))
			{
				handle.WaitOne(TimeSpan.FromSeconds(60));
				var clientException = ex.Should().NotBeNull().And.BeOfType<TransportException>().Subject;
				clientException.Message.Should().StartWith("BulkAll halted after receiving failures that can not");
				seenPages.Should().Be(FailAfterPage);
			}
		}

		[I] public async Task ContinueAfterDroppedCallsCallback()
		{
			var index = CreateIndexName();
			var documents = await CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var expectedBadDocuments = NumberOfDocuments - Size * FailAfterPage;
			var badDocumentIds = new List<int>(expectedBadDocuments);
			var observableBulk = KickOff(index, documents, r => r
				.ContinueAfterDroppedDocuments()
				.DroppedDocumentCallback((i, d) =>
				{
					d.Should().NotBeNull();
					badDocumentIds.Add(d.Id);
					i.IsValid.Should().BeFalse();
				})
			);
			observableBulk.Wait(TimeSpan.FromMinutes(5), b => Interlocked.Increment(ref seenPages));

			seenPages.Should().Be(3);
			badDocumentIds.Should()
				.NotBeEmpty()
				.And.HaveCount(expectedBadDocuments)
				.And.OnlyContain(id => id >= Size * FailAfterPage);
		}

		[I] public async Task BadBulkRequestFeedsToOnError()
		{
			var index = CreateIndexName();
			var documents = await CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var badUris = new[] { new Uri("http://test.example:9201"), new Uri("http://test.example:9202") };
			var pool = new StaticConnectionPool(badUris);
			var badClient = new ElasticClient(new ConnectionSettings(pool));
			var observableBulk = badClient.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(Size)
				.RefreshOnCompleted()
				.Index(index)
			);
			Exception ex = null;
			var handle = new ManualResetEvent(false);
			using (observableBulk.Subscribe(
				b => Interlocked.Increment(ref seenPages),
				e =>
				{
					ex = e;
					handle.Set();
				},
				() => handle.Set()
			))
			{
				handle.WaitOne(TimeSpan.FromSeconds(60));
				seenPages.Should().Be(0);
				var clientException = ex.Should().NotBeNull().And.BeOfType<TransportException>().Subject;
				clientException.Message.Should().StartWith("BulkAll halted after attempted bulk failed over all the active nodes");
			}
		}

		private BulkAllObservable<SmallObject> KickOff(
			string index,
			IEnumerable<SmallObject> documents,
			Func<BulkAllDescriptor<SmallObject>, BulkAllDescriptor<SmallObject>> selector = null
		)
		{
			selector = selector ?? (s => s);
			var observableBulk = Client.BulkAll(documents, f => selector(f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(Size)
				.RefreshOnCompleted()
				.Index(index)
			));
			return observableBulk;
		}

		private async Task<IEnumerable<SmallObject>> CreateIndexAndReturnDocuments(string index)
		{
			await CreateIndexAsync(index, NumberOfShards, m => m
				.Properties(p => p
					.Number(n => n.Name(pp => pp.Number).Coerce(false).IgnoreMalformed(false))
				)
			);

			var documents = CreateLazyStreamOfDocuments(NumberOfDocuments)
				.Select(s =>
				{
					if (s.Id < FailAfterPage * Size) return s;

					s.Number = "Not excepted";
					return s;
				});
			return documents;
		}
	}
}
