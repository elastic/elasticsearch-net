using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.CommonTerms
{
	public class CommonTermsUsageTests : QueryDslUsageTestsBase
	{
		public CommonTermsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			common = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "nelly the elephant not as a",
					cutoff_frequency = 0.001,
					low_freq_operator = "or",
					high_freq_operator = "and",
					minimum_should_match = 1,
					analyzer = "standard",
					disable_coord = true
				}
			}
		};

		protected override QueryContainer QueryInitializer => new CommonTermsQuery()
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			CutoffFrequency = 0.001,
			DisableCoord = true,
			HighFrequencyOperator = Operator.And,
			LowFrequencyOperator = Operator.Or,
			MinimumShouldMatch = 1,
			Name = "named_query",
			Query = "nelly the elephant not as a"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.CommonTerms(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
				.DisableCoord()
				.HighFrequencyOperator(Operator.And)
				.LowFrequencyOperator(Operator.Or)
				.MinimumShouldMatch(1)
				.Name("named_query")
				.Query("nelly the elephant not as a")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ICommonTermsQuery>(a => a.CommonTerms)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};
	}
}
