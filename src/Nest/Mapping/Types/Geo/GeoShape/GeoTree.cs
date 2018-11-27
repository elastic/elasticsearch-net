using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
