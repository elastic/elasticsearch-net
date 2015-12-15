using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldsJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var fields = value as Fields;
			writer.WriteStartArray();
			var infer = serializer.GetConnectionSettings().Inferrer;
			foreach (var f in fields?.ListOfFields ?? Enumerable.Empty<Field>())
			{
				writer.WriteValue(infer.Field(f));
			}
			writer.WriteEndArray();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;
			var fields = new Fields();
			while (reader.TokenType != JsonToken.EndArray)
			{
				var field = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					fields.And(field);
			}
			return fields;
		}
	}
}

