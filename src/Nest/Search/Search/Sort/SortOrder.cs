using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
