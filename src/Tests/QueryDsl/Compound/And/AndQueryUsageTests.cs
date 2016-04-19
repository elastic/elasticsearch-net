using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.And
{
	/**
	* A query that matches documents using the `AND` boolean operator on other queries.
	*
	* WARNING: Deprecated in 2.0.0-beta1. Use the <<bool-queries, bool query>> instead.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-and-query.html[and query] for more details.
	*/
	public class AndQueryUsageTests : QueryDslUsageTestsBase
	{
		public AndQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			and = new
			{
				_name = "named_query",
				boost = 1.1,
				filters = new[] {
					new { match_all = new { _name = "query1" } },
					new { match_all = new { _name = "query2" } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new AndQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Filters = new QueryContainer[] {
				new MatchAllQuery() { Name = "query1" },
				new MatchAllQuery() { Name = "query2" },
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.And(c => c
				.Name("named_query")
				.Boost(1.1)
				.Filters(
					qq => qq.MatchAll(m => m.Name("query1")),
					qq => qq.MatchAll(m => m.Name("query2"))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IAndQuery>(a => a.And)
		{
			{ q=>q.Filters = null }, { q=> q.Filters = Enumerable.Empty<QueryContainer>() }, { q=>q.Filters = new [] { ConditionlessQuery } }
		};
	}
}
