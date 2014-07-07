using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
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
			this._status.RequestUrl.Should().EndWith("/postfixed-index-*%2Cnest_test_data?timeout=2m");
			this._status.RequestMethod.Should().Be("DELETE");
		}
		
	}
}
