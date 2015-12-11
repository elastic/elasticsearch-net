using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.CommonOptions.TimeUnit
{
	public class TimeUnits 
	{
		/** #  Time units
		 * Whenever durations need to be specified, eg for a timeout parameter, the duration can be specified 
		 * as a whole number representing time in milliseconds, or as a time value like `2d` for 2 days. 
		 * 
		 * ## Using Time units in NEST
		 * NEST uses `TimeUnit` to strongly type this and there are several ways to construct one.
		 *
		 * ### Constructor
		 * The most straight forward way to construct a `TimeUnit` is through its constructor
		 */
		
		[U] public void Constructor()
		{
			var unitString = new Nest.TimeUnit("2d");
			var unitComposed = new Nest.TimeUnit(2, Nest.TimeUnitMeasure.Day);
			var unitTimeSpan = new Nest.TimeUnit(TimeSpan.FromDays(2));
			var unitMilliseconds = new Nest.TimeUnit(1000 * 60 * 60 * 24 * 2);
			
			/**
			* When serializing TimeUnit constructed from a string, composition of factor and interval, or a `TimeSpan`
			* the expression will be serialized as time unit string
			*/
			Expect("2d")
				.WhenSerializing(unitString)
				.WhenSerializing(unitComposed)
				.WhenSerializing(unitTimeSpan);
			/**
			* When constructed from a long representing milliseconds, a long will be serialized
			*/
			Expect(172800000).WhenSerializing(unitMilliseconds);

			/**
			* Milliseconds are always calculated even when not using the constructor that takes a long
			*/

			unitMilliseconds.Milliseconds.Should().Be(1000*60*60*24*2);
			unitComposed.Milliseconds.Should().Be(1000*60*60*24*2);
			unitTimeSpan.Milliseconds.Should().Be(1000*60*60*24*2);
			unitString.Milliseconds.Should().Be(1000*60*60*24*2);
		}
		/**
		* ### Implicit conversion
		* Alternatively `string`, `TimeSpan` and `long` can be implicitly assigned to `TimeUnit` properties and variables 
		*/

		[U] [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
		public void ImplicitConversion()
		{
			Nest.TimeUnit oneAndHalfYear = "1.5y";
			Nest.TimeUnit twoWeeks = TimeSpan.FromDays(14);
			Nest.TimeUnit twoDays = 1000*60*60*24*2;

			Expect("1.5y").WhenSerializing(oneAndHalfYear);
			Expect("2w").WhenSerializing(twoWeeks);
			Expect(172800000).WhenSerializing(twoDays);
		}


		[U] [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
		public void EqualityAndComparable()
		{
			Nest.TimeUnit oneAndHalfYear = "1.5y";
			Nest.TimeUnit twoWeeks = TimeSpan.FromDays(14);
			Nest.TimeUnit twoDays = 1000*60*60*24*2;

			/**
			* Milliseconds are calculated even when values are not passed as long
			*/
			oneAndHalfYear.Milliseconds.Should().BeGreaterThan(1);
			twoWeeks.Milliseconds.Should().BeGreaterThan(1);

			/**
			* This allows you to do comparisons on the expressions
			*/
			oneAndHalfYear.Should().BeGreaterThan(twoWeeks);
			(oneAndHalfYear > twoWeeks).Should().BeTrue();
			(oneAndHalfYear >= twoWeeks).Should().BeTrue();
			(twoDays >= new Nest.TimeUnit("2d")).Should().BeTrue();
			
			twoDays.Should().BeLessThan(twoWeeks);
			(twoDays < twoWeeks).Should().BeTrue();
			(twoDays <= twoWeeks).Should().BeTrue();
			(twoDays <= new Nest.TimeUnit("2d")).Should().BeTrue();
			
			/**
			* And assert equality
			*/
			twoDays.Should().Be(new Nest.TimeUnit("2d"));
			(twoDays == new Nest.TimeUnit("2d")).Should().BeTrue();
			(twoDays != new Nest.TimeUnit("2.1d")).Should().BeTrue();
			(new Nest.TimeUnit("2.1d") == new Nest.TimeUnit(TimeSpan.FromDays(2.1))).Should().BeTrue();
		}

		[U]
		public void UsingInterval()
		{
			/**
			* Time units are specified as a union of either a `DateInterval` or `TimeUnit`
			* both of which implicitly convert to the `Union` of these two.
			*/
			Expect("month").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Month);
			Expect("day").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Day);
			Expect("hour").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Hour);
			Expect("minute").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Minute);
			Expect("quarter").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Quarter);
			Expect("second").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Second);
			Expect("week").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Week);
			Expect("year").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>(DateInterval.Year);

			Expect("2d").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>((Nest.TimeUnit)"2d");
			Expect("1.16w").WhenSerializing<Union<DateInterval, Nest.TimeUnit>>((Nest.TimeUnit)TimeSpan.FromDays(8.1));
		}
	}
}
