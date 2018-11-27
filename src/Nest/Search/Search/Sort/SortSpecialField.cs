using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
