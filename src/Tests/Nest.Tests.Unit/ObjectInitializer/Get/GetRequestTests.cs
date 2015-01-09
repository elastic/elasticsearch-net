using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Get
{
	[TestFixture]
	public class GetRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetRequestTests()
		{
			var request = new GetRequest<ElasticsearchProject>(2)
			{
				Preference = "local",
				Routing = "2",
			};
			var response = this._client.Get<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/elasticsearchprojects/2?preference=local&routing=2");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
