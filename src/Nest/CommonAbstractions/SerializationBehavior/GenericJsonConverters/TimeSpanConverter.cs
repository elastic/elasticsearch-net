using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	internal class TimeSpanConverter : JsonConverter
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
			if (reader.TokenType == JsonToken.Null)
			{
				if (!objectType.IsGeneric() || objectType.GetGenericTypeDefinition() != typeof (Nullable<>))
					throw new JsonSerializationException($"Cannot convert null value to {objectType}.");

				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				return TimeSpan.Parse((string) reader.Value);
			}
			if (reader.TokenType == JsonToken.Integer)
			{
				return new TimeSpan((long) reader.Value);
			}

			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof (TimeSpan) || objectType == typeof (TimeSpan?);
	}
}