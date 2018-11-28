using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum SortSpecialField
	{
		[EnumMember(Value = "_score")]
		Score,

		[EnumMember(Value = "_doc")]
		DocumentIndexOrder
	}
}
