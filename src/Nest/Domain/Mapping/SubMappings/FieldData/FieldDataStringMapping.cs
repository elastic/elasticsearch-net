using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataStringFormat? Format { get; set; }
	}
}
