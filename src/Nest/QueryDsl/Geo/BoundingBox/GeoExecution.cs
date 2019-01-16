using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum GeoExecution
	{
		[EnumMember(Value = "memory")]
		Memory,

		[EnumMember(Value = "indexed")]
		Indexed
	}
}
