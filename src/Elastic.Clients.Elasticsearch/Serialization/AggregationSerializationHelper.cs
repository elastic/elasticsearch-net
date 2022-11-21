// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class AggregationSerializationHelper
{
	public static Aggregation ReadContainer<T>(ref Utf8JsonReader reader, JsonSerializerOptions options) where T : SearchAggregation
	{
		var variant = JsonSerializer.Deserialize<T?>(ref reader, options);

		var container = new Aggregation(variant);

		return container;
	}

	public static Aggregation ReadContainer<T>(string variantName, ref Utf8JsonReader reader, JsonSerializerOptions options) where T : SearchAggregation
	{
		var variant = JsonSerializer.Deserialize<T>(ref reader, options);

		var container = new Aggregation(variant);

		if (container.Variant is SearchAggregation agg)
			agg.Name = variantName;

		return container;
	}
}
