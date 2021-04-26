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
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum DateMathTimeUnit
	{
		[EnumMember(Value = "s")]
		Second,

		[EnumMember(Value = "m")]
		Minute,

		[EnumMember(Value = "h")]
		Hour,

		[EnumMember(Value = "d")]
		Day,

		[EnumMember(Value = "w")]
		Week,

		[EnumMember(Value = "M")]
		Month,

		[EnumMember(Value = "y")]
		Year
	}

	public static class DateMathTimeUnitExtensions
	{
		public static string GetStringValue(this DateMathTimeUnit value)
		{
			switch (value)
			{
				case DateMathTimeUnit.Second:
					return "s";
				case DateMathTimeUnit.Minute:
					return "m";
				case DateMathTimeUnit.Hour:
					return "h";
				case DateMathTimeUnit.Day:
					return "d";
				case DateMathTimeUnit.Week:
					return "w";
				case DateMathTimeUnit.Month:
					return "M";
				case DateMathTimeUnit.Year:
					return "y";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), value, null);
			}
		}
	}
}
