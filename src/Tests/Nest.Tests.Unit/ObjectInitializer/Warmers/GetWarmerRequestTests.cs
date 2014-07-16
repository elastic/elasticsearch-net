using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit.ObjectInitializer.Warmers
{
	[TestFixture]
	public class GetWarmerRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetWarmerRequestTests()
		{
			var request = new GetWarmerRequest
			{
				Indices = new IndexNameMarker[] { "test" },
				Name = "warmer_1"
			};
			var response = this._client.GetWarmer(request);
			_status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			_status.RequestUrl.Should().EndWith("/test/_warmer/warmer_1");
			_status.RequestMethod.Should().Be("GET");
		}
	}
}
