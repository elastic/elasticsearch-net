using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TypeNameJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(TypeName) == objectType;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var typeName = reader.Value.ToString();
				return (TypeName)typeName;
			}

			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var marker = (TypeName)value;
			var settings = serializer.GetConnectionSettings();
			var typeName = settings.Inferrer.TypeName(marker);
			writer.WriteValue(typeName);
		}
	}
}
