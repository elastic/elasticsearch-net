// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
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

		[I] public async Task OpenPointInTimeResponse() => await Assert<OpenPointInTimeResponse>(OpenPointInTimeStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
		});

		[I] public async Task SearchPointInTimeResponse() => await Assert<SearchResponse<Project>>(SearchPointInTimeStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.PointInTimeId.Should().NotBeNullOrEmpty();
			r.Total.Should().BeGreaterThan(0);
			r.Hits.Count.Should().BeGreaterThan(0);
			r.HitsMetadata.Total.Value.Should().Be(r.Total);
			r.HitsMetadata.Total.Relation.Should().Be(TotalHitsRelation.EqualTo);
			r.Hits.First().Should().NotBeNull();
			r.Hits.First().Source.Should().NotBeNull();
			r.Took.Should().BeGreaterThan(0);
		});

		[I] public async Task ClosePointInTimeResponse() => await Assert<ClosePointInTimeResponse>(ClosePointInTimeStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Succeeded.Should().BeTrue();
			r.NumberFreed.Should().BeGreaterOrEqualTo(1);
		});
	}
}
