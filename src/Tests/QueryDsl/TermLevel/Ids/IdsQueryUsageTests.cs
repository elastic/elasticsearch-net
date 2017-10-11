using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.TermLevel.Ids
{
	public class IdsQueryUsageTests : QueryDslUsageTestsBase
	{
		public IdsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			ids = new
			{
				_name = "named_query",
				boost = 1.1,
				type = new[] { "project", "developer" },
				values = new[] { 1, 2, 3, 4 }
			}

		};

		protected override QueryContainer QueryInitializer => new IdsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Values = new List<Id> { 1, 2,3,4 },
			Types = Type<Project>().And<Developer>()
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Ids(c => c
				.Name("named_query")
				.Boost(1.1)
				.Values(1, 2, 3, 4)
				.Types(typeof(Project), typeof(Developer))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IIdsQuery>(a => a.Ids)
		{
			q => q.Values = null,
			q => q.Values = Enumerable.Empty<Id>()
		};
	}
}
