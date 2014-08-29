using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatShardsRecord : ICatRecord
	{
		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public string Shard { get; set; }

		[JsonProperty("prirep")]
		public string PrimaryOrReplica { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("docs")]
		public string Docs { get; set; }

		[JsonProperty("store")]
		public string Store { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }
	}
}