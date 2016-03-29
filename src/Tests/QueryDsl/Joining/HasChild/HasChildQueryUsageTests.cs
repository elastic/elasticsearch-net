using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.HasChild
{
	public class HasChildUsageTests : QueryDslUsageTestsBase
	{
		public HasChildUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			has_child = new
			{
				_name = "named_query",
				boost = 1.1,
				type = "developer",
				score_mode = "avg",
				min_children = 1,
				max_children = 5,
				query = new { match_all = new { } },
				inner_hits = new { explain = true }
			}
		};

		protected override QueryContainer QueryInitializer => new HasChildQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Type = Infer.Type<Developer>(),
			InnerHits = new InnerHits { Explain = true },
			MaxChildren = 5,
			MinChildren = 1,
			Query = new MatchAllQuery(),
			ScoreMode = ChildScoreMode.Average
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.HasChild<Developer>(c => c
				.Name("named_query")
				.Boost(1.1)
				.InnerHits(i=>i.Explain())
				.MaxChildren(5)
				.MinChildren(1)
				.ScoreMode(ChildScoreMode.Average)
				.Query(qq=>qq.MatchAll())
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IHasChildQuery>(a => a.HasChild)
		{
			q =>  q.Query = null,
			q =>  q.Query = ConditionlessQuery,
			q =>  q.Type = null,
		};
	}
}
