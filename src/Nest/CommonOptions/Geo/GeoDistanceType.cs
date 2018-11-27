using System.Runtime.Serialization;

namespace Nest
{
	public enum GeoDistanceType
	{
		[EnumMember(Value = "arc")]
		Arc,

		[EnumMember(Value = "plane")]
		Plane
	}
}
