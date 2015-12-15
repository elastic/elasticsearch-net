using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SuggestField
	{
		[JsonProperty("input")]
		public string Input { get; set; }

		[JsonProperty("output")]
		public string Output { get; set; }

		[JsonProperty("payload")]
		public object Payload { get; set; }

		[JsonProperty("weight")]
		public int Weight { get; set; }

		[JsonProperty("context")]
		public IDictionary<string, IEnumerable<string>> Context { get; set; }
	}
}
