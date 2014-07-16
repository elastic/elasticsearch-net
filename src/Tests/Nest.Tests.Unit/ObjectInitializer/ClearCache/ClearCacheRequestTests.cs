using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.ClearCache
{
	[TestFixture]
	public class ClearCacheRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClearCacheRequestTests()
		{
			var request = new ClearCacheRequest()
			{
				Indices = new IndexNameMarker[] { typeof(ElasticsearchProject), "my-other-index"}, 
			};
			var response = this._client.ClearCache(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data%2Cmy-other-index/_cache/clear");
			this._status.RequestMethod.Should().Be("POST");
		}
	}
}
