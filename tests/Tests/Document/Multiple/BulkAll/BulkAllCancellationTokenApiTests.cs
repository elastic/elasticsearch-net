// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;

namespace Tests.Document.Multiple;

public class BulkAllCancellationTokenApiTests : BulkAllApiTestsBase
{
	public BulkAllCancellationTokenApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

	[I]
	[SkipOnCi]
	public void CancelBulkAll()
	{
		var index = CreateIndexName();
		var handle = new ManualResetEvent(false);

		var size = 1000;
		var pages = 1000;
		var seenPages = 0;
		var numberOfDocuments = size * pages;
		var documents = CreateLazyStreamOfDocuments(numberOfDocuments);

		//first we setup our cold observable
		var tokenSource = new CancellationTokenSource();
		var observableBulk = Client.BulkAll(documents, f => f
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

		//we wait N seconds to see some bulks
		handle.WaitOne(TimeSpan.FromSeconds(3));
		tokenSource.Cancel();
		//we wait N seconds to give in flight request a chance to cancel
		handle.WaitOne(TimeSpan.FromSeconds(3));

		if (ex != null && ex is not OperationCanceledException)
			throw ex;

		seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
		var count = Client.Count(new CountRequest(index));
		//var count = Client.Count<SmallObject>(f => f.Index(index));
		count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
		bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
		bulkObserver.TotalNumberOfRetries.Should().Be(0);
	}
}
