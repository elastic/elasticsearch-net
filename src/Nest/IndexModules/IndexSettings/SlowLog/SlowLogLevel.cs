using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SlowLogLevel
	{
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