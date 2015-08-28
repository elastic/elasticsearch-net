using System.Runtime.Serialization;

namespace Nest
{
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