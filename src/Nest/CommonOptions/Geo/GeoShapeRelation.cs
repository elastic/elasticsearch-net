using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum GeoShapeRelation
	{
		[EnumMember(Value = "intersects")]
		Intersects,

		[EnumMember(Value = "disjoint")]
		Disjoint,

		[EnumMember(Value = "within")]
		Within,

		[EnumMember(Value = "contains")]
		Contains
	}
}
