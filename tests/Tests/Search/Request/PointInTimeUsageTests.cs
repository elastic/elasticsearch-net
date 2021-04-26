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

namespace Tests.Search.Request
{
	/**
	 * A search request by default executes against the most recent visible data of the target indices, which is called point in time.
	 * Elasticsearch pit (point in time) is a lightweight view into the state of the data as it existed when initiated. In some cases,
	 * it's preferred to perform multiple search requests using the same point in time.
	 *
	 * IMPORTANT: Point in time search requests should not specify an index path parameter. When including a point in time in a
	 * search request, it will cause the URL path of the request to become the rooted '/_search' path instead of '/{index}/_search'.
	 *
	 * See the Elasticsearch documentation on {ref_current}/point-in-time-api.html[point in time API] for more detail.
	 */
	public class PointInTimeUsageTests
		: ApiTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public PointInTimeUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			pit = new
			{
				id = "a-point-in-time-id",
				keep_alive = "1m"
			}
		};
		
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.PointInTime("a-point-in-time-id", p => p
			.KeepAlive("1m"));

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				PointInTime = new Nest.PointInTime("a-point-in-time-id", "1m")
			};
		
		protected override string UrlPath => "/_search";

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search(f),
			(client, f) => client.SearchAsync(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);
	}
}
