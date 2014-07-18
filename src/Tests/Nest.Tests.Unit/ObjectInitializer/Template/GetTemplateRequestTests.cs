using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Template
{
	[TestFixture]
	public class GetTemplateRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetTemplateRequestTests()
		{
			var request = new GetTemplateRequest("me-templ");
			var response = this._client.GetTemplate(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_template/me-templ");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
