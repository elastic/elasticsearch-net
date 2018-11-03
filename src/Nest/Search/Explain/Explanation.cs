using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Explanation
	{
		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("details")]
		public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; } = EmptyReadOnly<ExplanationDetail>.Collection;

		[JsonProperty("value")]
		public float Value { get; internal set; }
	}
}
