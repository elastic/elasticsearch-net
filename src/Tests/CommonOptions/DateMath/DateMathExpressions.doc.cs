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

namespace Tests.CommonOptions.DateMath
{
	public class DateMathEpressions
	{
		/** # Date Expressions
		* The date type supports using date math expression when using it in a query/filter
		 *Whenever durations need to be specified, eg for a timeout parameter, the duration can be specified 
		 *
		 * The expression starts with an "anchor" date, which can be either now or a date string (in the applicable format) ending with ||. 
		 * It can then follow by a math expression, supporting +, - and / (rounding). 
		 * The units supported are y (year), M (month), w (week), d (day), h (hour), m (minute), and s (second).
		 * as a whole number representing time in milliseconds, or as a time value like `2d` for 2 days. 
		 * 
		 * Be sure to read the elasticsearch documentation {ref}/mapping-date-format.html#date-math[on this subject here]

		 */

		[U] public void SimpleExpressions()
		{
			/** You can create simple expressions using any of the static methods on  `DateMath` */
			Expect("now").WhenSerializing(DateMath.Now);
			Expect("2015-05-05T00:00:00").WhenSerializing(DateMath.Anchored(new DateTime(2015,05, 05)));
			
			/** strings implicitly convert to date maths */
			Expect("now").WhenSerializing<DateMath>("now");

			/** but are lenient to bad math expressions */
			var nonsense = "now||*asdaqwe";
			Expect(nonsense).WhenSerializing<DateMath>(nonsense)
				/** the resulting date math will assume the whole string is the anchor */
				.Result(dateMath => ((IDateMath)dateMath)
					.Anchor.Match(
						d => d.Should().NotBe(default(DateTime)), 
						s => s.Should().Be(nonsense)
					)
				);
			
			/** date's also implicitly convert to simple date math expressions */
			var date = new DateTime(2015, 05, 05);
			Expect("2015-05-05T00:00:00").WhenSerializing<DateMath>(date)
				/** the anchor will be an actual DateTime, even after a serialization - deserialization round trip */
				.Result(dateMath => ((IDateMath)dateMath)
				.	Anchor.Match(
						d => d.Should().Be(date), 
						s => s.Should().BeNull()
					)
				);
		}

		[U] public void ComplexExpressions()
		{
			/** Ranges can be chained on to simple expressions */
			Expect("now+1d").WhenSerializing(DateMath.Now.Add("1d"));

			/** plural means that you can chain multiple */
			Expect("now+1d-1m").WhenSerializing(DateMath.Now.Add("1d").Subtract(TimeSpan.FromMinutes(1)));

			/** a rounding value can also be chained at the end afterwhich no more ranges can be appended */
			Expect("now+1d-1m/d").WhenSerializing(DateMath.Now.Add("1d").Subtract(TimeSpan.FromMinutes(1)).RoundTo(TimeUnit.Day));
			
			/** When anchoring date's we need to append `||` as clear separator between anchor and ranges */
			/** plural means that you can chain multiple */
			Expect("2015-05-05T00:00:00||+1d-1m").WhenSerializing(DateMath.Anchored(new DateTime(2015,05,05)).Add("1d").Subtract(TimeSpan.FromMinutes(1)));

		}

	}
}
