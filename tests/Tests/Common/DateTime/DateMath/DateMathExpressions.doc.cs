// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.CommonOptions;

public class DateMathExpressions
{
	/**[[date-math-expressions]]
	 * === Date math expressions
	 * The date type supports using date math expression when using it in a query/filter
	 * Whenever durations need to be specified, eg for a timeout parameter, the duration can be specified
	 *
	 * The expression starts with an "anchor" date, which can be either now or a date string (in the applicable format) ending with `||`.
	 * It can be followed by a math expression, supporting `+`, `-` and `/` (rounding).
	 * The units supported are
	 *
	 * - `y` (year)
	 * - `M` (month)
	 * - `w` (week)
	 * - `d` (day)
	 * - `h` (hour)
	 * - `m` (minute)
	 * - `s` (second)
	 *
	 * :datemath: {ref_current}/common-options.html#date-math
	 * Be sure to read the Elasticsearch documentation on {datemath}[Date Math].
	 */
	[U] public void SimpleExpressions()
	{
		/**
		* ==== Simple expressions
		* You can create simple expressions using any of the static methods on `DateMath`
		*/
		//Expect("now").WhenSerializing(DateMath.Now);
		Expect("2015-05-05T00:00:00").WhenSerializing(DateMath.Anchored(new DateTime(2015,05, 05)));

		/** strings implicitly convert to `DateMath` */
		Expect("now").WhenSerializing<DateMath>("now");

		/** but are lenient to bad math expressions */
		var nonsense = "now||*asdaqwe";

		/** the resulting date math will assume the whole string is the anchor */
		Expect(nonsense)
			.WhenSerializing<DateMath>(nonsense)
			.AssertSubject(dateMath => dateMath
				.Anchor.Match(
					d => d.Should().NotBe(default),
					s => s.Should().Be(nonsense)
				)
			);

		/**`DateTime` also implicitly convert to simple date math expressions; the resulting
		 * anchor will be an actual `DateTime`, even after a serialization/deserialization round trip
		 */
		var date = new DateTime(2015, 05, 05);

		/**
		 * will serialize to
		 */
		//json
		var expected = "2015-05-05T00:00:00";

		// hide
		Expect(expected)
			.WhenSerializing<DateMath>(date)
			.AssertSubject(dateMath => dateMath
				.Anchor.Match(
					d => d.Should().Be(date),
					s => s.Should().BeNull()
				)
			);

		/**
		 * When the `DateTime` is local or UTC, the time zone information is included.
		 * For example, for a UTC `DateTime`
		 */
		var utcDate = new DateTime(2015, 05, 05, 0, 0, 0, DateTimeKind.Utc);

		/**
		 * will serialize to
		 */
		//json
		expected = "2015-05-05T00:00:00Z";

		// hide
		Expect(expected)
			.WhenSerializing<DateMath>(utcDate)
			.AssertSubject(dateMath => dateMath
				.Anchor.Match(
					d => d.Should().Be(utcDate),
					s => s.Should().BeNull()
				)
			);
	}

	[U] public void ComplexExpressions()
	{
		/**
		 * ==== Complex expressions
		* Ranges can be chained on to simple expressions
		*/
		Expect("now+1d").WhenSerializing(
			DateMath.Now.Add("1d"));

		/** Including multiple operations */
		Expect("now+1d-1m").WhenSerializing(
			DateMath.Now.Add("1d").Subtract(TimeSpan.FromMinutes(1)));

		/** A rounding value can be chained to the end of the expression, after which no more ranges can be appended */
		Expect("now+1d-1m/d").WhenSerializing(
			DateMath.Now.Add("1d")
				.Subtract(TimeSpan.FromMinutes(1))
				.RoundTo(DateMathTimeUnit.Day));

		/** When anchoring dates, a `||` needs to be appended as clear separator between the anchor and ranges.
		* Again, multiple ranges can be chained
		*/
		Expect("2015-05-05T00:00:00||+1d-1m").WhenSerializing(
			DateMath.Anchored(new DateTime(2015,05,05))
				.Add("1d")
				.Subtract(TimeSpan.FromMinutes(1)));
	}

	[U] public void FractionalsUnitsAreDroppedToNearestInteger()
	{
		/**
		* ==== Fractional times
		* Date math expressions within Elasticsearch do not support fractional numbers. To make working with Date math
		* easier within NEST, conversions from `string`, `TimeSpan` and `double` will convert a fractional value to the
		* largest whole number value and unit, rounded to the nearest second.
		*
		*/
		Expect("now+1w").WhenSerializing(DateMath.Now.Add(TimeSpan.FromDays(7)));

		Expect("now+1w").WhenSerializing(DateMath.Now.Add("1w"));

		Expect("now+1w").WhenSerializing(DateMath.Now.Add(604800000));

		Expect("now+7d").WhenSerializing(DateMath.Now.Add("7d"));

		Expect("now+30h").WhenSerializing(DateMath.Now.Add(TimeSpan.FromHours(30)));

		Expect("now+30h").WhenSerializing(DateMath.Now.Add("1.25d"));

		Expect("now+90001s").WhenSerializing(
			DateMath.Now.Add(TimeSpan.FromHours(25).Add(TimeSpan.FromSeconds(1))));

		Expect("now+90000s").WhenSerializing(
			DateMath.Now.Add(TimeSpan.FromHours(25).Add(TimeSpan.FromMilliseconds(1))));

		Expect("now+1y").WhenSerializing(DateMath.Now.Add("1y"));

		Expect("now+12M").WhenSerializing(DateMath.Now.Add("12M"));

		Expect("now+18M").WhenSerializing(DateMath.Now.Add("1.5y"));

		Expect("now+52w").WhenSerializing(DateMath.Now.Add(TimeSpan.FromDays(7 * 52)));
	}

	[U] public void Rounding()
	{
		/**
		 * ==== Rounding
		 * Rounding can be controlled using the constructor, and passing a value for rounding
		 */
		Expect("now+2s").WhenSerializing(
			DateMath.Now.Add(new DateMathTime("2.5s", MidpointRounding.ToEven)));

		Expect("now+3s").WhenSerializing(
			DateMath.Now.Add(new DateMathTime("2.5s", MidpointRounding.AwayFromZero)));

		Expect("now+0s").WhenSerializing(
			DateMath.Now.Add(new DateMathTime(500, MidpointRounding.ToEven)));

		Expect("now+1s").WhenSerializing(
			DateMath.Now.Add(new DateMathTime(500, MidpointRounding.AwayFromZero)));
	}

	[U] public void EqualityAndComparison()
	{
		/**
		 * ==== Equality and Comparisons
		 *
		 * `DateMathTime` supports implements equality and comparison
		 */

		DateMathTime twoSeconds = new DateMathTime(2, DateMathTimeUnit.Second);
		DateMathTime twoSecondsFromString = "2s";
		DateMathTime twoSecondsFromTimeSpan = TimeSpan.FromSeconds(2);
		DateMathTime twoSecondsFromDouble = 2000;

		twoSeconds.Should().Be(twoSecondsFromString);
		twoSeconds.Should().Be(twoSecondsFromTimeSpan);
		twoSeconds.Should().Be(twoSecondsFromDouble);

		DateMathTime threeSecondsFromString = "3s";
		DateMathTime oneMinuteFromTimeSpan = TimeSpan.FromMinutes(1);

		(threeSecondsFromString > twoSecondsFromString).Should().BeTrue();
		(oneMinuteFromTimeSpan > threeSecondsFromString).Should().BeTrue();

		/**
		 * Since years and months do not
		 * contain exact values
		 *
		 * - A year is approximated to 365 days
		 * - A month is approximated to (365 / 12) days
		 */
		DateMathTime oneYear = new DateMathTime(1, DateMathTimeUnit.Year);
		DateMathTime oneYearFromString = "1y";
		DateMathTime twelveMonths = new DateMathTime(12, DateMathTimeUnit.Month);
		DateMathTime twelveMonthsFromString = "12M";

		oneYear.Should().Be(oneYearFromString);
		oneYear.Should().Be(twelveMonths);
		twelveMonths.Should().Be(twelveMonthsFromString);

		DateMathTime thirteenMonths = new DateMathTime(13, DateMathTimeUnit.Month);
		DateMathTime thirteenMonthsFromString = "13M";
		DateMathTime fiftyTwoWeeks = "52w";

		(oneYear < thirteenMonths).Should().BeTrue();
		(oneYear < thirteenMonthsFromString).Should().BeTrue();
		(twelveMonths > fiftyTwoWeeks).Should().BeTrue();
		(oneYear > fiftyTwoWeeks).Should().BeTrue();
	}
}
