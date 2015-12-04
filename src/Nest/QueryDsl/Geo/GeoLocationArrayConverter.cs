using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Text.RegularExpressions;

namespace Nest
{
	internal class GeoLocationArrayConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as GeoLocation;
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
			return new GeoLocation(doubles[1], doubles[0]);
		}

	}
}