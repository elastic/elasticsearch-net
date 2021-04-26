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

namespace Tests.QueryDsl.Joining.HasParent
{
	public class HasParentUsageTests : QueryDslUsageTestsBase
	{
		public HasParentUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IHasParentQuery>(a => a.HasParent)
		{
			q => q.Query = null,
			q => q.Query = ConditionlessQuery,
			q => q.ParentType = null,
		};

		protected override QueryContainer QueryInitializer => new HasParentQuery
		{
			Name = "named_query",
			Boost = 1.1,
			ParentType = Infer.Relation<Developer>(),
			InnerHits = new InnerHits { Explain = true },
			Query = new MatchAllQuery(),
			Score = true,
			IgnoreUnmapped = true
		};

		protected override object QueryJson => new
		{
			has_parent = new
			{
				_name = "named_query",
				boost = 1.1,
				parent_type = "developer",
				score = true,
				ignore_unmapped = true,
				query = new
				{
					match_all = new { }
				},
				inner_hits = new
				{
					explain = true
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.HasParent<Developer>(c => c
				.Name("named_query")
				.Boost(1.1)
				.InnerHits(i => i.Explain())
				.Score()
				.Query(qq => qq.MatchAll())
				.IgnoreUnmapped()
			);
	}
}
