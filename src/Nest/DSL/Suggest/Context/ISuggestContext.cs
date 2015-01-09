using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ISuggestContext
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }
	}
}
