// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Search;

public class SearchMvtResponseBuilderTests
{
	private static readonly byte[] TileBytes = [0x1A, 0x03, 0x66, 0x6F, 0x6F];
	private const string ContentType = "application/vnd.mapbox-vector-tile";

	[U]
	public void SearchMvt_ReturnsBinaryTileResponse()
	{
		var client = FixedResponseClient.Create(TileBytes, contentType: ContentType);

		var response = client.SearchMvt("test-index", "location", 0, 0, 0);

		response.IsValidResponse.Should().BeTrue();
		response.ApiCallDetails.HttpStatusCode.Should().Be(200);
		response.ApiCallDetails.HasExpectedContentType.Should().BeTrue();
		response.Data.Should().Equal(TileBytes);
	}

	[U]
	public async Task SearchMvtAsync_ReturnsBinaryTileResponse()
	{
		var client = FixedResponseClient.Create(TileBytes, contentType: ContentType);

		var response = await client.SearchMvtAsync("test-index", "location", 0, 0, 0);

		response.IsValidResponse.Should().BeTrue();
		response.ApiCallDetails.HttpStatusCode.Should().Be(200);
		response.ApiCallDetails.HasExpectedContentType.Should().BeTrue();
		response.Data.Should().Equal(TileBytes);
	}
}
