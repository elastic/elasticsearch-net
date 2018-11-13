using Utf8Json;

namespace Nest
{
	internal class GeoLocationFormatter : IJsonFormatter<GeoLocation>
	{
		public GeoLocation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var count = 0;
			double lat = 0;
			double lon = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "lat":
						lat = reader.ReadDouble();
						break;
					case "lon":
						lon = reader.ReadDouble();
						break;
				}
			}

			return new GeoCoordinate(lat, lon);
		}

		public void Serialize(ref JsonWriter writer, GeoLocation value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName("lat");
			writer.WriteDouble(value.Latitude);
			writer.WriteValueSeparator();
			writer.WritePropertyName("lon");
			writer.WriteDouble(value.Longitude);
			writer.WriteEndObject();
		}
	}
}
