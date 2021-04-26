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

namespace Tests.QueryDsl.TermLevel.Range
{
	public class LongRangeQueryUsageTests : QueryDslUsageTestsBase
	{
		public LongRangeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ILongRangeQuery>(q => q.Range as ILongRangeQuery)
		{
			q => q.Field = null,
			q =>
			{
				q.GreaterThan = null;
				q.GreaterThanOrEqualTo = null;
				q.LessThan = null;
				q.LessThanOrEqualTo = null;
			}
		};

		protected override QueryContainer QueryInitializer => new LongRangeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			GreaterThan = 636634079999999999,
			GreaterThanOrEqualTo = 636634080000000000,
			LessThan = 636634080000000000,
			LessThanOrEqualTo = 636634079999999999,
			Relation = RangeRelation.Within
		};

		protected override object QueryJson => new
		{
			range = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					gt = 636634079999999999,
					gte = 636634080000000000,
					lt = 636634080000000000,
					lte = 636634079999999999,
					relation = "within"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.LongRange(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.GreaterThan(636634079999999999)
				.GreaterThanOrEquals(636634080000000000)
				.LessThan(636634080000000000)
				.LessThanOrEquals(636634079999999999)
				.Relation(RangeRelation.Within)
			);
	}
}
