// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Span.Term
{
	public class SpanTermUsageTests : QueryDslUsageTestsBase
	{
		public SpanTermUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanTermQuery>(a => a.SpanTerm)
		{
			q => q.Value = null,
			q => q.Value = string.Empty,
			q => q.Field = null,
		};

		protected override QueryContainer QueryInitializer => new SpanTermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Value = "kimchy",
			Field = "user"
		};

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

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanTerm(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field("user")
				.Value("kimchy")
			);
	}
}
