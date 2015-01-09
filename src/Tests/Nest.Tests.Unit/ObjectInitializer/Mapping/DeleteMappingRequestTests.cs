using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Mapping
{
	[TestFixture]
	public class DeleteMappingRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteMappingRequestTests()
		{
			var request = new DeleteMappingRequest("my-index", "my-type");
			var response = this._client.DeleteMapping(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index/my-type/_mapping");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}

}
