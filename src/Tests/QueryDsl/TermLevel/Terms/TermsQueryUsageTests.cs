using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { "term1", "term2" },
				disable_coord = true,
				minimum_should_match = 2
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = new [] { "term1", "term2" },
			DisableCoord = true,
			MinimumShouldMatch = 2
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.DisableCoord()
				.MinimumShouldMatch(MinimumShouldMatch.Fixed(2))
				.Terms("term1", "term2")
			);
	}
}