using Nest;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Other
{
	public class MissingQueryUsageTests : QueryDslUsageTestsBase
	{
		public MissingQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
		protected override object QueryJson => new
		{
			missing = new
			{
				field = "testfield",
				existence = true,
				null_value = true
			}
		};

		protected override QueryContainer QueryInitializer => new MissingQuery()
		{
			Field = "testfield",
			Existence = true,
			NullValue = true
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Missing(m => m
				.NullValue()
				.Existence()
				.Field("testfield"));
	}
}
