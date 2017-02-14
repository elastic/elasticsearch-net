using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(EnumMemberValueCasingJsonConverter<TimeUnit>))]
	public enum TimeUnit
	{
		[EnumMember(Value = "nanos")]
		Nanoseconds,
		[EnumMember(Value = "micros")]
		Microseconds,
		[EnumMember(Value = "ms")]
		Millisecond,
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

	public static class TimeUnitExtensions
	{
		public static string GetStringValue(this TimeUnit value)
		{
			switch (value)
			{
				case TimeUnit.Nanoseconds:
					return "nanos";
				case TimeUnit.Microseconds:
					return "micros";
				case TimeUnit.Millisecond:
					return "ms";
				case TimeUnit.Second:
					return "s";
				case TimeUnit.Minute:
					return "m";
				case TimeUnit.Hour:
					return "h";
				case TimeUnit.Day:
					return "d";
				case TimeUnit.Week:
					return "w";
				case TimeUnit.Month:
					return "M";
				case TimeUnit.Year:
					return "y";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), value, null);
			}
		}
	}
}
