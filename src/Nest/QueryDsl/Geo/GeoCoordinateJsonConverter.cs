using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class GeoCoordinateJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = (GeoCoordinate)value;
			if (p == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteStartArray();
			serializer.Serialize(writer, p.Longitude);
			serializer.Serialize(writer, p.Latitude);
			if (p.Z.HasValue)
				serializer.Serialize(writer, p.Z.Value);
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;
			var doubles = serializer.Deserialize<double[]>(reader);
			switch (doubles.Length)
			{
				case 2:
					return new GeoCoordinate(doubles[1], doubles[0]);
				case 3:
					return new GeoCoordinate(doubles[1], doubles[0], doubles[2]);
				default:
					return null;
			}
		}
	}
}
