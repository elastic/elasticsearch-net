// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

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
			// Default, complies with the OGC standard
			if (reader.ReadIsNull())
				return GeoOrientation.CounterClockWise;

			var enumString = reader.ReadString();
			switch (enumString.ToUpperInvariant())
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
			if (reader.ReadIsNull()) return null;

			var enumString = reader.ReadString();

			switch (enumString.ToUpperInvariant())
			{
				case "LEFT":
				case "CW":
				case "CLOCKWISE":
					return GeoOrientation.ClockWise;
				case "RIGHT":
				case "CCW":
				case "COUNTERCLOCKWISE":
					return GeoOrientation.CounterClockWise;
				default:
					return null;
			}
		}
	}
}
