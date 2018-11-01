using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldsJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;

			var fields = new Fields();
			while (reader.TokenType != JsonToken.EndArray)
			{
				// as per https://github.com/elastic/elasticsearch/pull/29639 this can now be an array of objects
				reader.Read();
				switch (reader.TokenType)
				{
					case JsonToken.String:
						fields.And((string)reader.Value);
						break;
					case JsonToken.StartObject:
						/// TODO 6.4 this is temporary until we add proper support for doc_values format
						reader.Read(); // "field";
						var field = reader.ReadAsString();
						fields.And(field);
						while (reader.TokenType != JsonToken.EndObject) reader.Read();
						reader.Read(); // "}";
						break;
				}
			}

			return fields;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var fields = value as Fields;
			writer.WriteStartArray();
			if (fields != null)
			{
				var infer = serializer.GetConnectionSettings().Inferrer;
				foreach (var f in fields.ListOfFields) writer.WriteValue(infer.Field(f));
			}

			writer.WriteEndArray();
		}
	}
}
