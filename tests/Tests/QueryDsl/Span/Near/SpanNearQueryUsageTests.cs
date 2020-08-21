// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Span.Near
{
	public class SpanNearUsageTests : QueryDslUsageTestsBase
	{
		public SpanNearUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanNearQuery>(a => a.SpanNear)
		{
			q => q.Clauses = null,
			q => q.Clauses = Enumerable.Empty<ISpanQuery>(),
			q => q.Clauses = new[] { new SpanQuery() },
		};

		protected override QueryContainer QueryInitializer => new SpanNearQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Clauses = new List<ISpanQuery>
			{
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value1" } },
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value2" } },
				new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field", Value = "value3" } },
				new SpanQuery { SpanGap = new SpanGapQuery { Field = "field", Width = 2 } }
			},
			Slop = 12,
			InOrder = true,
		};

		protected override object QueryJson => new
		{
			span_near = new
			{
				clauses = new object[]
				{
					new { span_term = new { field = new { value = "value1" } } },
					new { span_term = new { field = new { value = "value2" } } },
					new { span_term = new { field = new { value = "value3" } } },
					new { span_gap = new { field = 2 } }
				},
				slop = 12,
				in_order = true,
				_name = "named_query",
				boost = 1.1
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanNear(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Clauses(
					c => c.SpanTerm(st => st.Field("field").Value("value1")),
					c => c.SpanTerm(st => st.Field("field").Value("value2")),
					c => c.SpanTerm(st => st.Field("field").Value("value3")),
					c => c.SpanGap(st => st.Field("field").Width(2))
				)
				.Slop(12)
				.InOrder()
			);
	}
}
