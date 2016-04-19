using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Not
{
	public class NotQueryUsageTests : QueryDslUsageTestsBase
	{
		/**
		* A query that filters out matched documents using a query.
		*
		* WARNING: Deprecated in 2.1.0. Use the <<bool-queries, bool query>> with `must_not` clause instead.
		*
		* See the Elasticsearch documentation on {ref_current}/query-dsl-not-query.html[not query] for more details.
		*/
		public NotQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			not = new
			{
				_name = "named_query",
				boost = 1.1,
				filters = new[] {
					new { match_all = new { _name = "query1" } },
					new { match_all = new { _name = "query2" } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new NotQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Filters = new QueryContainer[] {
				new MatchAllQuery() { Name = "query1" },
				new MatchAllQuery() { Name = "query2" },
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Not(c => c
				.Name("named_query")
				.Boost(1.1)
				.Filters(
					qq => qq.MatchAll(m => m.Name("query1")),
					qq => qq.MatchAll(m => m.Name("query2"))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<INotQuery>(a => a.Not)
		{
			{ q=>q.Filters = null }, { q=> q.Filters = Enumerable.Empty<QueryContainer>() }, { q=>q.Filters = new [] { ConditionlessQuery } }
		};
	}
}
