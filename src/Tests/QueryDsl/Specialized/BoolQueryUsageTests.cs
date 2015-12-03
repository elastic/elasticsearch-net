using Nest;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using System.Linq;

namespace Tests.QueryDsl
{
	public class BoolQueryUsageTests : QueryDslUsageTestsBase
	{
		public BoolQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}


		protected override object QueryJson => new
		{
			@bool = new
			{
				boost = 2.0,
				must_not = new []
				{
					new { match_all = new { } }
				},
				should = new[]
				{
					new { match_all = new { } }
				},
				must = new[]
				{
					new { match_all = new { } }
				},
				filter = new []
				{
					new { match_all = new { } }
				},
				minimum_should_match = "1",
			}
		};

		protected override QueryContainer QueryInitializer =>
			new BoolQuery()
			{
				MustNot = new IQueryContainer[] { new QueryContainer(new MatchAllQuery()) },
				Should = new IQueryContainer[] { new QueryContainer(new MatchAllQuery()) },
				Must = new IQueryContainer[] { new QueryContainer(new MatchAllQuery()) },
				Filter = new IQueryContainer[] { new QueryContainer(new MatchAllQuery()) },
				MinimumShouldMatch = "1",
				Boost = 2
			};
        protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Bool(b => b
				.MustNot(m => m.MatchAll())
				.Should(m => m.MatchAll())
				.Must(m => m.MatchAll())
				.Filter(f => f.MatchAll())
				.MinimumShouldMatch(1)
				.Boost(2));
	}
}
