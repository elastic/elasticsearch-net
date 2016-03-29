using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Range
{
	public class TermRangeQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermRangeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			range = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					gt = "foo",
					gte = "foof",
					lt = "bar",
					lte = "barb"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermRangeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			GreaterThan = "foo",
			GreaterThanOrEqualTo = "foof",
			LessThan = "bar",
			LessThanOrEqualTo = "barb"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.TermRange(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.GreaterThan("foo")
				.GreaterThanOrEquals("foof")
				.LessThan("bar")
				.LessThanOrEquals("barb")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermRangeQuery>(q => q.Range as ITermRangeQuery)
		{
			q=> q.Field = null,
			q=>
			{
				q.GreaterThan = null;
				q.GreaterThanOrEqualTo = null;
				q.LessThan = null;
				q.LessThanOrEqualTo = null;
			},
            q =>
			{
				q.GreaterThan = string.Empty;
				q.GreaterThanOrEqualTo = string.Empty;
				q.LessThan = string.Empty;
				q.LessThanOrEqualTo = string.Empty;
			},
		};
	}
}