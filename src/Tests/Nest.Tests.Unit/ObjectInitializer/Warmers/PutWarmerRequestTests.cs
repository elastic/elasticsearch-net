using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nest.Tests.Unit.ObjectInitializer.Warmers
{
	[TestFixture]
	public class PutWarmerRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public PutWarmerRequestTests()
		{
			QueryContainer query = new MatchAllQuery();

			var request = new PutWarmerRequest("warmer_1")
			{
				Indices = new IndexNameMarker[] { "my-index" },
				SearchDescriptor = new SearchRequest { Query = query }
			};

			var response = this._client.PutWarmer(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index/_warmer/warmer_1");
			this._status.RequestMethod.Should().Be("PUT");
		}

		[Test]
		public void PutWarmerBody()
		{
			this.JsonEquals(this._status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
