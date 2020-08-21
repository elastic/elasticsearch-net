// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.MultiMatch
{
	[SkipVersion(">=8.0.0-SNAPSHOT", "")]
	public class MultiMatchUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMultiMatchQuery>(a => a.MultiMatch)
		{
			q => q.Query = null,
			q => q.Query = string.Empty
		};

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Fields = Field<Project>(p => p.Description).And("myOtherField"),
			Query = "hello world",
			Analyzer = "standard",
			Boost = 1.1,
			Slop = 2,
			Fuzziness = Fuzziness.Auto,
			PrefixLength = 2,
			MaxExpansions = 2,
			Operator = Operator.Or,
			MinimumShouldMatch = 2,
			FuzzyRewrite = MultiTermQueryRewrite.ConstantScoreBoolean,
			TieBreaker = 1.1,
			CutoffFrequency = 0.001,
			Lenient = true,
			ZeroTermsQuery = ZeroTermsQuery.All,
			Name = "named_query",
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override object QueryJson => new
		{
			multi_match = new
			{
				_name = "named_query",
				boost = 1.1,
				query = "hello world",
				analyzer = "standard",
				fuzzy_rewrite = "constant_score_boolean",
				fuzziness = "AUTO",
				cutoff_frequency = 0.001,
				prefix_length = 2,
				max_expansions = 2,
				slop = 2,
				lenient = true,
				tie_breaker = 1.1,
				minimum_should_match = 2,
				@operator = "or",
				fields = new[]
				{
					"description",
					"myOtherField"
				},
				zero_terms_query = "all",
				auto_generate_synonyms_phrase_query = false
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				.Fields(f => f.Field(p => p.Description).Field("myOtherField"))
				.Query("hello world")
				.Analyzer("standard")
				.Boost(1.1)
				.Slop(2)
				.Fuzziness(Fuzziness.Auto)
				.PrefixLength(2)
				.MaxExpansions(2)
				.Operator(Operator.Or)
				.MinimumShouldMatch(2)
				.FuzzyRewrite(MultiTermQueryRewrite.ConstantScoreBoolean)
				.TieBreaker(1.1)
				.CutoffFrequency(0.001)
				.Lenient()
				.ZeroTermsQuery(ZeroTermsQuery.All)
				.Name("named_query")
				.AutoGenerateSynonymsPhraseQuery(false)
			);
	}

	/**[float]
	 * === Multi match with boost usage
	 */
	public class MultiMatchWithBoostUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchWithBoostUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Fields = Field<Project>(p => p.Description, 2.2).And("myOtherField^0.3"),
			Query = "hello world",
		};

		protected override object QueryJson => new
		{
			multi_match = new
			{
				query = "hello world",
				fields = new[]
				{
					"description^2.2",
					"myOtherField^0.3"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				.Fields(Field<Project>(p => p.Description, 2.2).And("myOtherField^0.3"))
				.Query("hello world")
			);
	}

	/**[float]
	 * === Multi match with no fields specified
	 *
	 * Starting with Elasticsearch 6.1.0+, it's possible to send a Multi Match query without providing any fields.
	 * When no fields are provided the Multi Match query will use the fields defined in the index setting `index.query.default_field`
	 * (which in turns defaults to `*`).
	 */
	public class MultiMatchWithNoFieldsSpecifiedUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchWithNoFieldsSpecifiedUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Query = "hello world",
		};

		protected override object QueryJson => new
		{
			multi_match = new
			{
				query = "hello world"
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				.Query("hello world")
			);
	}
}
