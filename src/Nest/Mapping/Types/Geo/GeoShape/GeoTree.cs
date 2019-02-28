using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum GeoTree
	{
		[EnumMember(Value = "geohash")]
		Geohash,

		[EnumMember(Value = "quadtree")]
		Quadtree
	}
}
