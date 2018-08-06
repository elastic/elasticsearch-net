using System;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using static Nest.Infer;
using System.Collections.Generic;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Search.Request
{
	public class SearchAfterUsageTests : SearchUsageTestBase
	{
		public SearchAfterUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
					new { numberOfCommits = new { order = "desc" } },
					new { name = new { order = "desc" } }
				},
				search_after = new object []
				{
					Project.First.NumberOfCommits,
					Project.First.Name
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(srt => srt
				.Descending(p => p.NumberOfCommits)
				.Descending(p => p.Name)
			)
			.SearchAfter(
				Project.First.NumberOfCommits,
				Project.First.Name
			);


		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
					new SortField { Field = Field<Project>(p => p.NumberOfCommits), Order = SortOrder.Descending },
					new SortField { Field = Field<Project>(p => p.Name), Order = SortOrder.Descending }
				},
				SearchAfter = new List<object>
				{
					Project.First.NumberOfCommits,
					Project.First.Name,
				}
			};
	}
}
