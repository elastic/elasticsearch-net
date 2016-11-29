using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Day
	{
		[EnumMember(Value = "sunday")]
		Sunday,
		[EnumMember(Value = "monday")]
		Monday,
		[EnumMember(Value = "tuesday")]
		Tuesday,
		[EnumMember(Value = "wednesday")]
		Wednesday,
		[EnumMember(Value = "thursday")]
		Thursday,
		[EnumMember(Value = "friday")]
		Friday,
		[EnumMember(Value = "saturday")]
		Saturday
	}
}