using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Fuzzy
{
	public class FuzzyNumericQueryUsageTests : QueryDslUsageTestsBase
	{
		public FuzzyNumericQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			fuzzy = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					fuzziness = 2.0,
					max_expansions = 100,
					prefix_length = 3,
					rewrite = "constant_score",
					transpositions = true,
					value = 12.0
				}
			}

		};

		protected override QueryContainer QueryInitializer => new FuzzyNumericQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Fuzziness = 2,
			Value = 12,
			MaxExpansions = 100,
			PrefixLength = 3,
			Rewrite = RewriteMultiTerm.ConstantScore,
			Transpositions = true
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.FuzzyNumeric(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Fuzziness(2)
				.Value(12)
				.MaxExpansions(100)
				.PrefixLength(3)
				.Rewrite(RewriteMultiTerm.ConstantScore)
				.Transpositions()
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFuzzyQuery<double?, double?>>(
			a => a.Fuzzy as IFuzzyQuery<double?, double?>
		)
		{
			q => q.Field = null,
			q => q.Value = null
		};
	}
}