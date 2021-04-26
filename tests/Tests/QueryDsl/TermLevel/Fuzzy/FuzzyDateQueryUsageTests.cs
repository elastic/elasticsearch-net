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
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Fuzzy
{
	public class FuzzyDateQueryUsageTests : QueryDslUsageTestsBase
	{
		public FuzzyDateQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFuzzyQuery<DateTime?, Time>>(
			a => a.Fuzzy as IFuzzyQuery<DateTime?, Time>
		)
		{
			q => q.Field = null,
			q => q.Value = null
		};

		protected override QueryContainer QueryInitializer => new FuzzyDateQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Fuzziness = TimeSpan.FromDays(2),
			Value = Project.Instance.StartedOn,
			MaxExpansions = 100,
			PrefixLength = 3,
			Rewrite = MultiTermQueryRewrite.ConstantScore,
			Transpositions = true
		};

		protected override object QueryJson => new
		{
			fuzzy = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					fuzziness = "2d",
					max_expansions = 100,
					prefix_length = 3,
					rewrite = "constant_score",
					transpositions = true,
					value = "2015-01-01T00:00:00"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.FuzzyDate(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Fuzziness(TimeSpan.FromDays(2))
				.Value(Project.Instance.StartedOn)
				.MaxExpansions(100)
				.PrefixLength(3)
				.Rewrite(MultiTermQueryRewrite.ConstantScore)
				.Transpositions()
			);
	}
}
