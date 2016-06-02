using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class SingleOrEnumerableConverter<T> : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize<T[]>(reader);
			}

			return new[] { serializer.Deserialize<T>(reader) };
		}

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}