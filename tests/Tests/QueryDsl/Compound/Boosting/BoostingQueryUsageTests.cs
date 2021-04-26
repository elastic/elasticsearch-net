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

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Boosting
{
	public class BoostingQueryUsageTests : QueryDslUsageTestsBase
	{
		public BoostingQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IBoostingQuery>(a => a.Boosting)
		{
			q =>
			{
				q.NegativeQuery = null;
				q.PositiveQuery = null;
			},
			q =>
			{
				q.NegativeQuery = ConditionlessQuery;
				q.PositiveQuery = ConditionlessQuery;
			},
		};

		protected override NotConditionlessWhen NotConditionlessWhen => new NotConditionlessWhen<IBoostingQuery>(a => a.Boosting)
		{
			q =>
			{
				q.NegativeQuery = VerbatimQuery;
				q.PositiveQuery = VerbatimQuery;
			},
			q =>
			{
				q.NegativeQuery = null;
				q.PositiveQuery = VerbatimQuery;
			},
			q =>
			{
				q.NegativeQuery = VerbatimQuery;
				q.PositiveQuery = null;
			}
		};

		protected override QueryContainer QueryInitializer => new BoostingQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			PositiveQuery = new MatchAllQuery { Name = "filter" },
			NegativeQuery = new MatchAllQuery() { Name = "query" },
			NegativeBoost = 1.12
		};

		protected override object QueryJson => new
		{
			boosting = new
			{
				_name = "named_query",
				boost = 1.1,
				negative = new
				{
					match_all = new { _name = "query" }
				},
				negative_boost = 1.12,
				positive = new
				{
					match_all = new { _name = "filter" }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Boosting(c => c
				.Name("named_query")
				.Boost(1.1)
				.Positive(qq => qq.MatchAll(m => m.Name("filter")))
				.Negative(qq => qq.MatchAll(m => m.Name("query")))
				.NegativeBoost(1.12)
			);
	}
}
