using Elasticsearch.Net;


namespace Nest
{
	internal class GeoLocationFormatter : IJsonFormatter<GeoLocation>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "lat", 0 },
			{ "lon", 1 }
		};

		public GeoLocation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var count = 0;
			double lat = 0;
			double lon = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							lat = reader.ReadDouble();
							break;
						case 1:
							lon = reader.ReadDouble();
							break;
					}
				}
				else
					reader.ReadNextBlock();
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
