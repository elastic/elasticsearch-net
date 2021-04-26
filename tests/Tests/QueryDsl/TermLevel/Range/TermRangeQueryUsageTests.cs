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
	public class TermRangeQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermRangeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermRangeQuery>(q => q.Range as ITermRangeQuery)
		{
			q => q.Field = null,
			q =>
			{
				q.GreaterThan = null;
				q.GreaterThanOrEqualTo = null;
				q.LessThan = null;
				q.LessThanOrEqualTo = null;
			},
			q =>
			{
				q.GreaterThan = string.Empty;
				q.GreaterThanOrEqualTo = string.Empty;
				q.LessThan = string.Empty;
				q.LessThanOrEqualTo = string.Empty;
			},
		};

		protected override QueryContainer QueryInitializer => new TermRangeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			GreaterThan = "foo",
			GreaterThanOrEqualTo = "foof",
			LessThan = "bar",
			LessThanOrEqualTo = "barb"
		};

		protected override object QueryJson => new
		{
			range = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					gt = "foo",
					gte = "foof",
					lt = "bar",
					lte = "barb"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.TermRange(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.GreaterThan("foo")
				.GreaterThanOrEquals("foof")
				.LessThan("bar")
				.LessThanOrEquals("barb")
			);
	}
}
