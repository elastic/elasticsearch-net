using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
	[TestFixture]
	public class GetTemplateResponseTests : BaseJsonTests
	{
		[Test]
		public void HandlesResponseWithAlias()
		{

			var client = this.GetFixedReturnClient(MethodInfo.GetCurrentMethod(), "TemplateWithAliasResponse");
			var response = client.GetTemplate("employees");
			response.TemplateMapping.Aliases.Should().NotBeNull().And.NotBeEmpty();
			var alias = response.TemplateMapping.Aliases.First();
			alias.Key.Should().Be("employees");
			alias.Value.Routing.Should().Be("name");

		}

		[Test]
		public void HandlesResponseWithEmptyAlias()
		{

			var client = this.GetFixedReturnClient(MethodInfo.GetCurrentMethod(), "TemplateWithEmptyAliasResponse");
			var response = client.GetTemplate("employees");
			response.TemplateMapping.Aliases.Should().NotBeNull().And.NotBeEmpty();
			var alias = response.TemplateMapping.Aliases.First();
			alias.Key.Should().Be("employees");
		}
	}
}
