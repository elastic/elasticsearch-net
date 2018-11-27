using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

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
