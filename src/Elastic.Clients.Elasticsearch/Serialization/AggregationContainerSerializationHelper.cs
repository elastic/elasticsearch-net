using System.Collections.Generic;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch
{
	internal static class AggregationContainerSerializationHelper
	{
		public static AggregationContainer ReadContainer<T>(ref Utf8JsonReader reader, JsonSerializerOptions options) where T : AggregationBase
		{
			var variant = JsonSerializer.Deserialize<T?>(ref reader, options);

			var container = new AggregationContainer(variant);

			return container;
		}

		public static AggregationContainer ReadContainer<T>(string variantName, ref Utf8JsonReader reader, JsonSerializerOptions options) where T : AggregationBase
		{
			var variant = JsonSerializer.Deserialize<T?>(ref reader, options);

			variant.Name = variantName;

			var container = new AggregationContainer(variant);

			reader.Read();

			return container;
		}
	}
}
