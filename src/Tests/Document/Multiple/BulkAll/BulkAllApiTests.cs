using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllApiTests : SerializationTestBase, IClusterFixture<IntrusiveOperationCluster>
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
			var handle = new ManualResetEvent(false);
			var scrollObservable = this._client.ScrollAll<SmallObject>("1m", numberOfShards, s => s
				.MaxDegreeOfParallelism(numberOfShards / 2)
				.Search(search => search
					.Index(index)
					.AllTypes()
					.MatchAll()
				)
			);

			//we set up an observer
			var seenDocuments = 0;
			var seenSlices = new ConcurrentBag<int>();

			//since we call allTypes search should be bounded to index.
			var scrollObserver = new ScrollAllObserver<SmallObject>(
				onError: (e) => { handle.Set(); throw e; },
				onCompleted: () => handle.Set(),
				onNext: (b) =>
				{
					seenSlices.Add(b.Slice);
					Interlocked.Add(ref seenDocuments, b.SearchResponse.Hits.Count);
				}
			);
			//when we subscribe the observable becomes hot
			scrollObservable.Subscribe(scrollObserver);
			handle.WaitOne(TimeSpan.FromMinutes(5));
			seenDocuments.Should().Be(numberOfDocuments);
			var groups = seenSlices.GroupBy(s => s).ToList();
			groups.Count().Should().Be(numberOfShards);
			groups.Should().OnlyContain(g => g.Count() > 1);
		}

		private void BulkAll(string index, IEnumerable<SmallObject> documents, int size,  int pages, int numberOfDocuments)
		{
			var handle = new ManualResetEvent(false);
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
			var bulkObserver = new BulkAllObserver(
				onError: (e) => { handle.Set(); throw e; },
				onCompleted: () => handle.Set(),
				onNext: (b) => Interlocked.Increment(ref seenPages)
			);
			//when we subscribe the observable becomes hot
			observableBulk.Subscribe(bulkObserver);

			handle.WaitOne(TimeSpan.FromMinutes(5));

			seenPages.Should().Be(pages);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		}

		private async Task CreateIndexAsync(string indexName, int numberOfShards)
		{
			var handle = new ManualResetEvent(false);
			var result = await this._client.CreateIndexAsync(indexName, s => s
					.Settings(settings => settings
							.NumberOfShards(numberOfShards)
							.NumberOfReplicas(0)
					)
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
		}

		private static void OnError(ref Exception ex, Exception e, EventWaitHandle handle)
		{
			ex = e;
			handle.Set();
			throw e;
		}

		[I]
		public void DisposingObservableCancelsBulkAll()
		{
			var index = CreateIndexName();
			var handle = new ManualResetEvent(false);

			var size = 1000;
			var pages = 100;
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
			handle.WaitOne(TimeSpan.FromSeconds(1));
			observableBulk.Dispose();
			//we wait N seconds to give in flight request a chance to cancel
			handle.WaitOne(TimeSpan.FromSeconds(3));
			if (ex != null && !(ex is TaskCanceledException)) throw ex;

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		}

		[I]
		public void CancelBulkAll()
		{
			var index = CreateIndexName();
			var handle = new ManualResetEvent(false);

			var size = 1000;
			var pages = 100;
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
			handle.WaitOne(TimeSpan.FromSeconds(1));
			tokenSource.Cancel();
			//we wait Nseconds to give in flight request a chance to cancel
			handle.WaitOne(TimeSpan.FromSeconds(3));
			if (ex != null && !(ex is TaskCanceledException)) throw ex;

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
			bulkObserver.TotalNumberOfRetries.Should().Be(0);
		}

		[I]
		public async Task AwaitBulkAll()
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
	}
}
