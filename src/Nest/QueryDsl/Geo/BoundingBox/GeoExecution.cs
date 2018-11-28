using System.Runtime.Serialization;
using System.Runtime.Serialization;


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
