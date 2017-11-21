using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class StringTimeSpanConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				var timeSpan = (TimeSpan)value;
				writer.WriteValue(timeSpan.ToString());
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					if (!objectType.IsGeneric() || objectType.GetGenericTypeDefinition() != typeof(Nullable<>))
						throw new JsonSerializationException($"Cannot convert null value to {objectType}.");

					return null;
				case JsonToken.String:
					return TimeSpan.Parse((string)reader.Value);
			}

			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}
}
