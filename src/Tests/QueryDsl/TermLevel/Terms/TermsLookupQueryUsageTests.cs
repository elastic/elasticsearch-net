using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsLookupQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsLookupQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new
				{
					id = 12,
					index = "devs",
					path = "lastName",
					type = "developer"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			TermsLookup = new FieldLookup
			{
				Id = 12,
				Index = Index<Developer>(),
				Type = Type<Developer>(),
				Path = Field<Developer>(p=>p.LastName)
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.TermsLookup<Developer>(e=>e.Path(p=>p.LastName).Id(12))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermsQuery>(a => a.Terms)
		{
			q => q.Field = null,
			q => q.TermsLookup = null,
			q => q.TermsLookup.Id = null,
			q => q.TermsLookup.Type = null,
			q => q.TermsLookup.Index = null,
			q => q.TermsLookup.Path = null,
		};
	}
}