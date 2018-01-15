using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(EnumMemberValueCasingJsonConverter<DateMathTimeUnit>))]
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
