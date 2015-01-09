using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.OpenClose
{
	[TestFixture]
	public class OpenIndexRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public OpenIndexRequestTests()
		{
			var request = new OpenIndexRequest(typeof(ElasticsearchProject))
			{
				ExpandWildcards = ExpandWildcards.Open
			};
			//TODO Index(request) does not work as expected
			var response = this._client.OpenIndex(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/_open?expand_wildcards=open");
			this._status.RequestMethod.Should().Be("POST");
		}
		
	}

}
