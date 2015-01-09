using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataFrequencyFilter
	{
		[JsonProperty("min")]
		public double? Min { get; set; }

		[JsonProperty("max")]
		public double? Max { get; set; }

		[JsonProperty("min_segment_size")]
		public int? MinSegmentSize { get; set; }
	}
}
