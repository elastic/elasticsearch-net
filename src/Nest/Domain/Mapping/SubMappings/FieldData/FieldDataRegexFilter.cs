using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataRegexFilter
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
	}
}
