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
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl
{
	public abstract class QueryDslIntegrationTestsBase
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslIntegrationTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new { query = QueryJson };
		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(QueryFluent);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = QueryInitializer
			};

		protected abstract QueryContainer QueryInitializer { get; }

		protected abstract object QueryJson { get; }
		protected override string UrlPath => "/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search(f),
			(client, f) => client.SearchAsync(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);

		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);
	}
}
