using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.Match
{
	public class MatchPhrasePrefixUsageTests : QueryDslUsageTestsBase
	{
		public MatchPhrasePrefixUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			match = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello worl",
					analyzer = "standard",
					fuzzy_rewrite = "constant_score_boolean",
					fuzziness = "AUTO",
					fuzzy_transpositions = true,
					cutoff_frequency = 0.001,
					prefix_length = 2,
					max_expansions = 2,
					slop = 2,
					lenient = true,
					minimum_should_match = 2,
					@operator = "or",
					type = "phrase_prefix"
				}
			}

		};

		protected override QueryContainer QueryInitializer => new MatchPhrasePrefixQuery
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			CutoffFrequency = 0.001,
			Query = "hello worl",
			Fuzziness = Fuzziness.Auto,
			FuzzyTranspositions = true,
			MinimumShouldMatch = 2,
			FuzzyRewrite = RewriteMultiTerm.ConstantScoreBoolean,
			MaxExpansions = 2,
			Slop = 2,
			Lenient = true,
			Operator = Operator.Or,
			PrefixLength = 2
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MatchPhrasePrefix(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
				.Query("hello worl")
				.Fuzziness(Fuzziness.Auto)
				.Lenient()
				.FuzzyTranspositions()
				.MaxExpansions(2)
				.MinimumShouldMatch(2)
				.PrefixLength(2)
				.Operator(Operator.Or)
				.FuzzyRewrite(RewriteMultiTerm.ConstantScoreBoolean)
				.Slop(2)
				.Name("named_query")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMatchQuery>(a => a.Match)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};
	}
}
