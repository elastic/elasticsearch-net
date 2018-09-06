using System;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
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
			var documents = this.CreateLazyStreamOfDocuments(numberOfDocuments);

			var tokenSource = new CancellationTokenSource();
			var observableBulk = this.Client.BulkAll(documents, f => f
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
}