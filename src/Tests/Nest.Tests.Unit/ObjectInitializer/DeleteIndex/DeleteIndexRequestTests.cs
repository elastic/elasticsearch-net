using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.DeleteIndex
{
	[TestFixture]
	public class DeleteIndexRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteIndexRequestTests()
		{
			var request = new DeleteIndexRequest(new IndexNameMarker[] { "postfixed-index-*", typeof(ElasticsearchProject)})
			{
				Timeout = "2m"
			};
			var response = this._client.DeleteIndex(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			var asterix = Type.GetType("Mono.Runtime") != null ? "%2A" : "*";
			var expectedUrl = "/postfixed-index-{0}%2Cnest_test_data?timeout=2m".F(asterix);
			this._status.RequestUrl.Should().EndWith(expectedUrl);
			this._status.RequestMethod.Should().Be("DELETE");
		}
		
	}
}
