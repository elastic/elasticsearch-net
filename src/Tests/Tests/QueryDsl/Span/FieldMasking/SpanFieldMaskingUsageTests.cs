using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Span.FieldMasking
{
	public class SpanFieldMaskingUsageTests : QueryDslUsageTestsBase
	{
		public SpanFieldMaskingUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			field_masking_span = new
			{
				_name = "named_query",
				boost = 1.1,
				field = "name",
				query = new
				{
					span_term = new { description = new { value = "dolorem" } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new SpanFieldMaskingQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = Infer.Field<Project>(p => p.Name),
			Query = new SpanQuery
			{
				SpanTerm = new SpanTermQuery
				{
					Field = Infer.Field<Project>(p => p.Description),
					Value = "dolorem"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanFieldMasking(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Name)
				.Query(sq=>sq
					.SpanTerm(st=>st.Field(p=>p.Description).Value("dolorem"))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanFieldMaskingQuery>(a => a.SpanFieldMasking)
		{
			q => q.Query = null,
			q => q.Field = null ,
		};
	}
}
