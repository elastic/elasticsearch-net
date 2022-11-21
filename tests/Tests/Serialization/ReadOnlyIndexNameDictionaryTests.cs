// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Tests.Domain;
using Tests.Core.Extensions;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Cluster;
using Elastic.Clients.Elasticsearch.Serialization;
//using Elastic.Clients.Elasticsearch.Cluster.Health;

namespace Tests.Serialization;

#nullable enable
public class ReadOnlyIndexNameDictionaryTests : SerializerTestBase
{
	[U]
	public void DeserializesCorrectly_AndCanLookupByInferredName()
	{
		var json = @"{""indices"":{""devs"":{""status"":""yellow""},""project"":{""status"":""green""}},""indicesTwo"":{""devs"":{""status"":""yellow""},""project"":{""status"":""green""}}}";

		var stream = WrapInStream(json);

		var response = _requestResponseSerializer.Deserialize<SimplifiedHealthResponse>(stream);

		response.Indices.Should()
			.NotBeEmpty()
			.And.ContainKey(Inferrer.IndexName<Developer>());

		response.IndicesTwo.HasValue.Should().BeTrue();
#pragma warning disable CS8629 // Nullable value type may be null.
		response.IndicesTwo.Value.Should()
#pragma warning restore CS8629 // Nullable value type may be null.
			.NotBeEmpty()
			.And.ContainKey(Inferrer.IndexName<Developer>());
	}

	private class SimplifiedHealthResponse
	{
		[JsonInclude]
		[JsonPropertyName("indices")]
		[ReadOnlyIndexNameDictionaryConverter(typeof(IndexHealthStats))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats>? Indices { get; init; }

		[JsonInclude]
		[JsonPropertyName("indicesTwo")]
		public ReadOnlyIndexNameDictionary<IndexHealthStats>? IndicesTwo { get; init; }
	}

	[U]
	public void DeserializesCorrectly_AndHandleEmptyDictionaries()
	{
		var json = @"{""indices"":{""devs"":{""status"":""yellow""},""project"":{""status"":""green""}}, ""indicesTwo"":{}}";

		var stream = WrapInStream(json);

		var response = _requestResponseSerializer.Deserialize<SimplifiedHealthResponse>(stream);

		response.IndicesTwo.HasValue.Should().BeTrue();
#pragma warning disable CS8629 // Nullable value type may be null.
		response.IndicesTwo.Value.Should()
#pragma warning restore CS8629 // Nullable value type may be null.
			.BeEmpty()
			.And.NotContainKey(Inferrer.IndexName<Developer>());
	}
}
#nullable disable
