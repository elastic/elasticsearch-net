using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchTemplateRequest,
		SearchTemplateDescriptor<Project>, SearchTemplateRequest<Project>>
	{
		public SearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.SearchTemplate(f),
			fluentAsync: (c, f) => c.SearchTemplateAsync(f),
			request: (c, r) => c.SearchTemplate<Project>(r),
			requestAsync: (c, r) => c.SearchTemplateAsync<Project>(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/_search/template";

		protected override object ExpectJson => new
		{
			@params = new { state = "Stable" },
			source = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}"
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Hits.Count().Should().BeGreaterThan(0);
		}

		protected override Func<SearchTemplateDescriptor<Project>, ISearchTemplateRequest> Fluent => s => s
			.AllTypes()
			.Source("{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}")
			.Params(p => p
				.Add("state", "Stable")
			);

		protected override SearchTemplateRequest<Project> Initializer => new SearchTemplateRequest<Project>()
		{
			Source = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}",
			Params = new Dictionary<string, object>
			{
				{"state", "Stable"}
			}
		};
	}
	public class SearchTemplateInvalidApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchTemplateRequest,
		SearchTemplateDescriptor<Project>, SearchTemplateRequest<Project>>
	{
		private string _templateString = "{\"query\": {\"atch\":  {\"state\" : \"{{state}}\" }}}";
		public SearchTemplateInvalidApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.SearchTemplate(f),
			fluentAsync: (c, f) => c.SearchTemplateAsync(f),
			request: (c, r) => c.SearchTemplate<Project>(r),
			requestAsync: (c, r) => c.SearchTemplateAsync<Project>(r)
		);

		protected override int ExpectStatusCode => 400;
		protected override bool ExpectIsValid => false;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/_search/template";

		protected override object ExpectJson => new
		{
			@params = new { state = "Stable" },
			source = _templateString
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Contain("no [query]");
		}

		protected override Func<SearchTemplateDescriptor<Project>, ISearchTemplateRequest> Fluent => s => s
			.AllTypes()
			.Source(_templateString)
			.Params(p => p
				.Add("state", "Stable")
			);

		protected override SearchTemplateRequest<Project> Initializer => new SearchTemplateRequest<Project>()
		{
			Source = _templateString,
			Params = new Dictionary<string, object>
			{
				{"state", "Stable"}
			}
		};
	}
}
