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

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Joining.HasChild
{
	public class HasChildUsageTests : QueryDslUsageTestsBase
	{
		public HasChildUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IHasChildQuery>(a => a.HasChild)
		{
			q => q.Query = null,
			q => q.Query = ConditionlessQuery,
			q => q.Type = null,
		};

		protected override QueryContainer QueryInitializer => new HasChildQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Type = Infer.Relation<CommitActivity>(),
			InnerHits = new InnerHits { Explain = true },
			MaxChildren = 5,
			MinChildren = 1,
			Query = new MatchAllQuery(),
			ScoreMode = ChildScoreMode.Average
		};

		protected override object QueryJson => new
		{
			has_child = new
			{
				_name = "named_query",
				boost = 1.1,
				type = "commits",
				score_mode = "avg",
				min_children = 1,
				max_children = 5,
				query = new { match_all = new { } },
				inner_hits = new { explain = true }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.HasChild<CommitActivity>(c => c
				.Name("named_query")
				.Boost(1.1)
				.InnerHits(i => i.Explain())
				.MaxChildren(5)
				.MinChildren(1)
				.ScoreMode(ChildScoreMode.Average)
				.Query(qq => qq.MatchAll())
			);
	}
}
