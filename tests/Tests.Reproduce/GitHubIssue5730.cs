// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue5730
	{
		[U]
		public void IndexExistsIsValidForBothLocalAndCloudResponses()
		{
			var client = TestClient.FixedInMemoryClient(Array.Empty<byte>());

			var response = client.Indices.Exists("an-index");

			response.IsValid.Should().BeTrue();
			response.Exists.Should().BeTrue();

			// Elastic Cloud responses for HEAD requests do not include the `content-type` header.
			// This test ensures that our validation handles this.

			var cloudClient = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")),
				new CloudIndexExistsInMemoryConnection()));

			response = cloudClient.Indices.Exists("an-index");

			response.IsValid.Should().BeTrue();
			response.Exists.Should().BeTrue();
		}

		/// <summary>
		/// Simulates not returning the content-type header so passes null mimeType to the <see cref="ResponseBuilder" />.
		/// </summary>
		private class CloudIndexExistsInMemoryConnection : IConnection
		{
			private readonly byte[] _productCheckResponse = Encoding.UTF8.GetBytes(@"{
  ""name"" : ""es02"",
  ""cluster_name"" : ""es-docker-cluster"",
  ""cluster_uuid"" : ""J60CQtNRStaWl-UUPnHHVw"",
  ""version"" : {
    ""number"" : ""7.13.3"",
    ""build_flavor"" : ""default"",
    ""build_type"" : ""docker"",
    ""build_hash"" : ""5d21bea28db1e89ecc1f66311ebdec9dc3aa7d64"",
    ""build_date"" : ""2021-07-02T12:06:10.804015202Z"",
    ""build_snapshot"" : false,
    ""lucene_version"" : ""8.8.2"",
    ""minimum_wire_compatibility_version"" : ""6.8.0"",
    ""minimum_index_compatibility_version"" : ""6.0.0-beta1""
  },
  ""tagline"" : ""You Know, for Search""
}");

			public TResponse Request<TResponse>(RequestData requestData) where TResponse : class, IElasticsearchResponse, new() =>
				GetResponse<TResponse>(requestData);

			public Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
				where TResponse : class, IElasticsearchResponse, new() => Task.FromResult(GetResponse<TResponse>(requestData));

			public void Dispose() { }

			private TResponse GetResponse<TResponse>(RequestData requestData) where TResponse : class, IElasticsearchResponse, new()
			{
				if (requestData.Uri.PathAndQuery != "/")
					return ResponseBuilder.ToResponse<TResponse>(requestData, null, 200, null, Stream.Null, null);

				// Handles product check
				var ms = new MemoryStream(_productCheckResponse);
				return ResponseBuilder.ToResponse<TResponse>(requestData, null, 200, null, ms, "Elasticsearch", "application/json; charset=utf-8");
			}
		}
	}
}
