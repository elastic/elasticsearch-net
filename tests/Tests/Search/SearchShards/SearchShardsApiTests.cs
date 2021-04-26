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
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.SearchShards
{
	public class SearchShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, SearchShardsResponse, ISearchShardsRequest, SearchShardsDescriptor<Project>,
			SearchShardsRequest<Project>>
	{
		public SearchShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;


		protected override int ExpectStatusCode => 200;

		protected override Func<SearchShardsDescriptor<Project>, ISearchShardsRequest> Fluent => s => s;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchShardsRequest<Project> Initializer => new SearchShardsRequest<Project>();
		protected override string UrlPath => $"project/_search_shards";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.SearchShards(f),
			(c, f) => c.SearchShardsAsync(f),
			(c, r) => c.SearchShards(r),
			(c, r) => c.SearchShardsAsync(r)
		);

		protected override SearchShardsDescriptor<Project> NewDescriptor() => new SearchShardsDescriptor<Project>();
	}
}
