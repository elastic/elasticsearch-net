using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.CommonOptions.DateMath
{
	public class DateMathEpressions
	{
		/** == Date Math Expressions
		 * The date type supports using date math expression when using it in a query/filter
		 * Whenever durations need to be specified, eg for a timeout parameter, the duration can be specified 
		 *
		 * The expression starts with an "anchor" date, which can be either now or a date string (in the applicable format) ending with `||`. 
		 * It can then follow by a math expression, supporting `+`, `-` and `/` (rounding). 
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
		 * as a whole number representing time in milliseconds, or as a time value like `2d` for 2 days. 
		 * :datemath: {ref_current}/common-options.html#date-math
		 * Be sure to read the Elasticsearch documentation on {datemath}[Date Math].
		 */
		[U] public void SimpleExpressions()
		{
			/** === Simple Expressions
			* You can create simple expressions using any of the static methods on `DateMath` 
			*/
			Expect("now").WhenSerializing(Nest.DateMath.Now);
			Expect("2015-05-05T00:00:00").WhenSerializing(Nest.DateMath.Anchored(new DateTime(2015,05, 05)));
			
			/** strings implicitly convert to `DateMath` */
			Expect("now").WhenSerializing<Nest.DateMath>("now");

			/** but are lenient to bad math expressions */
			var nonsense = "now||*asdaqwe";

			Expect(nonsense).WhenSerializing<Nest.DateMath>(nonsense)
				/** the resulting date math will assume the whole string is the anchor */
				.Result(dateMath => ((IDateMath)dateMath)
					.Anchor.Match(
						d => d.Should().NotBe(default(DateTime)), 
						s => s.Should().Be(nonsense)
					)
				);
			
			/** `DateTime` also implicitly convert to simple date math expressions */
			var date = new DateTime(2015, 05, 05);
			Expect("2015-05-05T00:00:00").WhenSerializing<Nest.DateMath>(date)
				/** the anchor will be an actual `DateTime`, even after a serialization/deserialization round trip */
				.Result(dateMath => ((IDateMath)dateMath)
				.	Anchor.Match(
						d => d.Should().Be(date), 
						s => s.Should().BeNull()
					)
				);
		}

		[U] public void ComplexExpressions()
		{
			/** === Complex Expressions
			* Ranges can be chained on to simple expressions 
			*/
			Expect("now+1d").WhenSerializing(
				Nest.DateMath.Now.Add("1d"));

			/** Including multiple operations */
			Expect("now+1d-1m").WhenSerializing(
				Nest.DateMath.Now.Add("1d").Subtract(TimeSpan.FromMinutes(1)));

			/** A rounding value can be chained to the end of the expression, after which no more ranges can be appended */
			Expect("now+1d-1m/d").WhenSerializing(
				Nest.DateMath.Now.Add("1d")
					.Subtract(TimeSpan.FromMinutes(1))
					.RoundTo(Nest.TimeUnit.Day));
			
			/** When anchoring dates, a `||` needs to be appended as clear separator between the anchor and ranges.
			* Again, multiple ranges can be chained 
			*/
			Expect("2015-05-05T00:00:00||+1d-1m").WhenSerializing(
				Nest.DateMath.Anchored(new DateTime(2015,05,05))
					.Add("1d")
					.Subtract(TimeSpan.FromMinutes(1)));
		}

	}
}
