using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoDistanceType
	{
		sloppy_arc,
		arc,
		plane
	}
}