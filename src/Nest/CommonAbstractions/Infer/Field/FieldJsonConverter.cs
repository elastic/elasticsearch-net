using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var field = value as Field;
			if (field == null)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			writer.WriteValue(settings.Inferrer.Field(field));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String) return null;
			var field = reader.Value.ToString();
			return (Field)field;
		}
	}
}

