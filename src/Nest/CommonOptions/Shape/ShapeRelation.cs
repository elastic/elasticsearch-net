using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum ShapeRelation
	{
		[EnumMember(Value = "intersects")]
		Intersects,

		[EnumMember(Value = "disjoint")]
		Disjoint,

		[EnumMember(Value = "within")]
		Within
	}
}
