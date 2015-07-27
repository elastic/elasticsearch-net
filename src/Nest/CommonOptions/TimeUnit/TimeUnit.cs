using System.Runtime.Serialization;

namespace Nest
{
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