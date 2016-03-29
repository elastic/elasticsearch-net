using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DateInterval
	{
		[EnumMember(Value = "second")]
		Second, 
		[EnumMember(Value = "minute")]
		Minute, 
		[EnumMember(Value = "hour")]
		Hour, 
		[EnumMember(Value = "day")]
		Day,
		[EnumMember(Value = "week")]
		Week, 
		[EnumMember(Value = "month")]
		Month, 
		[EnumMember(Value = "quarter")]
		Quarter, 
		[EnumMember(Value = "year")]
		Year
	}

}
