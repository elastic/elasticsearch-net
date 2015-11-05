using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonConverter(typeof(GeoDistanceJsonConverter))]
	public class GeoDistance
	{
		public double Precision { get; private set; }
		public GeoPrecisionUnit Unit { get; private set; }

		public GeoDistance(double distance) : this(distance, GeoPrecisionUnit.Meters) { }
		public GeoDistance(double distance, GeoPrecisionUnit unit) 
		{
			this.Precision = distance;
			this.Unit = unit;
		}
		public static GeoDistance Inches(double inches) => new GeoDistance(inches, GeoPrecisionUnit.Inch);
		public static GeoDistance Yards(double yards) => new GeoDistance(yards, GeoPrecisionUnit.Yard);
		public static GeoDistance Miles(double miles) => new GeoDistance(miles, GeoPrecisionUnit.Miles);
		public static GeoDistance Kilometers(double kilometers) => new GeoDistance(kilometers, GeoPrecisionUnit.Kilometers);
		public static GeoDistance Meters(double meters) => new GeoDistance(meters, GeoPrecisionUnit.Meters);
		public static GeoDistance Centimeters(double centimeters) => new GeoDistance(centimeters, GeoPrecisionUnit.Centimeters);
		public static GeoDistance Millimeter(double millimeter) => new GeoDistance(millimeter, GeoPrecisionUnit.Millimeters);
	}
}
