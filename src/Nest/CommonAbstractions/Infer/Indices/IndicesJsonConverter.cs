using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class IndicesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Indices) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!(value is Indices marker))
			{
				writer.WriteNull();
				return;
			}
			marker.Match(
				all =>
				{
					writer.WriteStartArray();
					writer.WriteValue("_all");
					writer.WriteEndArray();
				},
				many =>
				{
					var settings = serializer.GetConnectionSettings();
					writer.WriteStartArray();
					foreach (var m in many.Indices.Cast<IUrlParameter>())
						writer.WriteValue(m.GetString(settings));
					writer.WriteEndArray();
				}
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;
			var indices = new List<IndexName>();
			while (reader.TokenType != JsonToken.EndArray)
			{
				var index = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					indices.Add(index);
			}
			return new Indices(indices);
		}

	}
}
