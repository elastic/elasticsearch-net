// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Fuzzy
{
	public class FuzzyQueryUsageTests : QueryDslUsageTestsBase
	{
		public FuzzyQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFuzzyQuery<string, Fuzziness>>(
			a => a.Fuzzy as IFuzzyQuery<string, Fuzziness>
		)
		{
			q => q.Field = null,
			q => q.Value = null
		};

		protected override QueryContainer QueryInitializer => new FuzzyQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Fuzziness = Fuzziness.Auto,
			Value = "ki",
			MaxExpansions = 100,
			PrefixLength = 3,
			Rewrite = MultiTermQueryRewrite.ConstantScore,
			Transpositions = true
		};

		protected override object QueryJson => new
		{
			fuzzy = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					fuzziness = "AUTO",
					max_expansions = 100,
					prefix_length = 3,
					rewrite = "constant_score",
					transpositions = true,
					value = "ki"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Fuzzy(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Fuzziness(Fuzziness.Auto)
				.Value("ki")
				.MaxExpansions(100)
				.PrefixLength(3)
				.Rewrite(MultiTermQueryRewrite.ConstantScore)
				.Transpositions()
			);
	}
}
