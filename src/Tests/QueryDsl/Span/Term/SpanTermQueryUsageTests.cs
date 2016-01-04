using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.SpanTerm
{
	public class SpanTermUsageTests : QueryDslUsageTestsBase
	{
		public SpanTermUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			span_term = new
			{
				user = new
				{
					_name = "named_query",
					boost = 1.1,
					value = "kimchy"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new SpanTermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Value = "kimchy",
			Field = "user"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanTerm(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field("user")
				.Value("kimchy")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanTermQuery>(a => a.SpanTerm)
		{
			q => q.Value = null,
			q => q.Value = string.Empty,
			q => q.Field = null,
		};
	}
}
