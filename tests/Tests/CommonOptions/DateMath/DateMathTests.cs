/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
