// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchTemplateRequest,
			SearchTemplateDescriptor<Project>, SearchTemplateRequest<Project>>
	{
		public SearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			@params = new { state = "Stable" },
			source = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchTemplateDescriptor<Project>, ISearchTemplateRequest> Fluent => s => s
			.Source("{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}")
			.Params(p => p
				.Add("state", "Stable")
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchTemplateRequest<Project> Initializer => new SearchTemplateRequest<Project>()
		{
			Source = "{\"query\": {\"match\":  {\"state\" : \"{{state}}\" }}}",
			Params = new Dictionary<string, object>
			{
				{ "state", "Stable" }
			}
		};

		protected override string UrlPath => $"/project/_search/template";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.SearchTemplate(f),
			(c, f) => c.SearchTemplateAsync(f),
			(c, r) => c.SearchTemplate<Project>(r),
			(c, r) => c.SearchTemplateAsync<Project>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Project> response) => response.Hits.Count().Should().BeGreaterThan(0);
	}

	public class SearchTemplateInvalidApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchTemplateRequest,
			SearchTemplateDescriptor<Project>, SearchTemplateRequest<Project>>
	{
		private readonly string _templateString = "{\"query\": {\"atch\":  {\"state\" : \"{{state}}\" }}}";

		public SearchTemplateInvalidApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => new
		{
			@params = new { state = "Stable" },
			source = _templateString
		};

		protected override int ExpectStatusCode => 400;

		protected override Func<SearchTemplateDescriptor<Project>, ISearchTemplateRequest> Fluent => s => s
			.Source(_templateString)
			.Params(p => p
				.Add("state", "Stable")
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchTemplateRequest<Project> Initializer => new SearchTemplateRequest<Project>()
		{
			Source = _templateString,
			Params = new Dictionary<string, object>
			{
				{ "state", "Stable" }
			}
		};

		protected override string UrlPath => $"/project/_search/template";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.SearchTemplate(f),
			(c, f) => c.SearchTemplateAsync(f),
			(c, r) => c.SearchTemplate<Project>(r),
			(c, r) => c.SearchTemplateAsync<Project>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Project> response) => response.ServerError.Should().NotBeNull();
	}
}
