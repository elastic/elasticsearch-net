using System.Text.Json.Serialization;
using Tests.Domain;
using Tests.Core.Extensions;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Cluster.Health;

namespace Tests.Serialization;

public class ReadOnlyIndexNameDictionaryTests : SerializerTestBase
{
	[U]
	public void DeserializesCorrectly_AndCanLookupByInferredName()
	{
		var json = @"{""indices"":{""devs"":{""status"":""yellow""},""project"":{""status"":""green""}}}";

		var stream = WrapInStream(json);

		var response = _requestResponseSerializer.Deserialize<SimplifiedClusterHealthResponse>(stream);

		response.Indices.Should()
			.NotBeEmpty()
			.And.ContainKey(Inferrer.IndexName<Developer>());
	}

	private class SimplifiedClusterHealthResponse
	{
		[JsonInclude]
		[JsonPropertyName("indices")]
		[MyConverter(typeof(IndexHealthStats))]

		//[JsonConverter(typeof(ReadOnlyIndexNameDictionaryConverterFactory))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; init; }
	}
}
