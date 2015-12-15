using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Filtered
{
	public class FilteredQueryUsageTests : QueryDslUsageTestsBase
	{
		public FilteredQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			filtered = new
			{
				_name = "named_query",
				boost = 1.1,
				filter = new { match_all = new { _name = "filter" } },
				query = new { match_all = new { _name = "query" } }
			}
		};

		protected override QueryContainer QueryInitializer => new FilteredQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Filter = new MatchAllQuery { Name ="filter" },
			Query = new MatchAllQuery() { Name = "query" },
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Filtered(c => c
				.Name("named_query")
				.Boost(1.1)
				.Filter(qq => qq.MatchAll(m => m.Name("filter")))
				.Query(qq => qq.MatchAll(m => m.Name("query")))
			);


		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFilteredQuery>(a => a.Filtered)
		{
			q=> {
				q.Filter = null;
				q.Query = null;
			},
			q => {
				q.Filter =  ConditionlessQuery;
				q.Query =  ConditionlessQuery;
			},
		};
	}
}
