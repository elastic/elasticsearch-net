using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Tests.Framework.Versions;
using Xunit;

namespace Tests.Search.MultiSearch.MultiSearchTemplate
{
	public class MultiSearchTemplateApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IMultiSearchResponse, IMultiSearchTemplateRequest, MultiSearchTemplateDescriptor, MultiSearchTemplateRequest>
	{
		public MultiSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiSearchTemplate(f),
			fluentAsync: (c, f) => c.MultiSearchTemplateAsync(f),
			request: (c, r) => c.MultiSearchTemplate(r),
			requestAsync: (c, r) => c.MultiSearchTemplateAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => false;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_msearch/template";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new object[]
		{
			new {},
			new { @params = new { state = "Stable" }, inline = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}" },
			new { index = "devs" },
			new { id = "template-id"},
			new { index = "devs"},
		};

		protected override Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> Fluent => ms => ms
			.Index(typeof(Project))
			.Type(typeof(Project))
			.Template<Project>("inline", s => s
				.Inline("{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}")
				.Params(p => p
					.Add("state", "Stable")
				)
			)
			.Template<Project>("id", s => s.Index("devs").Id("template-id"));

		protected override MultiSearchTemplateRequest Initializer => new MultiSearchTemplateRequest(typeof(Project), typeof(Project))
		{
			Operations = new Dictionary<string, ISearchTemplateRequest>
			{
				{ "inline", new SearchTemplateRequest<Project>(typeof(Project))
					{
						Inline = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}",
						Params = new Dictionary<string, object>
						{
							{ "state", "Stable" }
						}
					}
				},
				{ "id", new SearchTemplateRequest<Project>("devs") { Id = "template-id" } },
			}
		};

		protected override void ExpectResponse(IMultiSearchResponse response)
		{
			var inline = response.GetResponse<Project>("inline");
			inline.Should().NotBeNull();
			inline.ShouldBeValid();
			inline.Hits.Count().Should().BeGreaterThan(0);

			var id = response.GetResponse<Project>("id");
			id.Should().NotBeNull();
			id.ShouldNotBeValid();

		}
	}
}
