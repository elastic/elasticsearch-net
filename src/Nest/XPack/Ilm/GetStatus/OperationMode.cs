using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OperationMode
	{
		[EnumMember(Value = "RUNNING")]Running,
		[EnumMember(Value = "STOPPING")]Stopping,
		[EnumMember(Value = "STOPPED")]Stopped
	}
}
