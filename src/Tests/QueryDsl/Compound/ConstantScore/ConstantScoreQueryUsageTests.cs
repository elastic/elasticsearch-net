using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.ConstantScore
{
	public class ConstantScoreQueryUsageTests : QueryDslUsageTestsBase
	{
		public ConstantScoreQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			constant_score = new
			{
				_name = "named_query",
				boost = 1.1,
				filter = new
				{
					match_all = new
					{
						_name = "filter"
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer => new ConstantScoreQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Filter = new MatchAllQuery { Name = "filter" },
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.ConstantScore(c => c
				.Name("named_query")
				.Boost(1.1)
				.Filter(qq => qq.MatchAll(m => m.Name("filter")))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IConstantScoreQuery>(a => a.ConstantScore)
		{
			q => q.Filter = null ,
			q => q.Filter =  ConditionlessQuery,
		};
	}
}
