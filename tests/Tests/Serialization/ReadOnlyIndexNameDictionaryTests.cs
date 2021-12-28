using System.Text.Json.Serialization;
using Tests.Domain;
using Tests.Core.Extensions;

namespace Tests.Serialization;

public class ReadOnlyIndexNameDictionaryTests : SerializerTestBase
{
	[U]
	public void Test()
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
		public ReadOnlyIndexNameDictionary<Elastic.Clients.Elasticsearch.Cluster.Health.IndexHealthStats> Indices { get; init; }
	}
}
