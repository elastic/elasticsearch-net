using System;
using System.Collections.Generic;
using Nest_5_2_0;
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
				project = 1.4,
				devs = 1.3
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.IndicesBoost(b => b
				.Add("project", 1.4)
				.Add("devs", 1.3)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				IndicesBoost = new Dictionary<IndexName, double>
				{
					{ "project", 1.4 },
					{ "devs", 1.3 }
				}
			};
	}
}
