using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldMinMaxValueJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.StartArray)
			{
				var depth = reader.Depth;
				do
					reader.Read();
				while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
				return null;
			}
			return reader.Value.ToString();
		}
	}
}
