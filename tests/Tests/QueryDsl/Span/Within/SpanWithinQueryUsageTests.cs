// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Span.Within
{
	public class SpanWithinUsageTests : QueryDslUsageTestsBase
	{
		public SpanWithinUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanWithinQuery>(a => a.SpanWithin)
		{
			q =>
			{
				q.Big = null;
				q.Little = null;
			},
			q =>
			{
				q.Big = new SpanQuery();
				q.Little = new SpanQuery();
			},
		};

		protected override QueryContainer QueryInitializer => new SpanWithinQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Little = new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field1", Value = "hoya" } },
			Big = new SpanQuery { SpanTerm = new SpanTermQuery { Field = "field1", Value = "hoya2" } },
		};

		protected override object QueryJson => new
		{
			span_within = new
			{
				_name = "named_query",
				boost = 1.1,
				little = new
				{
					span_term = new { field1 = new { value = "hoya" } }
				},
				big = new
				{
					span_term = new { field1 = new { value = "hoya2" } }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanWithin(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Little(i => i
					.SpanTerm(st => st.Field("field1").Value("hoya"))
				)
				.Big(e => e
					.SpanTerm(st => st.Field("field1").Value("hoya2"))
				)
			);
	}
}
