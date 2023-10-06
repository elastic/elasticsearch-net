// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;
#else
namespace Elastic.Clients.Elasticsearch.Aggregations;
#endif

public sealed class TermsBucket<TKey> : AggregateDictionary
{
	public TermsBucket(IReadOnlyDictionary<string, IAggregate> backingDictionary) : base(backingDictionary)
	{
	}

	public TKey Key { get; init; }

	public string? KeyAsString { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_count_error")]
	public long? DocCountError { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_count")]
	public long DocCount { get; init; }
}
