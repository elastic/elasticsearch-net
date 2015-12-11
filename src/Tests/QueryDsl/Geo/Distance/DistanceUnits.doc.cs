using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.QueryDsl.Geo.Distance
{
	public class DistanceUnits
	{
		/** #  Distance Units
		 * Whenever distances need to be specified, e.g. for a geo distance query, the distance unit can be specified 
		 * as a double number representing distance in meters, as a new instance of a `DistanceUnit`, or as a string 
		 * of the form number and distance unit e.g. `"2.72km"`
		 * 
		 * ## Using Distance units in NEST
		 * NEST uses `DistanceUnit` to strongly type distance units and there are several ways to construct one.
		 *
		 * ### Constructor
		 * The most straight forward way to construct a `DistanceUnit` is through its constructor
		 */
		[U]
		public void Constructor()
		{
			var unitComposed = new DistanceUnit(25);
			var unitComposedWithUnits = new DistanceUnit(25, DistanceUnitMeasure.Meters);

			/**
			* When serializing DistanceUnit constructed from a string, composition of distance value and unit
			*/
			Expect("25.0m")
				.WhenSerializing(unitComposed)
				.WhenSerializing(unitComposedWithUnits);
		}

		/**
		* ### Implicit conversion
		* Alternatively a distance unit `string` can be assigned to a `DistanceUnit`, resulting in an implicit conversion to a new `DistanceUnit` instance. 
		* If no `DistanceUnitMeasure` is specified, the default distance unit is meters
		*/
		[U]
		public void ImplicitConversion()
		{
			DistanceUnit unitString = "25";
			DistanceUnit unitStringWithUnits = "25m";

			Expect(new DistanceUnit(25))
				.WhenSerializing(unitString)
				.WhenSerializing(unitStringWithUnits);
		}

		/**
		* ### Supported units
		* A number of units are supported, from millimeters to nautical miles
		*/
		[U]
		public void UsingDifferentUnits()
		{
			/**
			* Miles
			*/
			Expect("0.62mi").WhenSerializing(new DistanceUnit(0.62, DistanceUnitMeasure.Miles));

			/**
			* Yards
			*/
			Expect("9.0yd").WhenSerializing(new DistanceUnit(9, DistanceUnitMeasure.Yards));

			/**
			* Feet
			*/
			Expect("3.33ft").WhenSerializing(new DistanceUnit(3.33, DistanceUnitMeasure.Feet));

			/**
			* Inches
			*/
			Expect("43.23in").WhenSerializing(new DistanceUnit(43.23, DistanceUnitMeasure.Inch));

			/**
			* Kilometers
			*/
			Expect("0.1km").WhenSerializing(new DistanceUnit(0.1, DistanceUnitMeasure.Kilometers));

			/**
			* Meters
			*/
			Expect("400.0m").WhenSerializing(new DistanceUnit(400, DistanceUnitMeasure.Meters));

			/**
			* Centimeters
			*/
			Expect("123.456cm").WhenSerializing(new DistanceUnit(123.456, DistanceUnitMeasure.Centimeters));

			/**
			* Millimeters
			*/
			Expect("2.0mm").WhenSerializing(new DistanceUnit(2, DistanceUnitMeasure.Millimeters));

			/**
			* Nautical Miles
			*/
			Expect("45.5nmi").WhenSerializing(new DistanceUnit(45.5, DistanceUnitMeasure.NauticalMiles));
		}
	}
}