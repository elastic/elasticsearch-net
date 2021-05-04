// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Regexp
{
	public class RegexpQueryUsageTests : QueryDslUsageTestsBase
	{
		public RegexpQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IRegexpQuery>(a => a.Regexp)
		{
			q => q.Field = null,
			q => q.Value = null,
			q => q.Value = string.Empty
		};

		protected override QueryContainer QueryInitializer => new RegexpQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "s.*y",
			Flags = "INTERSECTION|COMPLEMENT|EMPTY",
			MaximumDeterminizedStates = 20000,
			Rewrite = MultiTermQueryRewrite.TopTerms(10)
		};

		protected override object QueryJson => new
		{
			regexp = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					flags = "INTERSECTION|COMPLEMENT|EMPTY",
					max_determinized_states = 20000,
					value = "s.*y",
					rewrite = "top_terms_10"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Regexp(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("s.*y")
				.Flags("INTERSECTION|COMPLEMENT|EMPTY")
				.MaximumDeterminizedStates(20000)
				.Rewrite(MultiTermQueryRewrite.TopTerms(10))
			);
	}
}
