using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Compound.Indices
{
	public class IndicesQueryUsageTests : QueryDslUsageTestsBase
	{
		public IndicesQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			indices = new
			{
				_name = "named_query",
				boost = 1.1,
				indices = new[] { "project" },
				no_match_query = new
				{
					match_all = new
					{
						_name = "no_match"
					}
				},
				query = new
				{
					match_all = new { }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new IndicesQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Indices = Index<Project>(),
			Query = new MatchAllQuery(),
			NoMatchQuery = new MatchAllQuery { Name ="no_match" }

		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Indices(c => c
				.Name("named_query")
				.Boost(1.1)
				.Indices(Index<Project>())
				.Query(qq => qq.MatchAll())
				.NoMatchQuery(qq => qq.MatchAll(m => m.Name("no_match")))
			);
	}
}
