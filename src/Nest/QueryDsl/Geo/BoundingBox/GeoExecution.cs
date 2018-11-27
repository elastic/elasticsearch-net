using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum GeoExecution
	{
		[EnumMember(Value = "memory")]
		Memory,

		[EnumMember(Value = "indexed")]
		Indexed
	}
}
