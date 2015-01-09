using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RoutingAllocationEnableOption
	{
		[EnumMember(Value = "all")]
		All,
		[EnumMember(Value = "primaries")]
		Primaries,
		[EnumMember(Value = "new_primaries")]
		NewPrimaries,
		[EnumMember(Value = "none")]
		None
	}
}
