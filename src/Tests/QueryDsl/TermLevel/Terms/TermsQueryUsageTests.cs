using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsQueryUsageTests : QueryDslUsageTestsBase
	{
		protected virtual string[] ExpectedTerms => new [] { "term1", "term2" };

		public TermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = ExpectedTerms,
				disable_coord = true,
				minimum_should_match = 2
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = ExpectedTerms,
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

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermsQuery>(a => a.Terms)
		{
			q => q.Field = null,
			q => q.Terms = null,
			q => q.Terms = Enumerable.Empty<object>(),
			q => q.Terms = new [] { "" }
		};
	}

	public class SingleTermTermsQueryUsageTests : TermsQueryUsageTests
	{
		protected override string[] ExpectedTerms => new [] { "term1"  };

		public SingleTermTermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.DisableCoord()
				.MinimumShouldMatch(MinimumShouldMatch.Fixed(2))
				.Terms("term1")
			);
	}


}
