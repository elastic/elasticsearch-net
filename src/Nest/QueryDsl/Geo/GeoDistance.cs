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
		public GeoPrecision Unit { get; private set; }

		public GeoDistance(double distance) : this(distance, GeoPrecision.Meters) { }
		public GeoDistance(double distance, GeoPrecision unit) 
		{
			this.Precision = distance;
			this.Unit = unit;
		}
		public static GeoDistance Inches(double inches) => new GeoDistance(inches, GeoPrecision.Inch);
		public static GeoDistance Yards(double yards) => new GeoDistance(yards, GeoPrecision.Yard);
		public static GeoDistance Miles(double miles) => new GeoDistance(miles, GeoPrecision.Miles);
		public static GeoDistance Kilometers(double kilometers) => new GeoDistance(kilometers, GeoPrecision.Kilometers);
		public static GeoDistance Meters(double meters) => new GeoDistance(meters, GeoPrecision.Meters);
		public static GeoDistance Centimeters(double centimeters) => new GeoDistance(centimeters, GeoPrecision.Centimeters);
		public static GeoDistance Millimeter(double millimeter) => new GeoDistance(millimeter, GeoPrecision.Millimeters);
	}
}
