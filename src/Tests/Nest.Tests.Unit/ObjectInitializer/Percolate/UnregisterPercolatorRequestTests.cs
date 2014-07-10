using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Percolate
{
	[TestFixture]
	public class UnregisterPercolatorRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public UnregisterPercolatorRequestTests()
		{
			var request = new UnregisterPercolatorRequest("index", "percolator-name");
			var response = this._client.UnregisterPercolator(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/index/.percolator/percolator-name");
			this._status.RequestMethod.Should().Be("DELETE");
		}
		
	}
}
