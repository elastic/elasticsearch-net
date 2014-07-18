using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.RootNodeInfo
{
	[TestFixture]
	public class RootNodeInfoRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public RootNodeInfoRequestTests()
		{
			var request = new InfoRequest();
			var response = this._client.RootNodeInfo(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
