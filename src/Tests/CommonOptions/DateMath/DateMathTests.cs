using System;
using FluentAssertions;
using Tests.Framework;

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

		[U]
		public void ImplicitConversionFromDateMathString()
		{
			string nullString = "now+3d";
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