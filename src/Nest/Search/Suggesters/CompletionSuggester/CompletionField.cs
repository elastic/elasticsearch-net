using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	///     Convenience class for use when indexing completion fields.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionField
	{
		[JsonProperty("contexts")]
		public IDictionary<string, IEnumerable<string>> Contexts { get; set; }

		[JsonProperty("input")]
		public IEnumerable<string> Input { get; set; }

		[JsonProperty("weight")]
		public int? Weight { get; set; }
	}
}
