using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

namespace Tests.CommonOptions.DateMath
{
	public class DateMathTests
	{
		[U]
		public void ImplicitConversionFromNullString()
		{
			string nullString = null;
			Nest.DateMath dateMath = nullString;
			dateMath.Should().BeNull();
		}

		[U]
		public void ImplicitConversionFromNullNullableDateTime()
		{
			DateTime? nullableDateTime = null;
			Nest.DateMath dateMath = nullableDateTime;
			dateMath.Should().BeNull();
		}

		[U] // F# backticks would be great in C# :)
		public void ImplicitConversionFromDefaultDateTimeIsNotNullButEmptyString()
		{
			// in 6.x DateMath is backed by a DateTime instance
			// for 7.x we will adress this
			DateTime nullableDateTime = default;
			Nest.DateMath dateMath = nullableDateTime;
			dateMath.Should().NotBeNull();
			dateMath.ToString().Should().BeEmpty();
		}

		[U]
		public void ImplicitConversionFromDateMathString()
		{
			var nullString = "now+3d";
			Nest.DateMath dateMath = nullString;
			dateMath.Should().NotBeNull();
		}

		[U]
		public void ImplicitConversionFromNullableDateTimeWithValue()
		{
			DateTime? nullableDateTime = DateTime.Now;
			Nest.DateMath dateMath = nullableDateTime;
			dateMath.Should().NotBeNull();
		}
	}

}
