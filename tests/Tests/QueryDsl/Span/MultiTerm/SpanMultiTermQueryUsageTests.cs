// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Span.MultiTerm
{
	public class SpanMultiTermUsageTests : QueryDslUsageTestsBase
	{
		public SpanMultiTermUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanMultiTermQuery>(a => a.SpanMultiTerm)
		{
			q => q.Match = null,
			q => q.Match = ConditionlessQuery,
		};

		protected override QueryContainer QueryInitializer => new SpanMultiTermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Match = new PrefixQuery { Field = Infer.Field<Project>(f => f.Description), Value = "pre-*" }
		};

		protected override object QueryJson => new
		{
			span_multi = new
			{
				_name = "named_query",
				boost = 1.1,
				match = new
				{
					prefix = new { description = new { value = "pre-*" } }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanMultiTerm(c => c
				.Name("named_query")
				.Boost(1.1)
				.Match(sq => sq
					.Prefix(pr => pr.Field(p => p.Description).Value("pre-*"))
				)
			);
	}
}
