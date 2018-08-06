using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl
{
	public class MatchNoneQueryUsageTests : QueryDslUsageTestsBase
	{
		public MatchNoneQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object QueryJson => new
		{
			match_none = new
			{
				_name = "named_query",
				boost = 1.1
			}
		};

		protected override QueryContainer QueryInitializer => new MatchNoneQuery
		{
			Name = "named_query",
			Boost = 1.1
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MatchNone(c => c
				.Name("named_query")
				.Boost(1.1)
			);
	}
}
