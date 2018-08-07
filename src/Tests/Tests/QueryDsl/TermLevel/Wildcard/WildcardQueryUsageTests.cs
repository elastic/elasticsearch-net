using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.QueryDsl.TermLevel.Wildcard
{
	public class WildcardQueryUsageTests : QueryDslUsageTestsBase
	{
		public WildcardQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			wildcard = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					rewrite = "top_terms_boost_10",
					value = "p*oj"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new WildcardQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "p*oj",
			Rewrite = MultiTermQueryRewrite.TopTermsBoost(10)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Wildcard(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("p*oj")
				.Rewrite(MultiTermQueryRewrite.TopTermsBoost(10))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IWildcardQuery>(a => a.Wildcard)
		{
			q => q.Field = null,
			q => q.Value = null,
			q => q.Value = string.Empty
		};
	}
}
