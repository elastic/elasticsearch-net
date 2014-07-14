using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Scroll
{
	[TestFixture]
	public class ClearScrollRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClearScrollRequestTests()
		{
			var request = new ClearScrollRequest("this-scroll-id");
			var response = this._client.ClearScroll(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_search/scroll");
			this._status.RequestMethod.Should().Be("DELETE");
		}
		
		[Test]
		public void ClearScrollBody()
		{
			this._status.Request.Utf8String().Should().Be("this-scroll-id");
		}
	}

}
