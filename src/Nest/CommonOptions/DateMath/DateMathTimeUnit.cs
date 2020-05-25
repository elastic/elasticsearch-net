// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
