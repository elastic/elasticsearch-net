using Nest;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Other
{
	public class ExistsQueryUsageTests : QueryDslUsageTestsBase
	{
		public ExistsQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			exists = new
			{
				field = "testfield"
			}
		};

		protected override QueryContainer QueryInitializer => new ExistsQuery
		{
			Field = "testfield"
		};


		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Exists(e => e.Field("testfield"));

	}
}
