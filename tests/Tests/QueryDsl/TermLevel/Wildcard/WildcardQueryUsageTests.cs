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

namespace Tests.QueryDsl.TermLevel.Wildcard
{
	public class WildcardQueryUsageTests : QueryDslUsageTestsBase
	{
		public WildcardQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IWildcardQuery>(a => a.Wildcard)
		{
			q => q.Field = null,
			q => q.Value = null,
			q => q.Value = string.Empty
		};

		protected override QueryContainer QueryInitializer => new WildcardQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "p*oj",
			Rewrite = MultiTermQueryRewrite.TopTermsBoost(10)
		};

		protected override object QueryJson => new
		{
			wildcard = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					rewrite = "top_terms_boost_10",
					value = "p*oj"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Wildcard(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("p*oj")
				.Rewrite(MultiTermQueryRewrite.TopTermsBoost(10))
			);
	}
}
