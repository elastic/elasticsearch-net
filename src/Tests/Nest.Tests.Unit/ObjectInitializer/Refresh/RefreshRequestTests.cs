using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Refresh
{
	[TestFixture]
	public class RefreshRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public RefreshRequestTests()
		{
			var request = new RefreshRequest()
			{
				Force = true,
				IgnoreUnavailable = true
			};
			var response = this._client.Refresh(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_refresh?force=true&ignore_unavailable=true");
			this._status.RequestMethod.Should().Be("POST");
		}
	}

}
