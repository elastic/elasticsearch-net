using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

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
			var settings  = new ConnectionConfiguration(new Uri($"http://localhost:{_cluster.DesiredPort}"));
			var lowLevelClient = new ElasticLowLevelClient(settings);

			var bytesResponse = lowLevelClient.Search<BytesResponse>("project", "project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}
	}
}
