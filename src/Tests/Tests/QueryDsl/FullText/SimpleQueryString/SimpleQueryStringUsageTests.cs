using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;


namespace Tests.QueryDsl.FullText.SimpleQueryString
{
	public class SimpleQueryStringUsageTests : QueryDslUsageTestsBase
	{
		public SimpleQueryStringUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			simple_query_string = new
			{
				_name = "named_query",
				boost = 1.1,
				fields = new[] { "description", "myOtherField" },
				query = "hello world",
				analyzer = "standard",
				default_operator = "or",
				flags = "AND|NEAR",
				lenient = true,
				analyze_wildcard = true,
				minimum_should_match = "30%",
				fuzzy_prefix_length = 0,
				fuzzy_max_expansions = 50,
				fuzzy_transpositions = true,
				auto_generate_synonyms_phrase_query = false
			}
		};

		protected override QueryContainer QueryInitializer => new SimpleQueryStringQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Fields = Field<Project>(p=>p.Description).And("myOtherField"),
			Query = "hello world",
			Analyzer = "standard",
			DefaultOperator = Operator.Or,
			Flags = SimpleQueryStringFlags.And|SimpleQueryStringFlags.Near,
			Lenient = true,
			AnalyzeWildcard = true,
			MinimumShouldMatch = "30%",
			FuzzyPrefixLength = 0,
			FuzzyMaxExpansions = 50,
			FuzzyTranspositions = true,
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SimpleQueryString(c => c
				.Name("named_query")
				.Boost(1.1)
				.Fields(f => f.Field(p=>p.Description).Field("myOtherField"))
				.Query("hello world")
				.Analyzer("standard")
				.DefaultOperator(Operator.Or)
				.Flags(SimpleQueryStringFlags.And|SimpleQueryStringFlags.Near)
				.Lenient()
				.AnalyzeWildcard()
				.MinimumShouldMatch("30%")
				.FuzzyPrefixLength(0)
				.FuzzyMaxExpansions(50)
				.FuzzyTranspositions()
				.AutoGenerateSynonymsPhraseQuery(false)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISimpleQueryStringQuery>(a => a.SimpleQueryString)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
		};
	}
}
