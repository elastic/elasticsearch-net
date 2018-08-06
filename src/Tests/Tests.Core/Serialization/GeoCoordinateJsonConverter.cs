using System;
using Nest;
using Newtonsoft.Json;

namespace Tests.Core.Serialization
{
	/// <summary>
	/// Copied over because writing coordinates out manually in ExpectJson is tedious
	/// </summary>
	internal class TestGeoCoordinateJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(GeoCoordinate);
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
