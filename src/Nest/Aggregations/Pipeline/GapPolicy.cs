using System.Runtime.Serialization;

namespace Nest
{
	public enum GapPolicy
	{
		[EnumMember(Value = "skip")]
		Skip,

		[EnumMember(Value = "insert_zeros")]
		InsertZeros
	}
}
