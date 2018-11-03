using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.QueryDsl.TypeLevel.Type
{
	public class TypeQueryUsageTests : QueryDslUsageTestsBase
	{
		public TypeQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITypeQuery>(a => a.Type)
		{
			q => q.Value = null,
		};

		protected override QueryContainer QueryInitializer => new TypeQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Value = Type<Developer>()
		};

		protected override object QueryJson => new
		{
			type = new
			{
				_name = "named_query",
				boost = 1.1,
				value = "developer"
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Type(c => c
				.Name("named_query")
				.Boost(1.1)
				.Value<Developer>()
			);
	}
}
