using Newtonsoft.Json;

namespace Nest
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