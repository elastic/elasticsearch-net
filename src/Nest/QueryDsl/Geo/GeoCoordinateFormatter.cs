using Elasticsearch.Net;

namespace Nest
{
	internal class GeoCoordinateFormatter : IJsonFormatter<GeoCoordinate>
	{
		public GeoCoordinate Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
				return null;

			var doubles = formatterResolver.GetFormatter<double[]>()
				.Deserialize(ref reader, formatterResolver);
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

		public void Serialize(ref JsonWriter writer, GeoCoordinate value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginArray();
			writer.WriteDouble(value.Longitude);
			writer.WriteValueSeparator();
			writer.WriteDouble(value.Latitude);
			if (value.Z.HasValue)
			{
				writer.WriteValueSeparator();
				writer.WriteDouble(value.Z.Value);
			}
			writer.WriteEndArray();
		}
	}
}
