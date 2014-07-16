using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit.ObjectInitializer.Warmers
{
	public class DeleteWarmerRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteWarmerRequestTests()
		{
			var request = new DeleteWarmerRequest("my-warmer")
			{
				AllIndices = true
			};

			var response = this._client.DeleteWarmer(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_all/_warmer/my-warmer");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}
}
