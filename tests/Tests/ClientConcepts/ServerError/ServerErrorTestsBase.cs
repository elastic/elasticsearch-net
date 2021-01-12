// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.ServerError
{
	public abstract class ServerErrorTestsBase
	{
		protected ServerErrorTestsBase()
		{
			var lowLevelClientSettings = FixedResponseClient.CreateConnectionSettings(ResponseJson, 500, serializer: new LowLevelRequestResponseSerializer());
			LowLevelClient = new ElasticLowLevelClient(lowLevelClientSettings);
			var settings = FixedResponseClient.CreateConnectionSettings(ResponseJson, 500);
			HighLevelClient = new ElasticClient(settings);
		}

		protected abstract string Json { get; }
		private IElasticClient HighLevelClient { get; }
		private IElasticLowLevelClient LowLevelClient { get; }

		private string ResponseJson => string.Concat(@"{ ""error"": ", Json, @", ""status"":500 }");

		protected virtual void AssertServerError()
		{
			LowLevelCall();
			HighLevelCall();
		}

		protected void HighLevelCall()
		{
			var response = HighLevelClient.Search<Project>(s => s);
			response.Should().NotBeNull();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			serverError.Error.Should().NotBeNull();
			serverError.Error.Headers.Should().NotBeNull();
			AssertResponseError("high level client", serverError.Error);
		}

		protected void LowLevelCall()
		{
			var response = LowLevelClient.Search<StringResponse>(PostData.Serializable(new { }));
			response.Should().NotBeNull();
			response.Body.Should().NotBeNullOrWhiteSpace();
			var hasServerError = response.TryGetElasticsearchServerError(out var serverError);
			hasServerError.Should().BeTrue("we're trying to deserialize a server error using the helper but it returned false");
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			AssertResponseError("low level client", serverError.Error);
		}

		protected abstract void AssertResponseError(string origin, Error error);
	}
}
