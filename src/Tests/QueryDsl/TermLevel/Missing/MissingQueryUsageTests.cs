using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Missing
{
	public class MissingQueryUsageTests : QueryDslUsageTestsBase
	{
		public MissingQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			missing = new
			{
				_name = "named_query",
				boost = 1.1,
				existence = true,
				field = "description",
				null_value = true
			}
		};

		protected override QueryContainer QueryInitializer => new MissingQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			NullValue = true,
			Existence = true
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Missing(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.NullValue()
				.Existence()
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMissingQuery>(a => a.Missing)
		{
			q => q.Field = null,
		};
	}
}