using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Compound.Bool
{
	public class BoolQueryUsageTests : QueryDslUsageTestsBase
	{
		public BoolQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
		{
			@bool = new
			{
				must = new[]
				{
					new { match_all = new { } }
				},
				must_not = new[]
				{
					new { match_all = new { } }
				},
				should = new[]
				{
					new { match_all = new { } }
				},
				filter = new[]
				{
					new { match_all = new { } }
				},
				minimum_should_match = 1,
				boost = 2.0,
			}
		};

		protected override QueryContainer QueryInitializer =>
			new BoolQuery()
			{
				MustNot = new QueryContainer[] { new MatchAllQuery() },
				Should = new QueryContainer[] { new MatchAllQuery() },
				Must = new QueryContainer[] { new MatchAllQuery() },
				Filter = new QueryContainer[] { new MatchAllQuery() },
				MinimumShouldMatch = 1,
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

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IBoolQuery>(a => a.Bool)
		{
			q=> {
				q.MustNot = null;
				q.Should = null;
				q.Must = null;
				q.Filter = null;
			},
			q => {
				q.MustNot = Enumerable.Empty<QueryContainer>();
				q.Should = Enumerable.Empty<QueryContainer>();
				q.Must = Enumerable.Empty<QueryContainer>();
				q.Filter = Enumerable.Empty<QueryContainer>();
			},
			q => {
				q.MustNot = new [] { ConditionlessQuery };
				q.Should = new [] { ConditionlessQuery };
				q.Must = new [] { ConditionlessQuery };
				q.Filter = new [] { ConditionlessQuery };
			},
		};
	}
}
