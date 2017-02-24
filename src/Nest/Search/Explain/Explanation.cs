using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class Explanation
	{
		[JsonProperty(PropertyName = "value")]
		public float Value { get; internal set; }
		[JsonProperty(PropertyName = "description")]
		public string Description { get; internal set; }

		[JsonProperty(PropertyName = "details")]
		public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; } = EmptyReadOnly<ExplanationDetail>.Collection;
	}
}
