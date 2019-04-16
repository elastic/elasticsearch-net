using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum GapPolicy
	{
		[EnumMember(Value = "skip")]
		Skip,

		[EnumMember(Value = "insert_zeros")]
		InsertZeros
	}
}
