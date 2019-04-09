using Newtonsoft.Json;

namespace Nest
{
	public class RolloverAction : LifecycleActionBase
	{
		public RolloverAction() : base("rollover"){ }

		[JsonProperty("max_size")]
		public long? MaximumSize { get; internal set; }

		[JsonProperty("max_age")]
		public Time MaximumAge { get; internal set; }

		[JsonProperty("max_docs")]
		public long? MaximumDocuments { get; internal set; }
	}
}
