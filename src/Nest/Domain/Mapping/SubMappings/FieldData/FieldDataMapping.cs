using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class FieldDataMapping
	{
		[JsonProperty("loading")]
		public FieldDataLoading? Loading { get; set; }

		[JsonProperty("filter")]
		public FieldDataFilter Filter { get; set; }
	}
}
