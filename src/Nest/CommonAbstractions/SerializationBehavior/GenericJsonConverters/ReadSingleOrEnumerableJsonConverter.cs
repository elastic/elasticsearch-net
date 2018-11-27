using System;
using System.Runtime.Serialization;

namespace Nest
{
	internal class ReadSingleOrEnumerableJsonConverter<T> : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			reader.TokenType == JsonToken.StartArray
				? serializer.Deserialize<T[]>(reader)
				: new[] { serializer.Deserialize<T>(reader) };

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
