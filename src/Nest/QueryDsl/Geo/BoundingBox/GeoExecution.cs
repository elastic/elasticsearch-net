using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoExecution
	{
		[EnumMember(Value = "memory")]
		Memory,
		[EnumMember(Value = "indexed")]
		Indexed
	}
}
