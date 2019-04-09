using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class AllocateAction : LifecycleActionBase
	{
		public AllocateAction() : base("allocate"){ }

		[JsonProperty("number_of_replicas")]
		public int? NumberOfReplicas { get; internal set; }

		[JsonProperty("include")]
		public IReadOnlyDictionary<string, string> Include { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("exclude")]
		public IReadOnlyDictionary<string, string> Exclude { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("require")]
		public IReadOnlyDictionary<string, string> Require { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
