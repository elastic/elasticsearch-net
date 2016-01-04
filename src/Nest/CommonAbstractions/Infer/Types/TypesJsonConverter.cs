using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class TypesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Types) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as Types;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			marker.Match(
				all=> writer.WriteNull(),
				many =>
				{
					var settings = serializer.GetConnectionSettings();
					writer.WriteStartArray();
					foreach(var m in many.Types.Cast<IUrlParameter>())
						writer.WriteValue(m.GetString(settings));
					writer.WriteEndArray();
				}
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{

			if (reader.TokenType != JsonToken.StartArray) return null;
			var types = new List<TypeName> { };
			while (reader.TokenType != JsonToken.EndArray)
			{
				var type = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					types.Add(type);
			}
			return new Types(types);
		}

	}
}
