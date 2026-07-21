// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Core.Search;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Search.Search;

[UsesVerify]
public class SearchRequestSerialization : SerializerTestBase
{
	[U]
	public async Task SearchRequestWithSourceProperty_SerializesCorrectly()
	{
		var descriptor = new SearchRequestDescriptor("test")
			.Source(new SourceConfig(false));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		var searchRequest = new SearchRequest("test")
		{
			Source = new SourceConfig(false),
		};

		var objectJson = await SerializeAndGetJsonStringAsync(searchRequest);
		objectJson.Should().Be(json);
	}
}
