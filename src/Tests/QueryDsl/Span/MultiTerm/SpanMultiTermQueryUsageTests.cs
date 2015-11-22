using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

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
	}
}
