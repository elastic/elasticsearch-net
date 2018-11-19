using System;
using System.Collections.Generic;
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
			if (fields != null)
			{
				// overridden Equals() method means a Fields with only one Field
				// results in Equality, which triggers Json.NET's Reference loop detection
				var referenceLoopHandling = serializer.ReferenceLoopHandling;
				serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

				foreach (var field in fields.ListOfFields)
					serializer.Serialize(writer, field);

				serializer.ReferenceLoopHandling = referenceLoopHandling;
			}
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			reader.TokenType != JsonToken.StartArray
				? null
				: new Fields(serializer.Deserialize<IEnumerable<Field>>(reader));
	}
}
