using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class TypesMultiSyntaxJsonConverter : JsonConverter
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
				many => writer.WriteValue(((IUrlParameter)marker).GetString(serializer.GetConnectionSettings()))
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string types = reader.Value.ToString();
				return (Types)types;
			}
			return null;
		}

	}
}
