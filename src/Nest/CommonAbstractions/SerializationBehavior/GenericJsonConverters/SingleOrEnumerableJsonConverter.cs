using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class SingleOrEnumerableJsonConverter<T> : JsonConverter
	{
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			return reader.TokenType == JsonToken.StartArray
				? serializer.Deserialize<T[]>(reader)
				: new[] { serializer.Deserialize<T>(reader) };
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				return;

			if (!(value is ICollection<T> list))
				throw new JsonSerializationException($"Invalid type for {GetType()}: {value.GetType()}");

			if (list.Count == 0)
				return;

			if (list.All(i => i == null))
				return;

			writer.WriteStartArray();
			foreach (var item in list)
				serializer.Serialize(writer, item);
			writer.WriteEndArray();
		}
	}
}
