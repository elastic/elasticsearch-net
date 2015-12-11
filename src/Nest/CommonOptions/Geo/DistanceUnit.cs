using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(DistanceUnitJsonConverter))]
	public class DistanceUnit
	{
		private static readonly Regex _distanceUnitRegex = new Regex(@"^(?<precision>\d+(?:\.\d+)?)(?<unit>\D+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		public double Precision { get; private set; }
		public DistanceUnitMeasure Unit { get; private set; }

		public DistanceUnit(double distance) : this(distance, DistanceUnitMeasure.Meters) { }

		public DistanceUnit(double distance, DistanceUnitMeasure unit) 
		{
			this.Precision = distance;
			this.Unit = unit;
		}

		public DistanceUnit(string distanceUnit)
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
				this.Unit = DistanceUnitMeasure.Meters;
				return;
			}

			var unitMeasure = unit.ToEnum<DistanceUnitMeasure>();
			if (unitMeasure == null)
			{
				throw new InvalidCastException($"cannot parse {typeof(DistanceUnitMeasure).Name} from string '{unit}'");
			}

			this.Unit = unitMeasure.Value;
		}

		public static DistanceUnit Inches(double inches) => new DistanceUnit(inches, DistanceUnitMeasure.Inch);
		public static DistanceUnit Yards(double yards) => new DistanceUnit(yards, DistanceUnitMeasure.Yards);
		public static DistanceUnit Miles(double miles) => new DistanceUnit(miles, DistanceUnitMeasure.Miles);
		public static DistanceUnit Kilometers(double kilometers) => new DistanceUnit(kilometers, DistanceUnitMeasure.Kilometers);
		public static DistanceUnit Meters(double meters) => new DistanceUnit(meters, DistanceUnitMeasure.Meters);
		public static DistanceUnit Centimeters(double centimeters) => new DistanceUnit(centimeters, DistanceUnitMeasure.Centimeters);
		public static DistanceUnit Millimeters(double millimeter) => new DistanceUnit(millimeter, DistanceUnitMeasure.Millimeters);
		public static DistanceUnit NauticalMiles(double nauticalMiles) => new DistanceUnit(nauticalMiles, DistanceUnitMeasure.NauticalMiles);
		
		public static implicit operator DistanceUnit(string distanceUnit)
		{
			return new DistanceUnit(distanceUnit);
		}
	}
}
