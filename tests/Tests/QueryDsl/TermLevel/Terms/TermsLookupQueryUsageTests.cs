// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
