using Newtonsoft.Json;

namespace Nest {
	public class ShrinkAction : LifecycleActionBase
	{
		public ShrinkAction() : base("shrink"){ }

		[JsonProperty("number_of_shards")]
		public int NumberOfShards { get; internal set; }
	}
}
