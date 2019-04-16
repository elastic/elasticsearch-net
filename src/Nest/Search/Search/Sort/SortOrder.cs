using System.Runtime.Serialization;
using Elasticsearch.Net;


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
