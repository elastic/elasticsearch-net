using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Month
	{
		[EnumMember(Value = "january")]
		January,
		[EnumMember(Value = "february")]
		February,
		[EnumMember(Value = "march")]
		March,
		[EnumMember(Value = "april")]
		April,
		[EnumMember(Value = "may")]
		May,
		[EnumMember(Value = "june")]
		June,
		[EnumMember(Value = "july")]
		July,
		[EnumMember(Value = "august")]
		August,
		[EnumMember(Value = "september")]
		September,
		[EnumMember(Value = "october")]
		October,
		[EnumMember(Value = "november")]
		November,
		[EnumMember(Value = "december")]
		December
	}
}