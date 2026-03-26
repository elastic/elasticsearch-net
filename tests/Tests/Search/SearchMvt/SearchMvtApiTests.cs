// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Search;

public class SearchMvtApiTests : ClusterTestClassBase<ReadOnlyCluster>
{
	private static readonly Field LocationField = Infer.Field<Project>(p => p.LocationPoint);

	public SearchMvtApiTests(ReadOnlyCluster cluster) : base(cluster) { }

	[I]
	public void SearchMvt_ReturnsBinaryTileResponse()
	{
		var response = Client.SearchMvt<Project>(Indices.Index<Project>(), LocationField, 0, 0, 0, d => d
			.Query(q => q.MatchAll())
			.GridPrecision(0)
			.Size(100));

		AssertResponse(response);
	}

 
	[I]
	public async Task SearchMvtAsync_ReturnsBinaryTileResponse()
	{
		var response = await Client.SearchMvtAsync<Project>(Indices.Index<Project>(), LocationField, 0, 0, 0, d => d
			.Query(q => q.MatchAll())
			.GridPrecision(0)
			.Size(100));

		AssertResponse(response);
	}

	private static void AssertResponse(SearchMvtResponse response)
	{
		response.ShouldBeValid();
		response.ApiCallDetails.HttpStatusCode.Should().Be(200);
		response.ApiCallDetails.HasExpectedContentType.Should().BeTrue();
		response.Data.Should().NotBeNullOrEmpty();
	}
}