using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MultiValueMode
	{
		[EnumMember(Value = "min")]
		Min, 
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "sum")]
		Sum
	}
}