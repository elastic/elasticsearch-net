using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Convenience class for use when indexing completion fields.
	/// </summary>
	[JsonObject]
	public class CompletionField<TPayload>
		where TPayload : class
	{
		[JsonProperty("input")]
		public IEnumerable<string> Input { get; set; }

		[JsonProperty("output")]
		public string Output { get; set; }

		[JsonProperty("payload")]
		public TPayload Payload { get; set; }

		[JsonProperty("weight")]
		public int? Weight { get; set; }

		[JsonProperty("context")]
		public IDictionary<string, IEnumerable<string>> Context { get; set; }
	}
}
