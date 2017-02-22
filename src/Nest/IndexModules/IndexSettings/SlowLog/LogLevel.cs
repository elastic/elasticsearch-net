using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LogLevel
	{
		[EnumMember(Value = "error")]
		Error,
		[EnumMember(Value = "warn")]
		Warn,
		[EnumMember(Value = "info")]
		Info,
		[EnumMember(Value = "debug")]
		Debug,
		[EnumMember(Value = "trace")]
		Trace
	}
}
