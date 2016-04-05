using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.CommonOptions.DistanceUnit
{
	public class DistanceUnits
	{
		/**== Distance Units
		 * Whenever distances need to be specified, e.g. for a {ref_current}/query-dsl-geo-distance-query.html[geo distance query],
		 * the distance unit can be specified as a double number representing distance in meters, as a new instance of
		 * a `Distance`, or as a string of the form number and distance unit e.g. "`2.72km`"
		 *
		 * === Using Distance units in NEST
		 * NEST uses `Distance` to strongly type distance units and there are several ways to construct one.
		 *
		 * ==== Constructor
		 * The most straight forward way to construct a `Distance` is through its constructor
		 */
		[U]
		public void Constructor()
		{
			var unitComposed = new Distance(25);
			var unitComposedWithUnits = new Distance(25, Nest.DistanceUnit.Meters);

			/**
			* `Distance` serializes to a string composed of a factor and distance unit.
			* The factor is a double so always has at least one decimal place when serialized
			*/
			Expect("25.0m")
				.WhenSerializing(unitComposed)
				.WhenSerializing(unitComposedWithUnits);
		}

		/**
		* ==== Implicit conversion
		* Alternatively a distance unit `string` can be assigned to a `Distance`, resulting in an implicit conversion to a new `Distance` instance.
		* If no `DistanceUnit` is specified, the default distance unit is meters
		*/
		[U]
		public void ImplicitConversion()
		{
			Distance distanceString = "25";
			Distance distanceStringWithUnits = "25m";

			Expect(new Distance(25))
				.WhenSerializing(distanceString)
				.WhenSerializing(distanceStringWithUnits);
		}

		/**
		* ==== Supported units
		* A number of distance units are supported, from millimeters to nautical miles
		*/
		[U]
		public void UsingDifferentUnits()
		{
			/** ===== Metric
			*`mm` (Millimeters)
			*/
			Expect("2.0mm").WhenSerializing(new Distance(2, Nest.DistanceUnit.Millimeters));

			/**
			*`cm` (Centimeters)
			*/
			Expect("123.456cm").WhenSerializing(new Distance(123.456, Nest.DistanceUnit.Centimeters));

			/**
			*`m` (Meters)
			*/
			Expect("400.0m").WhenSerializing(new Distance(400, Nest.DistanceUnit.Meters));

			/**
			*`km` (Kilometers)
			*/
			Expect("0.1km").WhenSerializing(new Distance(0.1, Nest.DistanceUnit.Kilometers));

			/** ===== Imperial
			*`in` (Inches)
			*/
			Expect("43.23in").WhenSerializing(new Distance(43.23, Nest.DistanceUnit.Inch));

			/**
			*`ft` (Feet)
			*/
			Expect("3.33ft").WhenSerializing(new Distance(3.33, Nest.DistanceUnit.Feet));

			/**
			*`yd` (Yards)
			*/
			Expect("9.0yd").WhenSerializing(new Distance(9, Nest.DistanceUnit.Yards));

			/**
			*`mi` (Miles)
			*/
			Expect("0.62mi").WhenSerializing(new Distance(0.62, Nest.DistanceUnit.Miles));

			/**
			*`nmi` or `NM` (Nautical Miles)
			*/
			Expect("45.5nmi").WhenSerializing(new Distance(45.5, Nest.DistanceUnit.NauticalMiles));
		}
	}
}
