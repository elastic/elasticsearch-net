using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Scroll
{
	[TestFixture]
	public class ScrollRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ScrollRequestTests()
		{
			var request = new ScrollRequest("scroll-id", "5m");
			var response = this._client.Scroll<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_search/scroll?scroll=5m");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void ScrollBody()
		{
			this._status.Request.Utf8String().Should().Be("scroll-id");
		}
	}

	[TestFixture]
	public class ScrollRequestBase64Tests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;
		private readonly string _scrollId = "c2Nhbjs1Ozc3NjgyOjYwZFRpNmJwVGZPdUpRNkh2cmRBSmc7Nzc2ODU6NjBkVGk2YnBUZk91SlE2SHZyZEFKZzs3NzY4NDo2MGRUaTZicFRmT3VKUTZIdnJkQUpnOzc3NjgxOjYwZFRpNmJwVGZPdUpRNkh2cmRBSmc7Nzc2ODM6NjBkVGk2YnBUZk91SlE2SHZyZEFKZzsxO3RvdGFsX2hpdHM6Mjs=";

		public ScrollRequestBase64Tests()
		{
			var request = new ScrollRequest(_scrollId, "5m");
			var response = this._client.Scroll<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_search/scroll?scroll=5m");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void ScrollBody()
		{
			this._status.Request.Utf8String().Should().Be(_scrollId);
		}
	}

}
