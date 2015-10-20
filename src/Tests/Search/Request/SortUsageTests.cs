using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	/**
	 * Allows to add one or more sort on specific fields. Each sort can be reversed as well. 
	 * The sort is defined on a per field level, with special field name for _score to sort by score.
	 */

	public class SortUsageTests : SearchUsageTestBase
	{
		public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				sort = new object[]
				{
						new { startedOn = new { order = "asc" } },
						new { name = new { order = "desc" } }
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Sort(ss => ss
				.OnField(p => p.StartedOn)
				.Order(SortOrder.Ascending)
			)
			.Sort(ss => ss
				.OnField(p => p.Name)
				.Order(SortOrder.Descending)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Sort = new List<ISort>
				{
						new Nest.Sort
						{
							Field = "startedOn",
							Order = SortOrder.Ascending
						},
						new Nest.Sort
						{
							Field = "name",
							Order = SortOrder.Descending
						}
				}
			};
	}
}
