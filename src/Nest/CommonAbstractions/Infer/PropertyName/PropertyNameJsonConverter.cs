using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class PropertyNameJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => objectType == typeof(PropertyName);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var property = value as PropertyName;
			if (property == null)
			{
				writer.WriteNull();
				return;
			}
			var infer = serializer.GetConnectionSettings().Inferrer;
			writer.WriteValue(infer.PropertyName(property));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String) return null;
			var property = reader.Value.ToString();
			return (PropertyName)property;
		}
	}
}

