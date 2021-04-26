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
using static Nest.Infer;

// CommonTerms is deprecated in 7.3.0
#pragma warning disable 618,612

namespace Tests.QueryDsl.FullText.CommonTerms
{
	public class CommonTermsUsageTests : QueryDslUsageTestsBase
	{
		public CommonTermsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ICommonTermsQuery>(a => a.CommonTerms)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new CommonTermsQuery()
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			CutoffFrequency = 0.001,
			HighFrequencyOperator = Operator.And,
			LowFrequencyOperator = Operator.Or,
			MinimumShouldMatch = 1,
			Name = "named_query",
			Query = "nelly the elephant not as a"
		};

		protected override object QueryJson => new
		{
			common = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "nelly the elephant not as a",
					cutoff_frequency = 0.001,
					low_freq_operator = "or",
					high_freq_operator = "and",
					minimum_should_match = 1,
					analyzer = "standard",
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.CommonTerms(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
				.HighFrequencyOperator(Operator.And)
				.LowFrequencyOperator(Operator.Or)
				.MinimumShouldMatch(1)
				.Name("named_query")
				.Query("nelly the elephant not as a")
			);
	}
}
