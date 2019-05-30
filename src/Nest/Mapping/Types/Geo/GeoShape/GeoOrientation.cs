using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoOrientationConverter))]
	public enum GeoOrientation
	{
		ClockWise,
		CounterClockWise
	}

	internal class GeoOrientationConverter : IJsonFormatter<GeoOrientation>
	{
		public void Serialize(ref JsonWriter writer, GeoOrientation value, IJsonFormatterResolver formatterResolver)
		{
			switch (value)
			{
				case GeoOrientation.ClockWise:
					writer.WriteString("cw");
					break;
				case GeoOrientation.CounterClockWise:
					writer.WriteString("ccw");
					break;
			}
		}

		public GeoOrientation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var enumString = reader.ReadString();
			switch (enumString.ToLowerInvariant())
			{
				case "left":
				case "cw":
				case "clockwise":
					return GeoOrientation.ClockWise;
			}
			// Default, complies with the OGC standard
			return GeoOrientation.CounterClockWise;
		}
	}
}
