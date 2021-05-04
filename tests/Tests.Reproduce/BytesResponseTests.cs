// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class BytesResponseTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public BytesResponseTests(ReadOnlyCluster cluster) => _cluster = cluster;

		[I] public void NonNullBytesResponse()
		{
			var client = _cluster.Client;

			var bytesResponse = client.LowLevel.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}

		[I] public void NonNullBytesLowLevelResponse()
		{
			var settings = new ConnectionConfiguration(new Uri($"http://localhost:{_cluster.Nodes.First().Port ?? 9200}"));
			var lowLevelClient = new ElasticLowLevelClient(settings);

			var bytesResponse = lowLevelClient.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}
	}
}
