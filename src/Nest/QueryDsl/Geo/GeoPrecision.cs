using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonConverter(typeof(GeoPrecisionJsonConverter))]
	public class GeoPrecision
	{
		public double Precision { get; private set; }
		public GeoPrecisionUnit Unit { get; private set; }

		public GeoPrecision(double precision) : this(precision, GeoPrecisionUnit.Meters) { }
		public GeoPrecision(double precision, GeoPrecisionUnit unit) 
		{
			this.Precision = precision;
			this.Unit = unit;
		}
		public static GeoPrecision Inches(double inches) => new GeoPrecision(inches, GeoPrecisionUnit.Inch);
		public static GeoPrecision Yards(double yards) => new GeoPrecision(yards, GeoPrecisionUnit.Yard);
		public static GeoPrecision Miles(double miles) => new GeoPrecision(miles, GeoPrecisionUnit.Miles);
		public static GeoPrecision Kilometers(double kilometers) => new GeoPrecision(kilometers, GeoPrecisionUnit.Kilometers);
		public static GeoPrecision Meters(double meters) => new GeoPrecision(meters, GeoPrecisionUnit.Meters);
		public static GeoPrecision Centimeters(double centimeters) => new GeoPrecision(centimeters, GeoPrecisionUnit.Centimeters);
		public static GeoPrecision Millimeter(double millimeter) => new GeoPrecision(millimeter, GeoPrecisionUnit.Millimeters);
	}
}
