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

namespace Tests.QueryDsl.TermLevel.Fuzzy
{
	public class FuzzyNumericQueryUsageTests : QueryDslUsageTestsBase
	{
		public FuzzyNumericQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFuzzyQuery<double?, double?>>(
			a => a.Fuzzy as IFuzzyQuery<double?, double?>
		)
		{
			q => q.Field = null,
			q => q.Value = null
		};

		protected override QueryContainer QueryInitializer => new FuzzyNumericQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Fuzziness = 2,
			Value = 12,
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
					fuzziness = 2.0,
					max_expansions = 100,
					prefix_length = 3,
					rewrite = "constant_score",
					transpositions = true,
					value = 12.0
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.FuzzyNumeric(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Fuzziness(2)
				.Value(12)
				.MaxExpansions(100)
				.PrefixLength(3)
				.Rewrite(MultiTermQueryRewrite.ConstantScore)
				.Transpositions()
			);
	}
}
