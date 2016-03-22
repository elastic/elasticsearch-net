using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.SearchTemplate.RenderSearchTemplate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class RenderSearchTemplateApiTests : ApiIntegrationTestBase<IRenderSearchTemplateResponse, IRenderSearchTemplateRequest, RenderSearchTemplateDescriptor, RenderSearchTemplateRequest>
	{
		public RenderSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.RenderSearchTemplate(f),
			fluentAsync: (c, f) => c.RenderSearchTemplateAsync(f),
			request: (c, r) => c.RenderSearchTemplate(r),
			requestAsync: (c, r) => c.RenderSearchTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_render/template";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		private static string inlineSearchTemplate = @"
{
    ""query"": {
      ""terms"": {
        ""status"": [
          ""{{#status}}"",
          ""{{.}}"",
          ""{{/status}}""
        ]
      }
    }
  }";
		private string[] statusValues = new[] { "pending", "published" };

		protected override Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> Fluent => s=>s
			.Inline(inlineSearchTemplate)
			.Params(p=>p
				.Add("status", statusValues)
			);


		protected override RenderSearchTemplateRequest Initializer => new RenderSearchTemplateRequest
		{
			Inline = inlineSearchTemplate,
			Params = new Dictionary<string, object>
			{
				{ "status", statusValues }
			}
		};

		[I] public Task AssertResponse() => this.AssertOnAllResponses(r =>
		{
			r.TemplateOutput.Should().NotBeNull();
			var searchRequest = r.TemplateOutput.As<ISearchRequest>();
			searchRequest.Should().NotBeNull();

			searchRequest.Query.Should().NotBeNull();
		});
	}
}
