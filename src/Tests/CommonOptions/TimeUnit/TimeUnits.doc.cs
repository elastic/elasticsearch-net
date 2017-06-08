using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.CommonOptions.TimeUnit
{
	public class TimeUnits
	{
		/**[[time-units]]
		 * === Time units
		 * Whenever durations need to be specified, eg for a timeout parameter, the duration can be specified
		 * as a whole number representing time in milliseconds, or as a time value like `2d` for 2 days.
		 *
		 * NEST uses a `Time` type to strongly type this and there are several ways to construct one.
		 *
		 * ==== Constructor
		 * The most straight forward way to construct a `Time` is through its constructor
		 */
		[U] public void Constructor()
		{
			var unitString = new Time("2d");
			var unitComposed = new Time(2, Nest.TimeUnit.Day);
			var unitTimeSpan = new Time(TimeSpan.FromDays(2));
			var unitMilliseconds = new Time(1000 * 60 * 60 * 24 * 2);

			/**
			* When serializing Time constructed from
			* - a string
			* - milliseconds (as a double)
			* - composition of factor and interval
			* - a `TimeSpan`
			*
			* the expression will be serialized to a time unit string composed of the factor and interval e.g. `2d`
			*/
			Expect("2d")
				.WhenSerializing(unitString)
				.WhenSerializing(unitComposed)
				.WhenSerializing(unitTimeSpan)
				.WhenSerializing(unitMilliseconds);

			/**
			* The `Milliseconds` property on `Time` is calculated even when not using the constructor that takes a double
			*/
			unitMilliseconds.Milliseconds.Should().Be(1000*60*60*24*2);
			unitComposed.Milliseconds.Should().Be(1000*60*60*24*2);
			unitTimeSpan.Milliseconds.Should().Be(1000*60*60*24*2);
			unitString.Milliseconds.Should().Be(1000*60*60*24*2);
		}
		/**
		* ==== Implicit conversion
		* Alternatively to using the constructor, `string`, `TimeSpan` and `double` can be implicitly converted to `Time`
		*/
		[U] [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
		public void ImplicitConversion()
		{
			Time oneAndHalfYear = "1.5y";
			Time twoWeeks = TimeSpan.FromDays(14);
			Time twoDays = 1000*60*60*24*2;

			Expect("1.5y").WhenSerializing(oneAndHalfYear);
			Expect("2w").WhenSerializing(twoWeeks);
			Expect("2d").WhenSerializing(twoDays);
		}

		[U] [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
		public void EqualityAndComparable()
		{
			Time oneAndHalfYear = "1.5y";
			Time twoWeeks = TimeSpan.FromDays(14);
			Time twoDays = 1000*60*60*24*2;

			/**
			* Milliseconds are calculated even when values are not passed as long...
			*/
			twoWeeks.Milliseconds.Should().BeGreaterThan(1);

			/**
			* ...**except** when dealing with years or months, whose millsecond value cannot
			* be calculated *accurately*, since they are not fixed durations. For instance,
			* 30 vs 31 vs 28 days in a month, or 366 vs 365 days in a year.
			* In this instance, Milliseconds will be -1.
			*/
			oneAndHalfYear.Milliseconds.Should().Be(-1);

			/**
			* This allows you to do comparisons on the expressions
			*/
			oneAndHalfYear.Should().BeGreaterThan(twoWeeks);
			(oneAndHalfYear > twoWeeks).Should().BeTrue();
			(oneAndHalfYear >= twoWeeks).Should().BeTrue();
		    (twoDays != null).Should().BeTrue();
            (twoDays >= new Time("2d")).Should().BeTrue();

			twoDays.Should().BeLessThan(twoWeeks);
			(twoDays < twoWeeks).Should().BeTrue();
			(twoDays <= twoWeeks).Should().BeTrue();
			(twoDays <= new Time("2d")).Should().BeTrue();

			/**
			* Special Time values `0` and `-1` can be compared against each other
			* and other Time values although admittingly this is a tad non-sensical.
			*/
			Time.MinusOne.Should().BeLessThan(Time.Zero);
			Time.Zero.Should().BeGreaterThan(Time.MinusOne);
			Time.Zero.Should().BeLessThan(twoDays);
			Time.MinusOne.Should().BeLessThan(twoDays);

			/**
			* And assert equality
			*/
			twoDays.Should().Be(new Time("2d"));
			(twoDays == new Time("2d")).Should().BeTrue();
			(twoDays != new Time("2.1d")).Should().BeTrue();
			(new Time("2.1d") == new Time(TimeSpan.FromDays(2.1))).Should().BeTrue();
			//the string "-1" is not the same as double -1 which is milliseconds
			(new Time("-1") == new Time(-1)).Should().BeFalse();
			(new Time("-1") == Time.MinusOne).Should().BeTrue();
		}

        // hide
        private class StringParsingTestCases : List<Tuple<string, TimeSpan, string>>
		{
			public void Add(string original, TimeSpan expect, string toString) =>
				this.Add(Tuple.Create(original, expect, toString));

			public void Add(string bad, string argumentExceptionContains) =>
				this.Add(Tuple.Create(bad, TimeSpan.FromDays(1), argumentExceptionContains));
		}

        // hide
		[U]public void StringImplicitConversionParsing()
		{
			var testCases = new StringParsingTestCases
			{
				{ "2.000000000e-06ms", TimeSpan.FromMilliseconds(2.000000000e-06), "0.000002ms"},
				{ "3.1e-11ms", TimeSpan.FromMilliseconds(3.1e-11), "0.000000000031ms"},
				{ "1000 nanos", new TimeSpan(10) , "1000nanos"},
				{ "1000nanos", new TimeSpan(10), "1000nanos"},
				{ "1000 NANOS", new TimeSpan(10), "1000nanos" },
				{ "1000NANOS", new TimeSpan(10), "1000nanos" },
				{ "10micros", new TimeSpan(100), "10micros" },
				{ "10   MS", new TimeSpan(0, 0, 0, 0, 10), "10ms" },
				{ "10ms", new TimeSpan(0, 0, 0, 0, 10), "10ms" },
				{ "10   ms", new TimeSpan(0, 0, 0, 0, 10), "10ms" },
				{ "10s", new TimeSpan(0, 0, 10), "10s" },
				{ "-10s", new TimeSpan(0, 0, -10), "-10s" },
				{ "-10S", new TimeSpan(0, 0, -10), "-10s" },
				{ "10m", new TimeSpan(0, 10, 0) , "10m"},
				{ "10M", new TimeSpan(300, 0, 0, 0), "10M" }, // 300 days not minutes
				{ "10h", new TimeSpan(10, 0, 0), "10h" },
				{ "10H", new TimeSpan(10, 0, 0) , "10h"},
				{ "10d", new TimeSpan(10, 0, 0, 0) , "10d"},
			};
			foreach (var testCase in testCases)
			{
				var time = new Time(testCase.Item1);
				time.ToTimeSpan().Should().Be(testCase.Item2, "we passed in {0}", testCase.Item1);
				time.ToString().Should().Be(testCase.Item3);
			}
		}

        // hide
        [U]public void StringParseExceptions()
		{
			var testCases = new StringParsingTestCases
			{
				{ "1000", "missing an interval"},
				{ "1000x", "string is invalid"},
			};
			foreach (var testCase in testCases)
			{
				Action create = () => new Time(testCase.Item1);
				var e = create.Invoking((a) => a()).ShouldThrow<ArgumentException>(testCase.Item1).Subject.First();
				e.Message.Should().Contain(testCase.Item3);
			}
		}

		[U] public void UsingInterval()
		{
			/**
			* ==== Units of Time
			* Units of `Time` are specified as a union of either a `DateInterval` or `Time`,
			* both of which implicitly convert to the `Union` of these two.
			*/
			Expect("month").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Month);
			Expect("day").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Day);
			Expect("hour").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Hour);
			Expect("minute").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Minute);
			Expect("quarter").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Quarter);
			Expect("second").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Second);
			Expect("week").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Week);
			Expect("year").WhenSerializing<Union<DateInterval, Time>>(DateInterval.Year);

			Expect("2d").WhenSerializing<Union<DateInterval, Time>>((Time)"2d");
			Expect("1.16w").WhenSerializing<Union<DateInterval, Time>>((Time)TimeSpan.FromDays(8.1));
		}

		//hide
		[U] public void MillisecondsNeverSerializeToMonthsOrYears()
		{
			double millisecondsInAMonth = 2592000000;
			Expect("4.29w").WhenSerializing(new Time(millisecondsInAMonth));
			Expect("8.57w").WhenSerializing(new Time(millisecondsInAMonth * 2));
			Expect("51.43w").WhenSerializing(new Time(millisecondsInAMonth * 12));
			Expect("102.86w").WhenSerializing(new Time(millisecondsInAMonth * 24));
		}

		//hide
		[U] public void ExpectedValues()
		{
			Expect("0ms").WhenSerializing(new Time(0));
			Expect(0).WhenSerializing(new Time("0"));
			Expect(0).WhenSerializing(Time.Zero);
			Expect("-1ms").WhenSerializing(new Time(-1));
			Expect(-1).WhenSerializing(new Time("-1"));
			Expect(-1).WhenSerializing(Time.MinusOne);

			Assert(
				1, Nest.TimeUnit.Year, -1, "1y",
				new Time(1, Nest.TimeUnit.Year),
				new Time("1y")
			);

			Assert(
				1, Nest.TimeUnit.Month, -1, "1M",
				new Time(1, Nest.TimeUnit.Month),
				new Time("1M")
			);

			Assert(
				1, Nest.TimeUnit.Week, TimeSpan.FromDays(7).TotalMilliseconds, "1w",
				new Time(1, Nest.TimeUnit.Week),
				new Time("1w"),
				new Time(TimeSpan.FromDays(7).TotalMilliseconds)
			);

			Assert(
				1, Nest.TimeUnit.Day, TimeSpan.FromDays(1).TotalMilliseconds, "1d",
				new Time(1, Nest.TimeUnit.Day),
				new Time("1d"),
				new Time(TimeSpan.FromDays(1).TotalMilliseconds)
			);

			Assert(
				1, Nest.TimeUnit.Hour, TimeSpan.FromHours(1).TotalMilliseconds, "1h",
				new Time(1, Nest.TimeUnit.Hour),
				new Time("1h"),
				new Time(TimeSpan.FromHours(1).TotalMilliseconds)
			);

			Assert(
				1, Nest.TimeUnit.Minute, TimeSpan.FromMinutes(1).TotalMilliseconds, "1m",
				new Time(1, Nest.TimeUnit.Minute),
				new Time("1m"),
				new Time(TimeSpan.FromMinutes(1).TotalMilliseconds)
			);

			Assert(
				1, Nest.TimeUnit.Second, TimeSpan.FromSeconds(1).TotalMilliseconds, "1s",
				new Time(1, Nest.TimeUnit.Second),
				new Time("1s"),
				new Time(TimeSpan.FromSeconds(1).TotalMilliseconds)
			);
		}

		//hide
		private void Assert(double expectedFactor, Nest.TimeUnit expectedInterval, double expectedMilliseconds, string expectedSerialized, params Time[] times)
		{
			foreach (var time in times)
			{
				time.Factor.Should().Be(expectedFactor);
				time.Interval.Should().Be(expectedInterval);
				time.Milliseconds.Should().Be(expectedMilliseconds);
				Expect(expectedSerialized).WhenSerializing(time);
			}
		}
	}
}
