using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NumericResolutionUnit
	{
		[EnumMember(Value = "milliseconds")]
		Milliseconds,
		[EnumMember(Value = "seconds")]
		Seconds
	}
}
