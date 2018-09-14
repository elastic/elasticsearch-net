using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkOnErrorApiTests : BulkAllApiTestsBase
	{
		private const int Size = 100, Pages = 3, NumberOfDocuments = Size * Pages, NumberOfShards = 1, FailAfterPage = 2;

		public BulkOnErrorApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		[I] public async Task WaitThrowsExceptionAndHalts()
		{
			var index = CreateIndexName();
			var documents = await this.CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var observableBulk = this.KickOff(index, documents);
			Action bulkObserver = () => observableBulk.Wait(TimeSpan.FromMinutes(5), b => Interlocked.Increment(ref seenPages));
			bulkObserver.ShouldThrow<ElasticsearchClientException>()
				.And.Message.Should().StartWith("BulkAll halted after receiving failures that can not");

			seenPages.Should().Be(2);
		}

		[I] public async Task SubscribeHitsOnError()
		{
			var index = CreateIndexName();
			var documents = await this.CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var observableBulk = this.KickOff(index, documents);
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
				var clientException = ex.Should().NotBeNull().And.BeOfType<ElasticsearchClientException>().Subject;
				clientException.Message.Should().StartWith("BulkAll halted after receiving failures that can not");
				seenPages.Should().Be(FailAfterPage);
			}
		}

		[I] public async Task ContinueAfterDroppedCallsCallback()
		{
			var index = CreateIndexName();
			var documents = await this.CreateIndexAndReturnDocuments(index);
			var seenPages = 0;
			var badDocumentIds = new List<int>(Size);
			var observableBulk = this.KickOff(index, documents, r => r
				.ContinueAfterDroppedDocuments(true)
				.DroppedDocumentCallback((i, d) =>
				{
					d.Should().NotBeNull();
					badDocumentIds.Add(d.Id);
					i.IsValid.Should().BeFalse();
				})

			);
			observableBulk.Wait(TimeSpan.FromMinutes(5), b => Interlocked.Increment(ref seenPages));

			seenPages.Should().Be(3);
			badDocumentIds.Should().NotBeEmpty()
				.And.HaveCount(Size)
				.And.OnlyContain(id => id >= Size * FailAfterPage);
		}

		private BulkAllObservable<SmallObject> KickOff(
			string index,
			IEnumerable<SmallObject> documents,
			Func<BulkAllDescriptor<SmallObject>, BulkAllDescriptor<SmallObject>> selector = null)
		{
			selector = selector ?? (s => s);
			var observableBulk = this.Client.BulkAll(documents, f => selector(f
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
			await this.CreateIndexAsync(index, NumberOfShards, m => m
				.Map<SmallObject>(mm => mm.Properties(p => p.Number(n => n.Name(pp => pp.Number).Coerce(false).IgnoreMalformed(false)))
				));

			var documents = this.CreateLazyStreamOfDocuments(NumberOfDocuments)
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
