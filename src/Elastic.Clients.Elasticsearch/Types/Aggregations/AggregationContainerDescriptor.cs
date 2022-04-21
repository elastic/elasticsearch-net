// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class AggregationContainerDescriptor<TDocument>
{
	internal AggregationDictionary Aggregations { get; set; }

	private AggregationContainerDescriptor<TDocument> SetContainer(string key, AggregationContainer container)
	{
		if (Self.Aggregations == null)
			Self.Aggregations = new AggregationDictionary();

		Self.Aggregations[key] = container;

		return this;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
}

public partial class AggregationContainerDescriptor
{
	internal AggregationDictionary Aggregations { get; set; }

	private AggregationContainerDescriptor SetContainer(string key, AggregationContainer container)
	{
		if (Self.Aggregations == null)
			Self.Aggregations = new AggregationDictionary();

		Self.Aggregations[key] = container;

		return this;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
}
