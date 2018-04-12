using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class IndicesBoostJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dictionary = (IDictionary<IndexName, double>)value;
			if (dictionary == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			writer.WriteStartArray();
			foreach(var entry in dictionary)
			{
				writer.WriteStartObject();
				var indexName = settings.Inferrer.IndexName(entry.Key);
				if (indexName != null)
				{
					writer.WritePropertyName(indexName);
					serializer.Serialize(writer, entry.Value);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					return serializer.Deserialize<Dictionary<IndexName, double>>(reader);
				case JsonToken.StartArray:
					var dictionary = new Dictionary<IndexName, double>();
					while (reader.TokenType != JsonToken.EndArray)
					{
						reader.Read();
						if (reader.TokenType == JsonToken.PropertyName)
						{
							var indexName = (IndexName)(string)reader.Value;
							dictionary.Add(indexName, reader.ReadAsDouble().GetValueOrDefault());
						}
					}
					return dictionary;
				default:
					return null;
			}
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
