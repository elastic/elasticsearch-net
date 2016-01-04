using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.SpanOr
{
	public class SpanOrUsageTests : QueryDslUsageTestsBase
	{
		public SpanOrUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			span_or = new
			{
				_name = "named_query",
				boost = 1.1,
				clauses = new[] {
					new { span_term = new { field = new { value = "value1" } } },
					new { span_term = new { field = new { value = "value2" } } },
					new { span_term = new { field = new { value = "value3" } } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new SpanOrQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Clauses = new List<ISpanQuery>
			{
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value1" } },
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value2" } },
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value3" } }
			},
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanOr(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Clauses(
					c => c.SpanTerm(st => st.Field("field").Value("value1")),
					c => c.SpanTerm(st => st.Field("field").Value("value2")),
					c => c.SpanTerm(st => st.Field("field").Value("value3"))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanOrQuery>(a => a.SpanOr)
		{
			q => q.Clauses = null,
			q => q.Clauses = Enumerable.Empty<ISpanQuery>(),
			q => q.Clauses = new [] { new SpanQuery() },
		};
	}
}
