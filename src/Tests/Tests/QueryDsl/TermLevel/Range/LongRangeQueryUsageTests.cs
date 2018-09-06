using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.TermLevel.Range
{
	public class LongRangeQueryUsageTests : QueryDslUsageTestsBase
	{
		public LongRangeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			range = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					gt = 636634079999999999,
					gte = 636634080000000000,
					lt = 636634080000000000,
					lte = 636634079999999999,
					relation = "within"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new LongRangeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			GreaterThan = 636634079999999999,
			GreaterThanOrEqualTo = 636634080000000000,
			LessThan = 636634080000000000,
			LessThanOrEqualTo = 636634079999999999,
			Relation = RangeRelation.Within
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.LongRange(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.GreaterThan(636634079999999999)
				.GreaterThanOrEquals(636634080000000000)
				.LessThan(636634080000000000)
				.LessThanOrEquals(636634079999999999)
				.Relation(RangeRelation.Within)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ILongRangeQuery>(q => q.Range as ILongRangeQuery)
		{
			q=> q.Field = null,
			q=>
			{
				q.GreaterThan = null;
				q.GreaterThanOrEqualTo = null;
				q.LessThan = null;
				q.LessThanOrEqualTo = null;
			}
		};
	}
}
