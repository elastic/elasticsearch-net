/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Search.Request
{
	public class SearchAfterParamsUsageTests : SearchUsageTestBase
	{
		public SearchAfterParamsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			new SearchRequest<Project>()
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

	public class SearchAfterUsageTests : SearchUsageTestBase
	{
		public SearchAfterUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private readonly IReadOnlyCollection<object> _previousSort = new List<object>
		{
			Project.First.NumberOfCommits,
			Project.First.Name
		};

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
			.SearchAfter(_previousSort);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				Sort = new List<ISort>
				{
					new FieldSort { Field = Field<Project>(p => p.NumberOfCommits), Order = SortOrder.Descending },
					new FieldSort { Field = Field<Project>(p => p.Name), Order = SortOrder.Descending }
				},
				SearchAfter = _previousSort.ToList()
			};
	}
}
