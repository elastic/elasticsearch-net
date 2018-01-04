using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class RoutingJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Routing) == objectType;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Integer)
				return new Routing((long)reader.Value);

			return new Routing(reader.Value as string);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var id = (Routing)value;
			if (id.Document != null)
			{
				var settings = serializer.GetConnectionSettings();
				var documentId = settings.Inferrer.Routing(id.Document.GetType(), id.Document);
				writer.WriteValue(documentId);
			}
			else writer.WriteValue(id.Value);
		}
	}
}
