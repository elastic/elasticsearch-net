using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class IdJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Id) == objectType;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Integer)
				return new Id((long)reader.Value);

			return new Id(reader.Value as string);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var id = (Id)value;
			if (id.Document != null)
			{
				var settings = serializer.GetConnectionSettings();
				var documentId = settings.Inferrer.Id(id.Document.GetType(), id.Document);
				writer.WriteValue(documentId);
			}
			else if (id.LongValue != null) writer.WriteValue(id.LongValue);
			else writer.WriteValue(id.StringValue);
		}
	}
}
