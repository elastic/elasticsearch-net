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
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Request
{
	public class IndicesBoostUsageTests : SearchUsageTestBase
	{
		public IndicesBoostUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			indices_boost = new object[]
			{
				new { project = 1.4 },
				new { devs = 1.3 }
			},
			query = new
			{
				match_all = new { }
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.IndicesBoost(b => b
				.Add("project", 1.4)
				.Add("devs", 1.3)
			)
			.Query(q => q
				.MatchAll()
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				IndicesBoost = new Dictionary<IndexName, double>
				{
					{ "project", 1.4 },
					{ "devs", 1.3 }
				},
				Query = new MatchAllQuery()
			};
	}
}
