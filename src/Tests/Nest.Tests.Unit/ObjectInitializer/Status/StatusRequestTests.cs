using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.IndicesStats
{
	[TestFixture]
	public class StatusRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public StatusRequestTests()
		{
			var request = new IndicesStatusRequest()
			{
				Recovery = true,
				Human = true
			};
			var response = this._client.Status(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_status?recovery=true&human=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
