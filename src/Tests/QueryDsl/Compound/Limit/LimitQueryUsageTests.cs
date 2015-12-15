using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Compound.Limit
{
	public class LimitQueryUsageTests : QueryDslUsageTestsBase
	{
		public LimitQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			limit = new
			{
				_name = "named_query",
				boost = 1.1,
				limit = 100
			}
		};

		protected override QueryContainer QueryInitializer => new LimitQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Limit = 100
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Limit(c => c
				.Name("named_query")
				.Boost(1.1)
				.Limit(100)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ILimitQuery>(p => p.Limit)
		{
			q => q.Limit = null
		};
	}
}
