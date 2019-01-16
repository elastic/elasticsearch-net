using System.Runtime.Serialization;

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
