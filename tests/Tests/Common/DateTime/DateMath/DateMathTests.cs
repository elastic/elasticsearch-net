// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Tests.Comomon;

public class DateMathTests
{
	[U]
	public void ImplicitConversionFromNullString()
	{
		string nullString = null;
		DateMath dateMath = nullString;
		dateMath.Should().BeNull();
	}

	[U]
	public void ImplicitConversionFromNullNullableDateTime()
	{
		DateTime? nullableDateTime = null;
		DateMath dateMath = nullableDateTime;
		dateMath.Should().BeNull();
	}

	[U]
	public void ImplicitConversionFromDefaultDateTimeIsMinValue()
	{
		DateTime dateTime = default;
		DateMath dateMath = dateTime;
		dateMath.Should().NotBeNull();
		dateMath.ToString().Should().Be("0001-01-01T00:00:00");
	}

	[U]
	public void ImplicitConversionFromDateMathString()
	{
		var nullString = "now+3d";
		DateMath dateMath = nullString;
		dateMath.Should().NotBeNull();
	}

	[U]
	public void ImplicitConversionFromNullableDateTimeWithValue()
	{
		DateTime? nullableDateTime = DateTime.Now;
		DateMath dateMath = nullableDateTime;
		dateMath.Should().NotBeNull();
	}
}
