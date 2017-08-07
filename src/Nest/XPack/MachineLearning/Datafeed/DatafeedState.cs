using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DatafeedState
	{
		[EnumMember(Value = "started")]
		Started,
		[EnumMember(Value = "stopped")]
		Stopped,
		[EnumMember(Value = "starting")]
		Starting,
		[EnumMember(Value = "stopping")]
		Stopping
	}
}