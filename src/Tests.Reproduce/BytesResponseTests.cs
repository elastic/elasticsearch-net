using System;
using System.Linq;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class BytesResponseTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public BytesResponseTests(ReadOnlyCluster cluster) => _cluster = cluster;

		[I] public void NonNullBytesResponse()
		{
			var client = _cluster.Client;

			var bytesResponse = client.LowLevel.Search<BytesResponse>("project", "project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}

		[I] public void NonNullBytesLowLevelResponse()
		{
			var settings  = new ConnectionConfiguration(new Uri($"http://localhost:{_cluster.Nodes.First().Port ?? 9200}"));
			var lowLevelClient = new ElasticLowLevelClient(settings);

			var bytesResponse = lowLevelClient.Search<BytesResponse>("project", "project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}
	}
}
