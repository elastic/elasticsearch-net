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

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsLookupQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsLookupQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermsQuery>(a => a.Terms)
		{
			q => q.Field = null,
			q => q.TermsLookup = null,
			q => q.TermsLookup.Id = null,
			q => q.TermsLookup.Index = null,
			q => q.TermsLookup.Path = null,
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			TermsLookup = new FieldLookup
			{
				Id = "12",
				Index = Index<Developer>(),
				Path = Field<Developer>(p => p.LastName),
				Routing = "myroutingvalue"
			}
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new
				{
					id = "12",
					index = "devs",
					path = "lastName",
					routing = "myroutingvalue"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.TermsLookup<Developer>(e => e
					.Path(p => p.LastName)
					.Id("12")
					.Routing("myroutingvalue")
				)
			);
	}
}
