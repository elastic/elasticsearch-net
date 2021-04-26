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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.PointInTime
{
	[SkipVersion("<7.10.0", "APIs introduced in 7.10.0")]
	public class PointInTimeApiTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
	{
		private const string OpenPointInTimeStep = nameof(OpenPointInTimeStep);
		private const string SearchPointInTimeStep = nameof(SearchPointInTimeStep);
		private const string SearchPointInTimeWithSortStep = nameof(SearchPointInTimeWithSortStep);
		private const string ClosePointInTimeStep = nameof(ClosePointInTimeStep);
		
		public PointInTimeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				OpenPointInTimeStep, u =>
					u.Calls<OpenPointInTimeDescriptor, OpenPointInTimeRequest, IOpenPointInTimeRequest, OpenPointInTimeResponse>(
						v => new OpenPointInTimeRequest(Nest.Indices.Index<Project>())
						{
							KeepAlive = "1m"
						},
						(v, d) => d.KeepAlive("1m"),
						(v, c, f) => c.OpenPointInTime(Nest.Indices.Index<Project>(), f),
						(v, c, f) => c.OpenPointInTimeAsync(Nest.Indices.Index<Project>(), f),
						(v, c, r) => c.OpenPointInTime(r),
						(v, c, r) => c.OpenPointInTimeAsync(r),
						(r, values) => values.ExtendedValue("pitId", r.Id)
					)
			},
			{
				SearchPointInTimeStep, u =>
					u.Calls<SearchDescriptor<Project>, SearchRequest<Project>, ISearchRequest<Project>, ISearchResponse<Project>>(
						v => new SearchRequest<Project>
						{
							Size = 1,
							Query = new QueryContainer(new MatchAllQuery()),
							PointInTime = new Nest.PointInTime(v, "1m")
						},
						(v, d) => d
							.Size(1)
							.Query(q => q.MatchAll())
							.PointInTime(v, p => p.KeepAlive("1m")),
						(v, c, f) => c.Search(f),
						(v, c, f) => c.SearchAsync(f),
						(v, c, r) => c.Search<Project>(r),
						(v, c, r) => c.SearchAsync<Project>(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
					)
			},
			{
				SearchPointInTimeWithSortStep, ">=7.12.0", u =>
					u.Calls<SearchDescriptor<Project>, SearchRequest<Project>, ISearchRequest<Project>, ISearchResponse<Project>>(
						v => new SearchRequest<Project>
						{
							Size = 1,
							Query = new QueryContainer(new MatchAllQuery()),
							PointInTime = new Nest.PointInTime(v, "1m"),
							Sort =new List<ISort>
							{
								FieldSort.ShardDocumentOrderDescending
							}
						},
						(v, d) => d
							.Size(1)
							.Query(q => q.MatchAll())
							.PointInTime(v, p => p.KeepAlive("1m"))
							.Sort(s => s.Descending(SortSpecialField.ShardDocumentOrder)),
						(v, c, f) => c.Search(f),
						(v, c, f) => c.SearchAsync(f),
						(v, c, r) => c.Search<Project>(r),
						(v, c, r) => c.SearchAsync<Project>(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
					)
			},
			{
				ClosePointInTimeStep, u =>
					u.Calls<ClosePointInTimeDescriptor, ClosePointInTimeRequest, IClosePointInTimeRequest, ClosePointInTimeResponse>(
						v => new ClosePointInTimeRequest{ Id = v },
						(v, d) => d.Id(v),
						(v, c, f) => c.ClosePointInTime(f),
						(v, c, f) => c.ClosePointInTimeAsync(f),
						(v, c, r) => c.ClosePointInTime(r),
						(v, c, r) => c.ClosePointInTimeAsync(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
					)
			}
		}) { }

		[I] public async Task OpenPointInTimeResponse() => await Assert<OpenPointInTimeResponse>(OpenPointInTimeStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
		});

		[I] public async Task SearchPointInTimeResponse() => await Assert<SearchResponse<Project>>(SearchPointInTimeStep, r =>
		{
			r.ShouldBeValid();
			r.PointInTimeId.Should().NotBeNullOrEmpty();
			r.Total.Should().BeGreaterThan(0);
			r.Hits.Count.Should().BeGreaterThan(0);
			r.HitsMetadata.Total.Value.Should().Be(r.Total);
			r.HitsMetadata.Total.Relation.Should().Be(TotalHitsRelation.EqualTo);
			r.Hits.First().Should().NotBeNull();
			r.Hits.First().Source.Should().NotBeNull();
			r.Took.Should().BeGreaterOrEqualTo(0);
		});

		[I] public async Task SearchPointInTimeWithSortResponse() => await Assert<SearchResponse<Project>>(SearchPointInTimeWithSortStep, r =>
		{
			r.ShouldBeValid();
			r.PointInTimeId.Should().NotBeNullOrEmpty();
			r.Total.Should().BeGreaterThan(0);
			r.Hits.Count.Should().BeGreaterThan(0);
		});

		[I] public async Task ClosePointInTimeResponse() => await Assert<ClosePointInTimeResponse>(ClosePointInTimeStep, r =>
		{
			r.ShouldBeValid();
			r.Succeeded.Should().BeTrue();
			r.NumberFreed.Should().BeGreaterOrEqualTo(1);
		});
	}
}
