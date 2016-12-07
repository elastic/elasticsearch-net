using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TimeUnit
	{
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
}
