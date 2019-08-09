using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.QueryDsl.TermLevel.Range
{
	public class DateRangeQueryUsageTests : QueryDslUsageTestsBase
	{
		public DateRangeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IDateRangeQuery>(q => q.Range as IDateRangeQuery)
		{
			q => q.Field = null,
			q =>
			{
				q.GreaterThan = null;
				q.GreaterThanOrEqualTo = null;
				q.LessThan = null;
				q.LessThanOrEqualTo = null;
			},
			q =>
			{
				q.GreaterThan = default;
				q.GreaterThanOrEqualTo = default;
				q.LessThan = default;
				q.LessThanOrEqualTo = default;
			}
		};

		protected override QueryContainer QueryInitializer => new DateRangeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "lastActivity",
			GreaterThanOrEqualTo = DateMath.Now.Subtract("1y").RoundTo(DateMathTimeUnit.Month),
			LessThanOrEqualTo = DateMath.Now,
			TimeZone = "+01:00",
			Format = "dd/MM/yyyy||yyyy"
		};

		protected override object QueryJson => new
		{
			range = new
			{
				lastActivity = new
				{
					_name = "named_query",
					boost = 1.1,
					format = "dd/MM/yyyy||yyyy",
					gte = "now-1y/M",
					lte = "now",
					time_zone = "+01:00"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.DateRange(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.LastActivity)
				.GreaterThanOrEquals(DateMath.Now.Subtract("1y").RoundTo(DateMathTimeUnit.Month))
				.LessThanOrEquals(DateMath.Now)
				.Format("dd/MM/yyyy||yyyy")
				.TimeZone("+01:00")
			);
	}

}
