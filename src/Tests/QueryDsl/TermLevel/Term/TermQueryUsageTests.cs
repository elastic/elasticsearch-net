using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Term
{
	public class TermQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					value = "project description"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "project description"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("project description")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermQuery>(q => q.Term)
		{
			q=> q.Field = null,
			q=> q.Value = "  ",
			q=> q.Value = null
		};
	}

	/**[float]
	*== Verbatim term query
	 *
	 * By default an empty term is conditionless so will be rewritten. Sometimes sending an empty term to
	 * match nothing makes sense. You can either use the `ConditionlessQuery` construct from NEST to provide a fallback or make the
	 * query verbatim as followed:
	*/
	public class VerbatimTermQueryUsageTests : TermQueryUsageTests
	{
		public VerbatimTermQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => null;
		//when reading back the json the notion of is conditionless is lost
		protected override bool SupportsDeserialization => false;

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					value = ""
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			IsVerbatim = true,
			Field = "description",
			Value = "",
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(c => c
				.Verbatim()
				.Field(p => p.Description)
				.Value(string.Empty)
			);
	}
}
