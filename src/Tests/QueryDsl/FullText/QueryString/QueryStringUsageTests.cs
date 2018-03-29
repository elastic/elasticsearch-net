using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.QueryString
{
	public class QueryStringUsageTests : QueryDslUsageTestsBase
	{
		public QueryStringUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			query_string = new
			{
				_name = "named_query",
				boost = 1.1,
				query = "hello world",
				default_field = "description",
				default_operator = "or",
				analyzer = "standard",
				quote_analyzer = "quote-an",
				allow_leading_wildcard = true,
				fuzzy_max_expansions = 3,
				fuzziness = "AUTO",
				fuzzy_prefix_length = 2,
				analyze_wildcard = true,
				max_determinized_states = 2,
				minimum_should_match = 2,
				lenient = true,
				fields = new[] { "description", "myOtherField" },
				tie_breaker = 1.2,
				rewrite = "constant_score",
				fuzzy_rewrite = "constant_score",
				quote_field_suffix = "'",
				escape = true,
				auto_generate_synonyms_phrase_query = false
			}
		};

		protected override QueryContainer QueryInitializer => new QueryStringQuery
		{
			Fields = Field<Project>(p=>p.Description).And("myOtherField"),
			Boost = 1.1,
			Name = "named_query",
			Query = "hello world",
			DefaultField = Field<Project>(p=>p.Description),
			DefaultOperator = Operator.Or,
			Analyzer = "standard",
			QuoteAnalyzer = "quote-an",
			AllowLeadingWildcard = true,
			MaximumDeterminizedStates = 2,
			Escape = true,
			FuzzyPrefixLength = 2,
			FuzzyMaxExpansions = 3,
			FuzzyRewrite = MultiTermQueryRewrite.ConstantScore,
			Rewrite = MultiTermQueryRewrite.ConstantScore,
			Fuzziness = Fuzziness.Auto,
			TieBreaker = 1.2,
			AnalyzeWildcard = true,
			MinimumShouldMatch = 2,
			QuoteFieldSuffix = "'",
			Lenient = true,
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.QueryString(c => c
				.Name("named_query")
				.Boost(1.1)
				.Fields(f => f.Field(p=>p.Description).Field("myOtherField"))
				.Query("hello world")
				.DefaultField(p=>p.Description)
				.DefaultOperator(Operator.Or)
				.Analyzer("standard")
				.QuoteAnalyzer("quote-an")
				.AllowLeadingWildcard()
				.MaximumDeterminizedStates(2)
				.Escape()
				.FuzzyPrefixLength(2)
				.FuzzyMaxExpansions(3)
				.FuzzyRewrite(MultiTermQueryRewrite.ConstantScore)
				.Rewrite(MultiTermQueryRewrite.ConstantScore)
				.Fuzziness(Fuzziness.Auto)
				.TieBreaker(1.2)
				.AnalyzeWildcard()
				.MinimumShouldMatch(2)
				.QuoteFieldSuffix("'")
				.Lenient()
				.AutoGenerateSynonymsPhraseQuery(false)
			);
#pragma warning restore 618 // usage of lowercase_expanded_terms and locale

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IQueryStringQuery>(a => a.QueryString)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
		};
	}
}
