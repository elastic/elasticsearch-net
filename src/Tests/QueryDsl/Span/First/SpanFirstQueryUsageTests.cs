using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.SpanFirst
{
	public class SpanFirstUsageTests : QueryDslUsageTestsBase
	{
		public SpanFirstUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			span_first = new
			{
				_name = "named_query",
				boost = 1.1,
				match = new
				{
					span_term = new { name = new { value = "value" } }
				},
				end = 3
			}
		};

		protected override QueryContainer QueryInitializer => new SpanFirstQuery
		{
			Name = "named_query",
			Boost = 1.1,
			End = 3,
			Match = new SpanQuery
			{
				SpanTerm = new SpanTermQuery { Field = "name", Value = "value" }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanFirst(c => c
				.Name("named_query")
				.Boost(1.1)
				.Match(sq=>sq
					.SpanTerm(st=>st.Field(p=>p.Name).Value("value"))
				)
				.End(3)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanFirstQuery>(a => a.SpanFirst)
		{
			q => q.Match = null,
			q => q.Match = new SpanQuery { SpanTerm = new SpanTermQuery() } ,
		};
	}
}
