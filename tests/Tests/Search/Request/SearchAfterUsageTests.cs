// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

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
				search_after = new object[]
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
					new FieldSort { Field = Field<Project>(p => p.NumberOfCommits), Order = SortOrder.Descending },
					new FieldSort { Field = Field<Project>(p => p.Name), Order = SortOrder.Descending }
				},
				SearchAfter = new List<object>
				{
					Project.First.NumberOfCommits,
					Project.First.Name,
				}
			};
	}
}
