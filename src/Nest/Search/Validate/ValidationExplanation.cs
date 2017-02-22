using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class ValidationExplanation
	{
		[JsonProperty(PropertyName = "index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "valid")]
		public bool Valid { get; internal set; }
		
		[JsonProperty(PropertyName = "error")]
		public string Error { get; internal set; }
		
		[JsonProperty(PropertyName = "explanation")]
		public string Explanation { get; internal set; }
	}
}
