using System;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllExceptionApiTests : BulkAllApiTestsBase
	{
		public BulkAllExceptionApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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


	[SkipOnCi] //TODO fails on canary windows only, need to come back to this one
	[SkipAttribute("Test fails after upgrading to .NET Core 3.0 on .NET 4.6.1 - only sees 1 request. Needs investigation")]
	public class BulkAllBadRetriesApiTests : BulkAllApiTestsBase
	{
		public BulkAllBadRetriesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		[U] public void Completes()
		{
			var client = Tests.Framework.Cluster.Nodes(2)
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
				.StartWith("BulkAll halted after");

			requests.Should().Be(3);
			// OnNext only called for successful batches.
			seenPages.Should().Be(0);
		}
	}
}
