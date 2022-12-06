// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Tests.Domain;
using Tests.Core.Extensions;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Cluster;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Tests.Serialization;

#nullable enable
public class ReadOnlyIndexNameDictionaryTests : SerializerTestBase
{
	[U]
	public void DeserializesCorrectly_AndCanLookupByInferredName()
	{
		var json = @"{""indices"":{""devs"":{""status"":""yellow""},""project"":{""status"":""green""}}}";

		var stream = WrapInStream(json);

		var response = _requestResponseSerializer.Deserialize<SimplifiedHealthResponse>(stream);

		response.Indices.Should()
			.NotBeEmpty()
			.And.ContainKey(Inferrer.IndexName<Developer>());

		response.Indices.Should().NotBeNull();
		response.Indices.Should().NotBeEmpty()
			.And.ContainKey(Inferrer.IndexName<Developer>());
	}

	[U]
	public void DeserializesCorrectly_AndHandleEmptyDictionaries()
	{
		var json = @"{""indices"":{}}";

		var stream = WrapInStream(json);

		var response = _requestResponseSerializer.Deserialize<SimplifiedHealthResponse>(stream);

		response.Indices.Should().NotBeNull();
		response.Indices.Should().BeEmpty();
	}

	private class SimplifiedHealthResponse
	{
		[JsonInclude]
		[JsonPropertyName("indices")]
		[ReadOnlyIndexNameDictionaryConverter(typeof(IndexHealthStats))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats>? Indices { get; init; }
	}
}
#nullable disable
