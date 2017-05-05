using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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

		[I]
		public void ReturnsExpectedResponse()
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
			var bulkObserver = new BulkAllObserver(
				onError: (e) => { throw e; },
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
			bulkObserver.TotalNumberOfRetries.Should().Be(0);
		}

		[I, SkipOnTeamCity("this test is extremely flakey on TC, but never fails localy")]
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
			var bulkObserver = new BulkAllObserver(
				onError: (e) => { throw e; },
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

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = this._client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
			bulkObserver.TotalNumberOfRetries.Should().Be(0);
		}

		[I, SkipOnTeamCity("this test is extremely flakey on TC, but never fails localy")]
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
			var bulkObserver = new BulkAllObserver(
				onError: (e) => { throw e; },
				onCompleted: () => handle.Set(),
				onNext: (b) => Interlocked.Increment(ref seenPages)
			);
			//when we subscribe the observable becomes hot
			observableBulk.Subscribe(bulkObserver);

			//we wait Nseconds to see some bulks
			handle.WaitOne(TimeSpan.FromSeconds(1));
			tokenSource.Cancel();
			//we wait Nseconds to give in flight request a chance to cancel
			handle.WaitOne(TimeSpan.FromSeconds(3));

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
			var handle = new ManualResetEvent(false);

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
