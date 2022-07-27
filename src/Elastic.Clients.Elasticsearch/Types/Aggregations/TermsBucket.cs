// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed class TermsBucket<TKey>
{
	public TKey Key { get; init; }
	public string? KeyAsString { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_count_error")]
	public long? DocCountError { get; init; }

	[JsonInclude]
	[JsonPropertyName("aggregations")]
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary Aggregations { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_count")]
	public long DocCount { get; init; }
}
