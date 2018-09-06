using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Document.Multiple.BulkAll
{
	public abstract class BulkAllApiTestsBase : IClusterFixture<IntrusiveOperationCluster>, IClassFixture<EndpointUsage>
	{
		protected BulkAllApiTestsBase(IntrusiveOperationCluster cluster, EndpointUsage usage) => this.Client = cluster.Client;

		protected class SmallObject
		{
			public int Id { get; set; }
		}

		protected IElasticClient Client { get; }

		protected static string CreateIndexName() => $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		protected IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
		{
			for (var i = 0; i < count; i++)
				yield return new SmallObject() { Id = i };
		}

		protected async Task CreateIndexAsync(string indexName, int numberOfShards)
		{
			var result = await this.Client.CreateIndexAsync(indexName, s => s
				.Settings(settings => settings
					.NumberOfShards(numberOfShards)
					.NumberOfReplicas(0)
				)
			);
			result.Should().NotBeNull();
			result.ShouldBeValid();
		}
		protected static void OnError(ref Exception ex, Exception e, EventWaitHandle handle)
		{
			ex = e;
			handle.Set();
			throw e;
		}
	}
}