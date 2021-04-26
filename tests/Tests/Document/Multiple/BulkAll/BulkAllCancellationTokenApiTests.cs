/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Threading;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllCancellationTokenApiTests : BulkAllApiTestsBase
	{
		public BulkAllCancellationTokenApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

		[I] [SkipOnCi]
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

			if (ex != null && !(ex is OperationCanceledException)) throw ex;

			seenPages.Should().BeLessThan(pages).And.BeGreaterThan(0);
			var count = Client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().BeLessThan(numberOfDocuments).And.BeGreaterThan(0);
			bulkObserver.TotalNumberOfFailedBuffers.Should().Be(0);
			bulkObserver.TotalNumberOfRetries.Should().Be(0);
		}
	}
}
