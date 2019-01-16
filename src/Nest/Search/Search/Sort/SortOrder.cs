using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum SortOrder
	{
		[EnumMember(Value = "asc")]
		Ascending,

		[EnumMember(Value = "desc")]
		Descending
	}
}
