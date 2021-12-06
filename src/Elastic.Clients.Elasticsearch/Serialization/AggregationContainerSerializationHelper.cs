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
			
			reader.Read();

			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("meta"))
				{
					var meta = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);

					//if (meta is not null)
					//{
					//	container.Meta = meta;
					//}

					reader.Read();
				}
			}

			return container;
		}
	}
}
