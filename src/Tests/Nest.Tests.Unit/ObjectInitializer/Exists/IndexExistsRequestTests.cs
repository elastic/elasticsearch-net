using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Exists
{
	[TestFixture]
	public class IndexExistsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public IndexExistsRequestTests()
		{
			var request = new IndexExistsRequest("my-index");
			var response = this._client.IndexExists(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index");
			this._status.RequestMethod.Should().Be("HEAD");
		}
	
	}
}
