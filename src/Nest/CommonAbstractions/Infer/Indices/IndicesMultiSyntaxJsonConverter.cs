using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class IndicesMultiSyntaxJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Indices) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as Indices;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			marker.Match(
				all=> writer.WriteValue("_all"),
				many => writer.WriteValue(((IUrlParameter)marker).GetString(serializer.GetConnectionSettings()))
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string indices = reader.Value.ToString();
				return (Indices)indices;
			}
			return null;
		}

	}
}
