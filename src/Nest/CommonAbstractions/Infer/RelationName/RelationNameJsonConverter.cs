using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class RelationNameJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(RelationName) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var marker = (RelationName)value;
			var settings = serializer.GetConnectionSettings();
			var typeName = settings.Inferrer.RelationName(marker);
			writer.WriteValue(typeName);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var typeName = reader.Value.ToString();
				return (RelationName) typeName;
			}
			return null;
		}
	}
}
