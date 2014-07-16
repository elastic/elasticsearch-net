using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Template
{
	[TestFixture]
	public class DeleteTemplateRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteTemplateRequestTests()
		{
			var request = new DeleteTemplateRequest("mah-template");
			var response = this._client.DeleteTemplate(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_template/mah-template");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}

}
