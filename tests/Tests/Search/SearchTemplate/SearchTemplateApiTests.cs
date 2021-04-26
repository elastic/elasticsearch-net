/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
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
