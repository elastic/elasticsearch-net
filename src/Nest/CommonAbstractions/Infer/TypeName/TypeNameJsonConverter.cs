using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TypeNameJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(TypeName) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as TypeName;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();

			var typeName = settings.Inferrer.TypeName(marker);
			writer.WriteValue(typeName);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string typeName = reader.Value.ToString();
				return (TypeName) typeName;
			}
			return null;
		}

	}
}
