using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(EnumMemberValueCasingJsonConverter<TimeUnit>))]
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
