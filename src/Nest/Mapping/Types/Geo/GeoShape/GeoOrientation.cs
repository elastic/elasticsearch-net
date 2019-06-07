using Elasticsearch.Net;

namespace Nest
{
	public enum GeoOrientation
	{
		ClockWise,
		CounterClockWise
	}

	internal class GeoOrientationFormatter : IJsonFormatter<GeoOrientation>
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
			switch (enumString)
			{
				case "LEFT":
				case "CW":
				case "CLOCKWISE":
					return GeoOrientation.ClockWise;
			}
			// Default, complies with the OGC standard
			return GeoOrientation.CounterClockWise;
		}
	}

	internal class NullableGeoOrientationFormatter : IJsonFormatter<GeoOrientation?>
	{
		public void Serialize(ref JsonWriter writer, GeoOrientation? value, IJsonFormatterResolver formatterResolver)
		{
			if (!value.HasValue)
			{
				writer.WriteNull();
				return;
			}

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

		public GeoOrientation? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var enumString = reader.ReadString();

			if (string.IsNullOrEmpty(enumString))
			{
				return null;
			}

			switch (enumString)
			{
				case "LEFT":
				case "CW":
				case "CLOCKWISE":
					return GeoOrientation.ClockWise;
				case "RIGHT":
				case "CCW":
				case "COUNTERCLOCKWISE":
					return GeoOrientation.CounterClockWise;
			}

			return null;
		}
	}
}
