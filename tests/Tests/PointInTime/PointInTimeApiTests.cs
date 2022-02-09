// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.PointInTime;

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
					u.Calls<OpenPointInTimeRequestDescriptor, OpenPointInTimeRequest, OpenPointInTimeResponse>(
						v => new OpenPointInTimeRequest(Indices.Index<Project>())
						{
							KeepAlive = "1m"
						},
						(v, d) => d.KeepAlive("1m"),
						(v, c, f) => c.OpenPointInTime(Indices.Index<Project>(), f),
						(v, c, f) => c.OpenPointInTimeAsync(Indices.Index<Project>(), f),
						(v, c, r) => c.OpenPointInTime(r),
						(v, c, r) => c.OpenPointInTimeAsync(r),
						(r, values) => values.ExtendedValue("pitId", r.Id.ToString()) // Converting to a string here is okay as the Id can be implicitly created from it
					)
			},
			{
				SearchPointInTimeStep, u =>
					u.Calls<SearchRequestDescriptor<Project>, SearchRequest<Project>, SearchResponse<Project>>(
						v => new SearchRequest<Project>
						{
							Size = 1,
							Query = new QueryContainer(new MatchAllQuery()),
							Pit = new PointInTimeReference
							{
								Id = v,
								KeepAlive = "1m"
							}
						},
						(v, d) => d
							.Size(1)
							.Query(q => q.MatchAll())
							.Pit(v, p => p.KeepAlive("1m")),
						(v, c, f) => c.Search(f),
						(v, c, f) => c.SearchAsync(f),
						(v, c, r) => c.Search<Project>(r),
						(v, c, r) => c.SearchAsync<Project>(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
					)
			},
			//{
			//	SearchPointInTimeWithSortStep, ">=7.12.0", u =>
			//		u.Calls<SearchRequestDescriptor<Project>, SearchRequest<Project>, SearchResponse<Project>>(
			//			v => new SearchRequest<Project>
			//			{
			//				Size = 1,
			//				Query = new QueryContainer(new MatchAllQuery()),
			//				PointInTime = new Nest.PointInTime(v, "1m"),
			//				Sort =new List<ISort>
			//				{
			//					FieldSort.ShardDocumentOrderDescending
			//				}
			//			},
			//			(v, d) => d
			//				.Size(1)
			//				.Query(q => q.MatchAll())
			//				.PointInTime(v, p => p.KeepAlive("1m"))
			//				.Sort(s => s.Descending(SortSpecialField.ShardDocumentOrder)),
			//			(v, c, f) => c.Search(f),
			//			(v, c, f) => c.SearchAsync(f),
			//			(v, c, r) => c.Search<Project>(r),
			//			(v, c, r) => c.SearchAsync<Project>(r),
			//			uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
			//		)
			//},
			{
				ClosePointInTimeStep, u =>
					u.Calls<ClosePointInTimeRequestDescriptor, ClosePointInTimeRequest, ClosePointInTimeResponse>(
						v => new ClosePointInTimeRequest{ Id = v },
						(v, d) => d.Id(v),
						(v, c, f) => c.ClosePointInTime(f),
						(v, c, f) => c.ClosePointInTimeAsync(f),
						(v, c, r) => c.ClosePointInTime(r),
						(v, c, r) => c.ClosePointInTimeAsync(r),
						uniqueValueSelector: values => values.ExtendedValue<string>("pitId")
					)
			}
		})
	{ }

	[I]
	public async Task OpenPointInTimeResponse() => await Assert<OpenPointInTimeResponse>(OpenPointInTimeStep, r =>
	{
		r.ShouldBeValid();
		r.Id.ToString().Should().NotBeNullOrEmpty();
	});

	[I]
	public async Task SearchPointInTimeResponse() => await Assert<SearchResponse<Project>>(SearchPointInTimeStep, r =>
	{
		r.ShouldBeValid();
		r.PitId.Should().NotBeNull(); // TODO - Having this typed as Id is a pain for usability - Review this in the spec
		r.Total.Should().BeGreaterThan(0);
		r.Hits.Count.Should().BeGreaterThan(0);
		r.HitsMetadata.Total.Value.Should().Be(r.Total);
		r.HitsMetadata.Total.Relation.Should().Be(TotalHitsRelation.Eq);
		//r.Hits.First().Should().NotBeNull();
		//r.Hits.First().Source.Should().NotBeNull();
		r.Took.Should().BeGreaterOrEqualTo(0);
	});

	//[I] public async Task SearchPointInTimeWithSortResponse() => await Assert<SearchResponse<Project>>(SearchPointInTimeWithSortStep, r =>
	//{
	//	r.ShouldBeValid();
	//	r.PointInTimeId.Should().NotBeNullOrEmpty();
	//	r.Total.Should().BeGreaterThan(0);
	//	r.Hits.Count.Should().BeGreaterThan(0);
	//});

	[I]
	public async Task ClosePointInTimeResponse() => await Assert<ClosePointInTimeResponse>(ClosePointInTimeStep, r =>
	{
		r.ShouldBeValid();
		r.Succeeded.Should().BeTrue();
		r.NumFreed.Should().BeGreaterOrEqualTo(1);
			//r.NumberFreed.Should().BeGreaterOrEqualTo(1);
	});
}
