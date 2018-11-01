using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISplitIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class SplitIndexResponse : AcknowledgedResponseBase, ISplitIndexResponse
	{
		public bool ShardsAcknowledged { get; internal set; }
	}
}
