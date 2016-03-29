using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(DistanceJsonConverter))]
	public class Distance
	{
		private static readonly Regex _distanceUnitRegex = new Regex(@"^(?<precision>\d+(?:\.\d+)?)(?<unit>\D+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		public double Precision { get; private set; }
		public DistanceUnit Unit { get; private set; }

		public Distance(double distance) : this(distance, DistanceUnit.Meters) { }

		public Distance(double distance, DistanceUnit unit) 
		{
			this.Precision = distance;
			this.Unit = unit;
		}

		public Distance(string distanceUnit)
		{
			distanceUnit.ThrowIfNullOrEmpty(nameof(distanceUnit));
			var match = _distanceUnitRegex.Match(distanceUnit);

			if (!match.Success)
				throw new ArgumentException("must be a valid distance unit", nameof(distanceUnit));

			var precision = double.Parse(match.Groups["precision"].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
			var unit = match.Groups["unit"].Value;

			this.Precision = precision;

			if (string.IsNullOrEmpty(unit))
			{
				this.Unit = DistanceUnit.Meters;
				return;
			}

			var unitMeasure = unit.ToEnum<DistanceUnit>();
			if (unitMeasure == null)
			{
				throw new InvalidCastException($"cannot parse {typeof(DistanceUnit).Name} from string '{unit}'");
			}

			this.Unit = unitMeasure.Value;
		}

		public static Distance Inches(double inches) => new Distance(inches, DistanceUnit.Inch);
		public static Distance Yards(double yards) => new Distance(yards, DistanceUnit.Yards);
		public static Distance Miles(double miles) => new Distance(miles, DistanceUnit.Miles);
		public static Distance Kilometers(double kilometers) => new Distance(kilometers, DistanceUnit.Kilometers);
		public static Distance Meters(double meters) => new Distance(meters, DistanceUnit.Meters);
		public static Distance Centimeters(double centimeters) => new Distance(centimeters, DistanceUnit.Centimeters);
		public static Distance Millimeters(double millimeter) => new Distance(millimeter, DistanceUnit.Millimeters);
		public static Distance NauticalMiles(double nauticalMiles) => new Distance(nauticalMiles, DistanceUnit.NauticalMiles);
		
		public static implicit operator Distance(string distanceUnit)
		{
			return new Distance(distanceUnit);
		}
	}
}
