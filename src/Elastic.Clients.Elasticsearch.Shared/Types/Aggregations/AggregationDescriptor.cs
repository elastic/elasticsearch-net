// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;
#else
namespace Elastic.Clients.Elasticsearch.Aggregations;
#endif

public partial class AggregationDescriptor<TDocument>
{
	internal AggregationDictionary Aggregations { get; set; }

	private AggregationDescriptor<TDocument> SetContainer(string key, Aggregation container)
	{
		if (Self.Aggregations == null)
			Self.Aggregations = new AggregationDictionary();

		Self.Aggregations[key] = container;

		return this;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
}

public partial class AggregationDescriptor
{
	internal AggregationDictionary Aggregations { get; set; }

	private AggregationDescriptor SetContainer(string key, Aggregation container)
	{
		if (Self.Aggregations == null)
			Self.Aggregations = new AggregationDictionary();

		Self.Aggregations[key] = container;

		return this;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
}
