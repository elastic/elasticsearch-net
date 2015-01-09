using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataFilter
	{
		[JsonProperty("frequency")]
		public FieldDataFrequencyFilter Frequency { get; set; }

		[JsonProperty("regex")]
		public FieldDataRegexFilter Regex { get; set; }
	}
}
