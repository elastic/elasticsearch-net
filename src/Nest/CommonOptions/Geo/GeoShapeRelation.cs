using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum GeoShapeRelation
	{
		/// <summary>
		/// INTERSECTS relation.
		/// </summary>
		[EnumMember(Value = "intersects")]
		Intersects,

		/// <summary>
		/// DISJOINT relation.
		/// </summary>
		[EnumMember(Value = "disjoint")]
		Disjoint,

		/// <summary>
		/// WITHIN relation.
		/// </summary>
		[EnumMember(Value = "within")]
		Within,

		/// <summary>
		/// CONTAINS relation.
		/// <para />
		/// Supported for indices created with Elasticsearch 7.5.0+
		/// </summary>
		[EnumMember(Value = "contains")]
		Contains
	}
}
