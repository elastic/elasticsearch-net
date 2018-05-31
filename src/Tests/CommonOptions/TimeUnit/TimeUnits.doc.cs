using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
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
			* The `Milliseconds` property on `Time` is calculated even when not using the constructor that takes a `double`
			*/
			unitMilliseconds.Milliseconds.Should().Be(1000*60*60*24*2);
			unitComposed.Milliseconds.Should().Be(1000*60*60*24*2);
			unitTimeSpan.Milliseconds.Should().Be(1000*60*60*24*2);
			unitString.Milliseconds.Should().Be(1000*60*60*24*2);
		}
		/**
		* ==== Implicit conversion
		* There are implicit conversions from `string`, `TimeSpan` and `double` to an instance of `Time`, making them
		 * easier to work with
		*/
		[U] public void ImplicitConversion()
		{
			Time oneMinute = "1m";
			Time fourteenDays = TimeSpan.FromDays(14);
			Time twoDays = 1000*60*60*24*2;

			Expect("1m").WhenSerializing(oneMinute);
			Expect("14d").WhenSerializing(fourteenDays);
			Expect("2d").WhenSerializing(twoDays);
		}

		/**
		 * ==== Equality and Comparison
		 */
		[U] [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
		public void EqualityAndComparable()
		{
			/**
			* Comparisons on the expressions can be performed since Milliseconds are calculated
			* even when values are not passed as `double` milliseconds
			*/
			Time fourteenDays = TimeSpan.FromDays(14);
			fourteenDays.Milliseconds.Should().Be(1209600000);

			Time twoDays = 1000*60*60*24*2;

			fourteenDays.Should().BeGreaterThan(twoDays);
			(fourteenDays > twoDays).Should().BeTrue();
		    (twoDays != null).Should().BeTrue();
            (twoDays >= new Time("2d")).Should().BeTrue();

			twoDays.Should().BeLessThan(fourteenDays);
			(twoDays < fourteenDays).Should().BeTrue();
			(twoDays <= fourteenDays).Should().BeTrue();
			(twoDays <= new Time("2d")).Should().BeTrue();

			/**
			* Equality can also be performed
			*/
			twoDays.Should().Be(new Time("2d"));
			(twoDays == new Time("2d")).Should().BeTrue();
			(twoDays != new Time("2.1d")).Should().BeTrue();
			(new Time("2.1d") == new Time(TimeSpan.FromDays(2.1))).Should().BeTrue();

			/**
			 * Equality has down to 1/10 nanosecond precision
			 */
			Time oneNanosecond = new Time(1, Nest.TimeUnit.Nanoseconds);
			Time onePointNoughtNineNanoseconds = "1.09nanos";
			Time onePointOneNanoseconds = "1.1nanos";

			(oneNanosecond == onePointNoughtNineNanoseconds).Should().BeTrue();
			(oneNanosecond == onePointOneNanoseconds).Should().BeFalse();
		}

		/** ==== Special Time values
		 *
		 * Elasticsearch has two special values that can sometimes be passed where a `Time` is accepted
		 *
		 * - `0` represented as `Time.Zero`
		 * - `-1` represented as `Time.MinusOne`
		 */
		[U] public void SpecialTimeValues()
		{
			/**
			 * The following are all equal to `Time.MinusOne`
			 */
			Time.MinusOne.Should().Be(Time.MinusOne);
			new Time("-1").Should().Be(Time.MinusOne);
			new Time(-1).Should().Be(Time.MinusOne);
			((Time) (-1)).Should().Be(Time.MinusOne);
			((Time) "-1").Should().Be(Time.MinusOne);
			((Time) (-1)).Should().Be((Time) "-1");

			/**
			 * Similarly, the following are all equal to `Time.Zero`
			 */
			Time.Zero.Should().Be(Time.Zero);
			new Time("0").Should().Be(Time.Zero);
			new Time(0).Should().Be(Time.Zero);
			((Time) 0).Should().Be(Time.Zero);
			((Time) "0").Should().Be(Time.Zero);
			((Time) 0).Should().Be((Time) "0");

			/** Special Time values `0` and `-1` can be compared against other Time values
			 * although admittedly, this is a tad nonsensical.
			 */
			var twoDays = new Time(2, Nest.TimeUnit.Day);
			Time.MinusOne.Should().BeLessThan(Time.Zero);
			Time.Zero.Should().BeGreaterThan(Time.MinusOne);
			Time.Zero.Should().BeLessThan(twoDays);
			Time.MinusOne.Should().BeLessThan(twoDays);

			/**
			 * If there is a need to construct a time of -1ms or 0ms, use the constructor
			 * that accepts a factor and time unit, or specify a string with ms time units
			 */
			(new Time(-1, Nest.TimeUnit.Millisecond) == new Time("-1ms")).Should().BeTrue();
			(new Time(0, Nest.TimeUnit.Millisecond) == new Time("0ms")).Should().BeTrue();
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
				{ "10M", new TimeSpan(0, 10, 0), "10m" }, // 300 days not minutes
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
				{ "1000", "cannot be parsed to an interval"},
				{ "1000x", "is invalid"},
			};
			foreach (var testCase in testCases)
			{
				Action create = () => new Time(testCase.Item1);
				var e = create.Invoking((a) => a()).ShouldThrow<ArgumentException>(testCase.Item1).Subject.First();
				e.Message.Should().Contain(testCase.Item3);
			}
		}

		// hide
		private class DoubleParsingTestCases : List<Tuple<double, TimeSpan, string>>
		{
			public void Add(double original, TimeSpan expect, string toString) =>
				this.Add(Tuple.Create(original, expect, toString));
		}

		// hide
		[U]public void DoubleImplicitConversionParsing()
		{
			// from: https://msdn.microsoft.com/en-us/library/system.timespan.frommilliseconds.aspx
			// The value parameter is converted to ticks, and that number of ticks is used to initialize the new TimeSpan.
			// Therefore, value will only be considered accurate to the nearest millisecond. This means that
			// fractional millisecond values with TimeSpan.FromMilliseconds(fraction) will be rounded.
			var testCases = new DoubleParsingTestCases
			{
				{ 1e-4, new TimeSpan(1) , "100nanos"}, // smallest value that can be represented with ticks
				{ 1e-3, new TimeSpan(10), "1micros"},
				{ 0.1, TimeSpan.FromTicks(1000), "100micros"},
				{ 1, TimeSpan.FromMilliseconds(1), "1ms"},
				{ 1.2, TimeSpan.FromTicks(12000), "1200micros"},
				{ 10, TimeSpan.FromMilliseconds(10), "10ms"},
				{ 100, TimeSpan.FromMilliseconds(100), "100ms"},
				{ 1000, TimeSpan.FromSeconds(1), "1s" },
				{ 60000, TimeSpan.FromMinutes(1), "1m" },
				{ 3.6e+6, TimeSpan.FromHours(1), "1h" },
				{ 8.64e+7, TimeSpan.FromDays(1), "1d" },
				{ 1.296e+8, TimeSpan.FromDays(1.5), "36h" },
			};
			foreach (var testCase in testCases)
			{
				var time = new Time(testCase.Item1);
				time.ToTimeSpan().Should().Be(testCase.Item2, "we passed in {0}", testCase.Item1);
				time.ToString().Should().Be(testCase.Item3);
			}
		}

		// hide
		[U] public void DoubleImplicitConversionOneNanosecond()
		{
			Time oneNanosecond = 1e-6;
			// cannot be expressed as a TimeSpan using ToTimeSpan(), as smaller than a one tick.
			oneNanosecond.ToTimeSpan().Should().Be(TimeSpan.Zero);
			oneNanosecond.ToString().Should().Be("1nanos");
		}

		[U] public void UsingInterval()
		{
			/**
			* ==== Units of Time
			*
			* Where Units of Time can be specified as a union of either a `DateInterval` or `Time`,
			* a `DateInterval` or `Time` may be passed which will be implicity converted to a
			* `Union<DateInterval, Time>`, the serialized form of which represents the initial value
			* passed
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
			Expect("11664m").WhenSerializing<Union<DateInterval, Time>>((Time)TimeSpan.FromDays(8.1));
		}

		//hide
		[U] public void MillisecondsNeverSerializeToMonthsOrYears()
		{
			double millisecondsInAMonth = 2592000000;
			Expect("30d").WhenSerializing(new Time(millisecondsInAMonth));
			Expect("60d").WhenSerializing(new Time(millisecondsInAMonth * 2));
			Expect("360d").WhenSerializing(new Time(millisecondsInAMonth * 12));
			Expect("720d").WhenSerializing(new Time(millisecondsInAMonth * 24));
		}

		//hide
		[U] public void ExpectedValues()
		{
			Expect(0).WhenSerializing(new Time(0));
			Expect(0).WhenSerializing((Time)0);
			Expect(0).WhenSerializing(new Time("0"));
			Expect(0).WhenSerializing(Time.Zero);
			Expect(-1).WhenSerializing(new Time(-1));
			Expect(-1).WhenSerializing((Time)(-1));
			Expect(-1).WhenSerializing(new Time("-1"));
			Expect(-1).WhenSerializing(Time.MinusOne);

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
