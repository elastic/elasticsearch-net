// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Exists
{
	public class ExistsQueryUsageTests : QueryDslUsageTestsBase
	{
		public ExistsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IExistsQuery>(a => a.Exists)
		{
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new ExistsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
		};

		protected override object QueryJson => new
		{
			exists = new
			{
				_name = "named_query",
				boost = 1.1,
				field = "description"
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Exists(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
			);
	}
}
