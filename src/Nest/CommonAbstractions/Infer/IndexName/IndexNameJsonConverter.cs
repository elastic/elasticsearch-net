using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class IndexNameJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(IndexName) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as IndexName;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var indexName = settings.Inferrer.IndexName(marker);
			writer.WriteValue(indexName);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string typeName = reader.Value.ToString();
				return (IndexName)typeName;
			}
			return null;
		}

	}
}
