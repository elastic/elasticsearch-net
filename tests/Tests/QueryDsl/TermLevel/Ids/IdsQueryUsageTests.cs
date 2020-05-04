// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Ids
{
	public class IdsQueryUsageTests : QueryDslUsageTestsBase
	{
		public IdsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IIdsQuery>(a => a.Ids)
		{
			q => q.Values = null,
			q => q.Values = Enumerable.Empty<Id>()
		};

		protected override QueryContainer QueryInitializer => new IdsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Values = new List<Id> { 1, 2, 3, 4 },
		};

		protected override object QueryJson => new
		{
			ids = new
			{
				_name = "named_query",
				boost = 1.1,
				values = new[] { 1, 2, 3, 4 }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Ids(c => c
				.Name("named_query")
				.Boost(1.1)
				.Values(1, 2, 3, 4)
			);
	}
}
