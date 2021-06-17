// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
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
			public TResponse Request<TResponse>(RequestData requestData) where TResponse : class, IElasticsearchResponse, new() =>
				ResponseBuilder.ToResponse<TResponse>(requestData, null, 200, null, Stream.Null, null);

			public Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
				where TResponse : class, IElasticsearchResponse, new() =>
				Task.FromResult(ResponseBuilder.ToResponse<TResponse>(requestData, null, 200, null, Stream.Null, null));

			public void Dispose() { }
		}
	}
}
