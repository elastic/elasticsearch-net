using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum SortOrder
	{
		[EnumMember(Value = "asc")]
		Ascending,

		[EnumMember(Value = "desc")]
		Descending
	}
}
