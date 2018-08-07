using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TimeSpanToStringConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				var timeSpan = (TimeSpan) value;
				writer.WriteValue(timeSpan.Ticks);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null: return null;
				case JsonToken.String: return TimeSpan.Parse((string) reader.Value);
				case JsonToken.Integer: return new TimeSpan((long) reader.Value);
			}
			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof (TimeSpan) || objectType == typeof (TimeSpan?);
	}
}
