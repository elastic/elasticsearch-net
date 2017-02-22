using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class NestedIdentity
	{
		[JsonProperty("field")]
		public Field Field { get; internal set; }

		[JsonProperty("offset")]
		public int Offset { get; internal set; }

		[JsonProperty("_nested")]
		public NestedIdentity Nested { get; internal set; }
	}
}