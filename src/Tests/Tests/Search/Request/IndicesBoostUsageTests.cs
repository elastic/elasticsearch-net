using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class IndicesBoostUsageTests : SearchUsageTestBase
	{
		public IndicesBoostUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			indices_boost = new object[] {
				new { project = 1.4 },
				new { devs = 1.3 }
			},
			query = new
			{
				match_all = new {}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.IndicesBoost(b => b
				.Add("project", 1.4)
				.Add("devs", 1.3)
			)
			.Query(q => q
				.MatchAll()
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				IndicesBoost = new Dictionary<IndexName, double>
				{
					{ "project", 1.4 },
					{ "devs", 1.3 }
				},
				Query = new MatchAllQuery()
			};
	}
}
