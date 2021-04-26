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

namespace Tests.Search.Scroll.ClearScroll
{
	// ReadOnlyCluster because eventhough its technically a write action it does not hinder
	// on going reads
	public class ClearScrollApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClearScrollResponse, IClearScrollRequest, ClearScrollDescriptor, ClearScrollRequest>
	{
		private string _scrollId = "default-for-unit-tests";

		public ClearScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			scroll_id = new[]
			{
				_scrollId
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ClearScrollDescriptor, IClearScrollRequest> Fluent => cs => cs.ScrollId(_scrollId);
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override ClearScrollRequest Initializer => new ClearScrollRequest(_scrollId);
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/scroll";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.ClearScroll(f),
			(c, f) => c.ClearScrollAsync(f),
			(c, r) => c.ClearScroll(r),
			(c, r) => c.ClearScrollAsync(r)
		);

		protected override ClearScrollDescriptor NewDescriptor() => new ClearScrollDescriptor();

		protected override void OnBeforeCall(IElasticClient client)
		{
			var scroll = Client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes(1)));
			if (!scroll.IsValid)
				throw new Exception("Setup: Initial scroll failed.");

			_scrollId = scroll.ScrollId ?? _scrollId;
		}
	}
}
