using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.MultiMatch
{
	public class MultiMatchUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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
				fields = new[] {
					"description",
					"myOtherField"
				},
				zero_terms_query = "all"
			}
		};

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Fields = Field<Project>(p=>p.Description).And("myOtherField"),
			Query = "hello world",
			Analyzer = "standard",
			Boost = 1.1,
			Slop = 2,
			Fuzziness = Fuzziness.Auto,
			PrefixLength = 2,
			MaxExpansions = 2,
			Operator = Operator.Or,
			MinimumShouldMatch = 2,
			FuzzyRewrite = RewriteMultiTerm.ConstantScoreBoolean,
			TieBreaker = 1.1,
			CutoffFrequency = 0.001,
			Lenient = true,
			ZeroTermsQuery = ZeroTermsQuery.All,
			Name = "named_query",
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				.Fields(f => f.Field(p=>p.Description).Field("myOtherField"))
				.Query("hello world")
				.Analyzer("standard")
				.Boost(1.1)
				.Slop(2)
				.Fuzziness(Fuzziness.Auto)
				.PrefixLength(2)
				.MaxExpansions(2)
				.Operator(Operator.Or)
				.MinimumShouldMatch(2)
				.FuzzyRewrite(RewriteMultiTerm.ConstantScoreBoolean)
				.TieBreaker(1.1)
				.CutoffFrequency(0.001)
				.Lenient()
				.ZeroTermsQuery(ZeroTermsQuery.All)
				.Name("named_query")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMultiMatchQuery>(a => a.MultiMatch)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Fields = null
		};
	}

	public class MultiMatchWithBoostUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchWithBoostUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			multi_match = new
			{
				query = "hello world",
				fields = new[] {
					"description^2.2",
					"myOtherField^0.3"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Fields = Field<Project>(p=>p.Description, 2.2).And("myOtherField^0.3"),
			Query = "hello world",
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				//.Fields(f => f.Field(p=>p.Description, 2.2).Field("myOtherField^0.3"))
				.Fields(Field<Project>(p=>p.Description, 2.2).And("myOtherField^0.3"))
				.Query("hello world")
			);
	}
}
