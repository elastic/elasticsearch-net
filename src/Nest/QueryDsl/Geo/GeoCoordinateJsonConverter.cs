using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class GeoCoordinateJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as GeoCoordinate;
			if (p == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteStartArray();
			serializer.Serialize(writer, p.Longitude);
			serializer.Serialize(writer, p.Latitude);
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;
			var doubles = serializer.Deserialize<double[]>(reader);
			if (doubles.Length != 2) return null;
			return new GeoCoordinate(doubles[1], doubles[0]);
		}

	}
}