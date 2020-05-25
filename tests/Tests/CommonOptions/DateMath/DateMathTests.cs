// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
		public void ImplicitConversionFromDefaultDateTimeIsMinValue()
		{
			// in 6.x DateMath is backed by a DateTime instance
			// for 7.x we will adress this
			DateTime nullableDateTime = default;
			Nest.DateMath dateMath = nullableDateTime;
			dateMath.Should().NotBeNull();
			dateMath.ToString().Should().Be("0001-01-01T00:00:00");
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
