using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICreateIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("shards_acknowledged")]
		bool ShardsAcknowledged { get; }

		[JsonProperty("index")]
		string Index { get; }
	}

	public class CreateIndexResponse : AcknowledgedResponseBase, ICreateIndexResponse
	{
		public bool ShardsAcknowledged { get; set; }

		public string Index { get; set; }
	}
}
