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

namespace Tests.QueryDsl.Compound.ConstantScore
{
	public class ConstantScoreQueryUsageTests : QueryDslUsageTestsBase
	{
		public ConstantScoreQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IConstantScoreQuery>(a => a.ConstantScore)
		{
			q => q.Filter = null,
			q => q.Filter = ConditionlessQuery,
		};

		protected override NotConditionlessWhen NotConditionlessWhen => new NotConditionlessWhen<IConstantScoreQuery>(a => a.ConstantScore)
		{
			q => q.Filter = VerbatimQuery
		};

		protected override QueryContainer QueryInitializer => new ConstantScoreQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Filter = new MatchAllQuery { Name = "filter" },
		};

		protected override object QueryJson => new
		{
			constant_score = new
			{
				_name = "named_query",
				boost = 1.1,
				filter = new
				{
					match_all = new
					{
						_name = "filter"
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.ConstantScore(c => c
				.Name("named_query")
				.Boost(1.1)
				.Filter(qq => qq.MatchAll(m => m.Name("filter")))
			);
	}
}
