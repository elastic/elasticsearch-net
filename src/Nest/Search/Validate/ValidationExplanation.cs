using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ValidationExplanation
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("valid")]
		public bool Valid { get; internal set; }

		[JsonProperty("error")]
		public string Error { get; internal set; }

		[JsonProperty("explanation")]
		public string Explanation { get; internal set; }
	}
}
