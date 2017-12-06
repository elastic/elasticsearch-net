using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.ServerError
{
	public abstract class ServerErrorTestsBase
	{
		private IElasticClient HighLevelClient { get; }
		private IElasticLowLevelClient LowLevelClient { get; }

		protected ServerErrorTestsBase()
		{
			var settings = TestClient.GetFixedReturnSettings(ResponseJson, 500);
			this.LowLevelClient = new ElasticLowLevelClient(settings);
			this.HighLevelClient = new ElasticClient(settings);
		}
		protected virtual void AssertServerError()
		{
			LowLevelCall();
			HighLevelCall();
		}

		protected void HighLevelCall()
		{
			var response = this.HighLevelClient.Search<Project>(s => s);
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
			var response = this.LowLevelClient.Search<StringResponse>(PostData.Serializable(new { }));
			response.Should().NotBeNull();
			response.Body.Should().NotBeNullOrWhiteSpace();
			var hasServerError = response.TryGetServerError(out var serverError);
			hasServerError.Should().BeTrue("we're trying to deserialize a server error using the helper but it returned false");
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			AssertResponseError("low level client", serverError.Error);
		}

		private string ResponseJson => string.Concat(@"{ ""error"": ", Json, @",  ""status"":500 }");

		protected abstract string Json { get;  }

		protected abstract void AssertResponseError(string origin, Error error);
	}
}