// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Elastic.Transport.Extensions;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DistanceFormatter))]
	public class Distance
	{
		private static readonly Regex DistanceUnitRegex =
			new Regex(@"^(?<precision>\d+(?:\.\d+)?)(?<unit>\D+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		public Distance(double distance) : this(distance, DistanceUnit.Meters) { }

		public Distance(double distance, DistanceUnit unit)
		{
			Precision = distance;
			Unit = unit;
		}

		public Distance(string distanceUnit)
		{
			distanceUnit.ThrowIfNullOrEmpty(nameof(distanceUnit));
			var match = DistanceUnitRegex.Match(distanceUnit);

			if (!match.Success)
				throw new ArgumentException("must be a valid distance unit", nameof(distanceUnit));

			var precision = double.Parse(match.Groups["precision"].Value, NumberStyles.Any, CultureInfo.InvariantCulture);
			var unit = match.Groups["unit"].Value;

			Precision = precision;

			if (string.IsNullOrEmpty(unit))
			{
				Unit = DistanceUnit.Meters;
				return;
			}

			var unitMeasure = unit.ToEnum<DistanceUnit>();
			if (unitMeasure == null) throw new InvalidCastException($"cannot parse {typeof(DistanceUnit).Name} from string '{unit}'");

			Unit = unitMeasure.Value;
		}

		public double Precision { get; private set; }
		public DistanceUnit Unit { get; private set; }

		public static Distance Inches(double inches) => new Distance(inches, DistanceUnit.Inch);

		public static Distance Yards(double yards) => new Distance(yards, DistanceUnit.Yards);

		public static Distance Miles(double miles) => new Distance(miles, DistanceUnit.Miles);

		public static Distance Kilometers(double kilometers) => new Distance(kilometers, DistanceUnit.Kilometers);

		public static Distance Meters(double meters) => new Distance(meters, DistanceUnit.Meters);

		public static Distance Centimeters(double centimeters) => new Distance(centimeters, DistanceUnit.Centimeters);

		public static Distance Millimeters(double millimeter) => new Distance(millimeter, DistanceUnit.Millimeters);

		public static Distance NauticalMiles(double nauticalMiles) => new Distance(nauticalMiles, DistanceUnit.NauticalMiles);

		public static implicit operator Distance(string distanceUnit) => new Distance(distanceUnit);

		public override string ToString() => Precision.ToString(CultureInfo.InvariantCulture) + Unit.GetStringValue();
	}
}
