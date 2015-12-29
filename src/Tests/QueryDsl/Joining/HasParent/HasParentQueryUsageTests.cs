using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.HasParent
{
	public class HasParentUsageTests : QueryDslUsageTestsBase
	{
		public HasParentUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			has_parent = new
			{
				_name = "named_query",
				boost = 1.1,
				type = "developer",
				score_mode = "score",
				query = new
				{
					match_all = new { }
				},
				inner_hits = new
				{
					explain = true
				}
			}
		};

		protected override QueryContainer QueryInitializer => new HasParentQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Type = Infer.Type<Developer>(),
			InnerHits = new InnerHits { Explain = true },
			Query = new MatchAllQuery(),
			ScoreMode = ParentScoreMode.Score
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.HasParent<Developer>(c => c
				.Name("named_query")
				.Boost(1.1)
				.InnerHits(i=>i.Explain())
				.ScoreMode(ParentScoreMode.Score)
				.Query(qq=>qq.MatchAll())
				
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IHasParentQuery>(a => a.HasParent)
		{
			q =>  q.Query = null,
			q =>  q.Query = ConditionlessQuery,
			q =>  q.Type = null,
		};
	}
}
