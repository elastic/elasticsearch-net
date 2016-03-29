using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Boosting
{
	public class BoostingQueryUsageTests : QueryDslUsageTestsBase
	{
		public BoostingQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			boosting = new
			{
				_name = "named_query",
				boost = 1.1,
				negative = new
				{
					match_all = new { _name = "query" }
				},
				negative_boost = 1.12,
				positive = new
				{
					match_all = new { _name = "filter" }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new BoostingQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			PositiveQuery = new MatchAllQuery { Name ="filter" },
			NegativeQuery= new MatchAllQuery() { Name = "query" },
			NegativeBoost = 1.12
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Boosting(c => c
				.Name("named_query")
				.Boost(1.1)
				.Positive(qq => qq.MatchAll(m => m.Name("filter")))
				.Negative(qq => qq.MatchAll(m => m.Name("query")))
				.NegativeBoost(1.12)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IBoostingQuery>(a => a.Boosting)
		{
			q=> {
				q.NegativeQuery = null;
				q.PositiveQuery = null;
			},
			q => {
				q.NegativeQuery =  ConditionlessQuery;
				q.PositiveQuery =  ConditionlessQuery;
			},
		};
	}
}
