using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(GeoOrientationConverter))]
	public enum GeoOrientation
	{
		ClockWise,
		CounterClockWise
	}

	internal class GeoOrientationConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var geoOrientation = (GeoOrientation)value;
			switch (geoOrientation)
			{
				case GeoOrientation.ClockWise:
					writer.WriteValue("cw");
					break;
				case GeoOrientation.CounterClockWise:
					writer.WriteValue("ccw");
					break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var enumString = (string)reader.Value;
			switch (enumString.ToLower())
			{
				case "left":
				case "cw":
				case "clockwise":
					return GeoOrientation.ClockWise;
			}
			// Default, complies with the OGC standard
			return GeoOrientation.CounterClockWise;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
