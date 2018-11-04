using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ExplanationDetail
	{
		[JsonProperty(PropertyName = "description")]
		public string Description { get; internal set; }

		[JsonProperty(PropertyName = "details")]
		public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; } = EmptyReadOnly<ExplanationDetail>.Collection;

		[JsonProperty(PropertyName = "value")]
		public float Value { get; internal set; }
	}
}
