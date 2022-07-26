// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public class EmptyTermsAggregate : IAggregate
	{
		[JsonInclude]
		[JsonPropertyName("doc_count_error_upper_bound")]
		public long? DocCountErrorUpperBound { get; init; }

		[JsonInclude]
		[JsonPropertyName("sum_other_doc_count")]
		public long SumOtherDocCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("buckets")]
		public Buckets<TermsBucket<EmptyTermsBucket>> Buckets { get; init; }

		[JsonInclude]
		[JsonPropertyName("meta")]
		public Dictionary<string, object>? Meta { get; init; }
	}
}
