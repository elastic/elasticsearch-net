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

namespace Tests.Search.Scroll.Scroll
{
	public class ScrollApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, IScrollRequest, ScrollDescriptor<Project>, ScrollRequest>
	{
		private string _scrollId = "default-for-unit-tests";

		public ScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/scroll";

		protected override object ExpectJson => new
		{
			scroll = "1m",
			scroll_id = _scrollId
		};

		protected override ScrollRequest Initializer => new ScrollRequest(_scrollId, "1m");
		protected override ScrollDescriptor<Project> NewDescriptor() => new ScrollDescriptor<Project>("1m", _scrollId);

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Scroll("1m", _scrollId, f),
			(c, f) => c.ScrollAsync("1m", _scrollId, f),
			(c, r) => c.Scroll<Project>(r),
			(c, r) => c.ScrollAsync<Project>(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var response = client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes(1)));
			if (!response.IsValid)
				throw new Exception("Scroll setup failed");

			_scrollId = response.ScrollId ?? _scrollId;
		}

		protected override void OnAfterCall(IElasticClient client) => client.ClearScroll(cs => cs.ScrollId(_scrollId));
	}
}
