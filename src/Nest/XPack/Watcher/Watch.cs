using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Watch
	{
		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Meta { get; internal set; }

		[JsonProperty("input")]
		public IInputContainer Input { get; internal set; }

		[JsonProperty("condition")]
		public IConditionContainer Condition { get; internal set; }

		[JsonProperty("trigger")]
		public ITriggerContainer Trigger { get; internal set; }

		[JsonProperty("transform")]
		public ITransformContainer Transform { get; internal set; }

		[JsonProperty("actions")]
		[JsonConverter(typeof(ActionsJsonConverter))]
		public Actions Actions { get; internal set; }

		[JsonProperty("status")]
		public WatchStatus Status { get; internal set; }

		[JsonProperty("throttle_period")]
		public string ThrottlePeriod { get; internal set; }
	}
}
