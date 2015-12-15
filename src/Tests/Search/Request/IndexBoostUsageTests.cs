using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class IndexBoostUsageTests : SearchUsageTestBase
	{
		public IndexBoostUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			indices_boost = new
			{
				index1 = 1.4,
				index2 = 1.3
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.IndicesBoost(b => b
				.Add("index1", 1.4)
				.Add("index2", 1.3)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				IndicesBoost = new Dictionary<IndexName, double>
				{
						{ "index1", 1.4 },
						{ "index2", 1.3 }
				}
			};
	}
}
