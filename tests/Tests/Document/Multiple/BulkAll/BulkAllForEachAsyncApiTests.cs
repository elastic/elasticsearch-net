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
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllForEachAsyncApiTests : BulkAllApiTestsBase
	{
		public BulkAllForEachAsyncApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

		[I] public async Task AwaitBulkAll()
		{
			var index = CreateIndexName();

			var size = 1000;
			var pages = 10;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = CreateLazyStreamOfDocuments(numberOfDocuments);

			var tokenSource = new CancellationTokenSource();
			var observableBulk = Client.BulkAll(documents, f => f
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
			var count = Client.Count<SmallObject>(f => f.Index(index));
			count.Count.Should().Be(numberOfDocuments);
		}
	}
}
