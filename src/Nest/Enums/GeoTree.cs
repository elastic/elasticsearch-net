using System.Runtime.Serialization;

namespace Nest
{
	public enum GeoTree
	{
		[EnumMember(Value = "geohash")]
		Geohash,
		[EnumMember(Value = "quadtree")]
		Quadtree
	}
}
