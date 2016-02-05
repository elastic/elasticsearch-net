using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class LatLon
	{
		[JsonProperty("lat")]
		public double? Lat { get; set; }

		[JsonProperty("lon")]
		public double? Lon { get; set; }
	}
}