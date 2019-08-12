using System;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain.Extensions;
using Tests.Framework.VirtualClustering;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllExceptionApiTests : BulkAllApiTestsBase
	{
		public BulkAllExceptionApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }

		[I] public void WaitBulkAllThrowsAndIsCaught()
		{
			var index = CreateIndexName();

			var size = 1000;
			var pages = 10;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = CreateLazyStreamOfDocuments(numberOfDocuments);

			var tokenSource = new CancellationTokenSource();
			var observableBulk = Client.BulkAll(documents, f => f
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
	}


	public class BulkAllBadRetriesApiTests : BulkAllApiTestsBase
	{
		public BulkAllBadRetriesApiTests(IntrusiveOperationCluster cluster) : base(cluster) { }
		
		[U] public void Completes()
		{
			var client = VirtualClusterWith.Nodes(2)
				.ClientCalls(c => c.FailAlways())
				.StaticConnectionPool()
				.AllDefaults()
				.Client;
			
			
			var index = CreateIndexName();

			var size = 1000;
			var pages = 10;
			var seenPages = 0;
			var numberOfDocuments = size * pages;
			var documents = CreateLazyStreamOfDocuments(numberOfDocuments);
			var requests = 0;

			Exception ex = null;
			var tokenSource = new CancellationTokenSource();
			var observableBulk = client.BulkAll(documents, f => f
					.MaxDegreeOfParallelism(1)
					.BulkResponseCallback(r => Interlocked.Increment(ref requests))
					.BackOffTime(TimeSpan.FromMilliseconds(1))
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
					Interlocked.Increment(ref seenPages);
				});
			}
			catch (Exception e)
			{
				ex = e;
			}
			ex.Should().NotBeNull();

			var clientException = ex.Should().BeOfType<ElasticsearchClientException>().Subject;
			
			clientException.Message.Should()
				.StartWith("BulkAll halted after")
				.And.EndWith("from _bulk and exhausting retries (2)");
			
			requests.Should().Be(3);
			// OnNext only called for successful batches.
			seenPages.Should().Be(0);
		}
	}
}
