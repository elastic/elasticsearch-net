using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class GetAliasRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetAliasRequestTests()
		{
			var request = new GetAliasRequest()
			{
				Alias = "my-alias"  
			};
			var response = this._client.GetAlias(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_alias/my-alias");
			this._status.RequestMethod.Should().Be("GET");
		}
	}
}
