// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information


using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;
using Elastic.Transport.Products.Elasticsearch;

namespace Tests.ClientConcepts.Errors
{
	public abstract class ServerErrorTestsBase
	{
		protected ServerErrorTestsBase()
		{
			var settings = FixedResponseClient.CreateConnectionSettings(ResponseJson, 500);
			HighLevelClient = new ElasticClient(settings);
		}

		protected abstract string Json { get; }
		private IElasticClient HighLevelClient { get; }

		private string ResponseJson => string.Concat(@"{ ""error"": ", Json, @", ""status"":500 }");

		protected virtual void AssertServerError()
		{
			HighLevelCall();
		}

		protected void HighLevelCall()
		{
			var response = HighLevelClient.Search<Project>(s => { });
			response.Should().NotBeNull();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			serverError.Error.Should().NotBeNull();
			serverError.Error.Headers.Should().NotBeNull();
			AssertResponseError("high level client", serverError.Error);
		}

		protected abstract void AssertResponseError(string origin, Error error);
	}
}
