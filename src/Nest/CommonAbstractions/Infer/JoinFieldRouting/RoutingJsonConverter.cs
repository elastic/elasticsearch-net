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

			var routing = (Routing)value;
			if (routing.Document != null)
			{
				var settings = serializer.GetConnectionSettings();
				var documentId = settings.Inferrer.Routing(routing.Document.GetType(), routing.Document);
				writer.WriteValue(documentId);
			}
			else if (routing.DocumentGetter != null)
			{
				var settings = serializer.GetConnectionSettings();
				var doc = routing.DocumentGetter();
				var documentId = settings.Inferrer.Routing(doc.GetType(), doc);
				writer.WriteValue(documentId);
			}
			else if (routing.LongValue != null) writer.WriteValue(routing.LongValue);
			else writer.WriteValue(routing.StringValue);
		}
	}
}
