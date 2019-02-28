using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum GeoDistanceType
	{
		[EnumMember(Value = "arc")]
		Arc,

		[EnumMember(Value = "plane")]
		Plane
	}
}
