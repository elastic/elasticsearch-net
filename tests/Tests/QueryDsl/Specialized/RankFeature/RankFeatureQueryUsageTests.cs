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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.RankFeature
{
	/**
	 * The rank_feature query is a specialized query that only works on `rank_feature` fields and `rank_features` fields.
	 * Its goal is to boost the score of documents based on the values of numeric features. It is typically put in a should clause of a bool query
	 * so that its score is added to the score of the query.
	 *
     * Compared to using `function_score` or other ways to modify the score, this query has the benefit of being able to efficiently
	 * skip non-competitive hits when track_total_hits is not set to true. Speedups may be spectacular.
	 *
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-rank-feature-query.html[rank feature query] for more details.
	*/
	public class RankFeatureQueryUsageTests : QueryDslUsageTestsBase
	{
		public RankFeatureQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IRankFeatureQuery>(a => a.RankFeature)
		{
			q =>
			{
				q.Field = null;
			}
		};

		protected override QueryContainer QueryInitializer => new RankFeatureQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.Rank),
			Function = new RankFeatureSaturationFunction()
		};

		protected override object QueryJson =>
			new { rank_feature = new { _name = "named_query", boost = 1.1, field = "rank", saturation = new { } } };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.RankFeature(rf => rf
				.Name("named_query")
				.Boost(1.1)
				.Field(f => f.Rank)
				.Saturation()
			);
	}

	public class RankFeatureQueryNoFunctionUsageTests : QueryDslUsageTestsBase
	{
		public RankFeatureQueryNoFunctionUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
		protected override QueryContainer QueryInitializer => new RankFeatureQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.Rank),
		};

		protected override object QueryJson =>
			new { rank_feature = new { _name = "named_query", boost = 1.1, field = "rank" } };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.RankFeature(rf => rf
				.Name("named_query")
				.Boost(1.1)
				.Field(f => f.Rank)
			);
	}

	[SkipVersion("<7.12.0", "Introduced in 7.12.0")]
	public class RankFeatureLinearFunctionUsageTests : QueryDslUsageTestsBase
	{
		public RankFeatureLinearFunctionUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
		protected override QueryContainer QueryInitializer => new RankFeatureQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.Rank),
			Function = new RankFeatureLinearFunction()
		};

		protected override object QueryJson =>
			new { rank_feature = new { _name = "named_query", boost = 1.1, field = "rank", linear = new { } } };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.RankFeature(rf => rf
				.Name("named_query")
				.Boost(1.1)
				.Field(f => f.Rank)
				.Linear());
	}
}
