using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Joining.SpanMultiTerm
{
	public class SpanMultiTermUsageTests : QueryDslUsageTestsBase
	{
		public SpanMultiTermUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			span_multi = new
			{
				_name = "named_query",
				boost = 1.1,
				match = new
				{
					prefix = new { name = new { value = "pre-*" } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new SpanMultiTermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Match = new PrefixQuery { Field = "name", Value = "pre-*" }
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanMultiTerm(c => c
				.Name("named_query")
				.Boost(1.1)
				.Match(sq=>sq
					.Prefix(pr=>pr.Field(p=>p.Name).Value("pre-*"))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanMultiTermQuery>(a => a.SpanMultiTerm)
		{
			q => q.Match = null,
			q => q.Match = ConditionlessQuery,
		};
	}
}
