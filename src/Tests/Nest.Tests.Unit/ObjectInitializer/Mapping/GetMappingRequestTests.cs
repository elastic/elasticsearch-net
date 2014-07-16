using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Mapping
{
	[TestFixture]
	public class GetMappingRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetMappingRequestTests()
		{
			var request = new GetMappingRequest("my-index", "my-type");
			var response = this._client.GetMapping(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index/_mapping/my-type");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
