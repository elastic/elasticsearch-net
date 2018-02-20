using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	[Obsolete("Scheduled to be removed in 6.0")]
	public enum GeoOptimizeBBox
	{
		[EnumMember(Value = "memory")]
		Memory,
		[EnumMember(Value = "indexed")]
		Indexed,
		[EnumMember(Value = "none")]
		None
	}
}
