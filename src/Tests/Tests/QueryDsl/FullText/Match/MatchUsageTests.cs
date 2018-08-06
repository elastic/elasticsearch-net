using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.Match
{
	public class MatchUsageTests : QueryDslUsageTestsBase
	{
		public MatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			match = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello world",
					analyzer = "standard",
					fuzzy_rewrite = "top_terms_blended_freqs_10",
					fuzziness = "AUTO",
					fuzzy_transpositions = true,
					cutoff_frequency = 0.001,
					lenient = true,
					minimum_should_match = 2,
			        @operator = "or",
					auto_generate_synonyms_phrase_query = false
				}
			}

		};

		protected override QueryContainer QueryInitializer => new MatchQuery
		{
			Field = Field<Project>(p=>p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			CutoffFrequency = 0.001,
			Query = "hello world",
			Fuzziness = Fuzziness.Auto,
			FuzzyTranspositions = true,
			MinimumShouldMatch = 2,
			FuzzyRewrite = MultiTermQueryRewrite.TopTermsBlendedFreqs(10),
			Lenient = true,
			Operator = Operator.Or,
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Match(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
				.Query("hello world")
				.Fuzziness(Fuzziness.Auto)
				.Lenient()
				.FuzzyTranspositions()
				.MinimumShouldMatch(2)
				.Operator(Operator.Or)
				.FuzzyRewrite(MultiTermQueryRewrite.TopTermsBlendedFreqs(10))
				.Name("named_query")
				.AutoGenerateSynonymsPhraseQuery(false)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMatchQuery>(a => a.Match)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};
	}
}
