using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataNonStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataNonStringFormat? Format { get; set; }
	}
}
