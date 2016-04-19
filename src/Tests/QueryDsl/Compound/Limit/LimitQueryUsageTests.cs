using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Limit
{
	/**
	* A `limit` query limits the number of documents (per shard) to execute on.
	*
	* WARNING: Deprecated in 2.0.0-beta1. Use the `terminate_after` parameter on the request instead.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-limit-query.html[limit query] for more details.
	*/
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
