// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Framework.SerializationTests
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
			var settings  = new ConnectionConfiguration(new Uri($"http://localhost:{_cluster.Nodes.First().Port ?? 9200}"));
			var lowLevelClient = new ElasticLowLevelClient(settings);

			var bytesResponse = lowLevelClient.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}

		[U]
		public void TryGetServerErrorDoesNotThrowException()
		{
			var responseBytes = Encoding.UTF8.GetBytes(StubResponse.NginxHtml401Response);

			var client = FixedResponseClient.Create(responseBytes, 401,
				modifySettings: s => s.DisableDirectStreaming(),
				contentType: "text/html",
				exception: new Exception("problem with the request as a result of 401")
			);

			var bytesResponse = client.LowLevel.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(responseBytes);
			bytesResponse.TryGetElasticsearchServerError(out var serverError).Should().BeFalse();
			serverError.Should().BeNull();
		}

		[U] public void SkipDeserializationForStatusCodesSetsBody()
		{
			var responseBytes = Encoding.UTF8.GetBytes(StubResponse.NginxHtml401Response);

			var client = FixedResponseClient.Create(responseBytes, 401,
				modifySettings: s => s.DisableDirectStreaming().SkipDeserializationForStatusCodes(401),
				contentType: "text/html",
				exception: new Exception("problem with the request as a result of 401")
			);

			var bytesResponse = client.LowLevel.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(responseBytes);
		}
	}
}
