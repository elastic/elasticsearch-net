using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllApiTests : SerializationTestBase, IClusterFixture<IntrusiveOperationCluster>, IClassFixture<EndpointUsage>
	{
		private class SmallObject
		{
			public int Id { get; set; }
		}

		private readonly IElasticClient _client;

		private static string CreateIndexName() => $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		private IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
		{
			for (var i = 0; i < count; i++)
				yield return new SmallObject() { Id = i };
		}

		public BulkAllApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
		{
			this._client = cluster.Client;
		}

		[I] public async Task BulkAllAndScrollAll()
		{
			var index = CreateIndexName();

			const int size = 1000, pages = 100, numberOfDocuments = size * pages, numberOfShards = 10;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			await this.CreateIndexAsync(index, numberOfShards);

			this.BulkAll(index, documents, size, pages, numberOfDocuments);
			this.ScrollAll(index, numberOfShards, numberOfDocuments);
		}

		private void ScrollAll(string index, int numberOfShards, int numberOfDocuments)
		{
			var seenDocuments = 0;
			var seenSlices = new ConcurrentBag<int>();
			var scrollObserver = this._client.ScrollAll<SmallObject>("1m", numberOfShards, s => s
				.MaxDegreeOfParallelism(numberOfShards / 2)
				.Search(search => search
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
			var observableBulk = this._client.BulkAll(documents, f => f
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
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		}

		private async Task CreateIndexAsync(string indexName, int numberOfShards)
		{
			var result = await this._client.CreateIndexAsync(indexName, s => s
					.Settings(settings => settings
							.NumberOfShards(numberOfShards)
							.NumberOfReplicas(0)
					)
			);
			result.Should().NotBeNull();
			result.ShouldBeValid();
		}

		private static void OnError(ref Exception ex, Exception e, EventWaitHandle handle)
		{
			ex = e;
			handle.Set();
			throw e;
		}

		[I, SkipOnTeamCity("this test is extremely flakey on TC, but never fails localy")]
		public void DisposingObservableCancelsBulkAll()
		{
			var index = CreateIndexName();
			var handle = new ManualResetEvent(false);

			var size = 1000;
			var pages = 1000;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			//first we setup our cold observable
			var observableBulk = this._client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
			);
			//we set up an observer
			Exception ex = null;
			var bulkObserver = new BulkAllObserver(
				onError: (e) => OnError(ref ex, e, handle),
				onCompleted: () => handle.Set(),
				onNext: (b) => Interlocked.Increment(ref seenPages)
			);

			//when we subscribe the observable becomes hot
			observableBulk.Subscribe(bulkObserver);

			//we wait N seconds to see some bulks
			handle.WaitOne(TimeSpan.FromSeconds(3));
			observableBulk.Dispose();
			//we wait N seconds to give in flight request a chance to cancel
			handle.WaitOne(TimeSpan.FromSeconds(3));
			if (ex != null && !(ex is TaskCanceledException) && !(ex is OperationCanceledException)) throw ex;

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		}

		[I, SkipOnTeamCity("this test is extremely flakey on TC, but never fails localy")]
		public void CancelBulkAll()
		{
			var index = CreateIndexName();
			var handle = new ManualResetEvent(false);

			var size = 1000;
			var pages = 1000;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			//first we setup our cold observable
			var tokenSource = new CancellationTokenSource();
			var observableBulk = this._client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
			, tokenSource.Token);

			//we set up an observer
			Exception ex = null;
			var bulkObserver = new BulkAllObserver(
				onError: (e) => OnError(ref ex, e, handle),
				onNext: (b) => Interlocked.Increment(ref seenPages)
			);
			//when we subscribe the observable becomes hot
			observableBulk.Subscribe(bulkObserver);

			//we wait Nseconds to see some bulks
			handle.WaitOne(TimeSpan.FromSeconds(3));
			tokenSource.Cancel();
			//we wait Nseconds to give in flight request a chance to cancel
			handle.WaitOne(TimeSpan.FromSeconds(3));
			if (ex != null && !(ex is TaskCanceledException) && !(ex is OperationCanceledException)) throw ex;

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
			bulkObserver.TotalNumberOfRetries.Should().Be(0);
		}

		[I] public async Task AwaitBulkAll()
		{
			var index = CreateIndexName();

			var size = 1000;
			var pages = 10;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			var tokenSource = new CancellationTokenSource();
			var observableBulk = this._client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(8)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
				.BufferToBulk((r, buffer) => r.IndexMany(buffer))
			, tokenSource.Token);


			await observableBulk
				.ForEachAsync(x => Interlocked.Increment(ref seenPages), tokenSource.Token);

			seenPages.Should().Be(pages);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments);
		}

		[I] public void WaitBulkAllThrowsAndIsCaught()
		{
			var index = CreateIndexName();

			var size = 1000;
			var pages = 10;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			var tokenSource = new CancellationTokenSource();
			var observableBulk = this._client.BulkAll(documents, f => f
				.MaxDegreeOfParallelism(4)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(size)
				.RefreshOnCompleted()
				.Index(index)
				.BufferToBulk((r, buffer) => r.IndexMany(buffer))
			, tokenSource.Token);

			try
			{
				observableBulk.Wait(TimeSpan.FromSeconds(30), b =>
				{
					if (seenPages == 8) throw new Exception("boom");
					Interlocked.Increment(ref seenPages);

				});
			}
			catch (Exception e)
			{
				seenPages.Should().Be(8);
				e.Message.Should().Be("boom");
			}
		}

		[I] public void ForEachAsyncReleasesProcessedItemsInMemory()
		{
			WeakReference<SmallObject> deallocReference = null;
			SmallObject obj = null;

			var lazyCollection = GetLazyCollection(
				weakRef => deallocReference = weakRef,
				delegate { },//...
				delegate { },//Making sure that all of the objects have gone through pipeline
				delegate { },//so that the first one can be deallocated
				delegate { },//Various GC roots prevent several of previous (2 or 3)
				delegate { },//items in the lazy Enumerable from deallocation during forced GC
				delegate { },//...
				delegate {
					GC.Collect(2, GCCollectionMode.Forced, true);
					deallocReference.TryGetTarget(out obj);
				}
			);

			var index = CreateIndexName();
			var observableBulk = this._client.BulkAll(lazyCollection, f => f
				.MaxDegreeOfParallelism(1)
				.Size(1)
				.Index(index)
				.BufferToBulk((r, buffer) => r.IndexMany(buffer)));

			observableBulk.Wait(TimeSpan.FromSeconds(30), delegate { });

			deallocReference.Should().NotBeNull();
			obj.Should().BeNull();
		}

		private IEnumerable<SmallObject> GetLazyCollection(params Action<WeakReference<SmallObject>>[] getFirstObjectCallBack)
		{
			var counter = 0;
			foreach (var callback in getFirstObjectCallBack)
			{
				var obj = new SmallObject { Id = ++counter };
				callback(new WeakReference<SmallObject>(obj));
				yield return obj;
			}
		}
	}
}
