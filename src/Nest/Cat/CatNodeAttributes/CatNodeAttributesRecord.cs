using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatNodeAttributesRecord : ICatRecord
	{
		// duration indices successful_shards failed_shards total_shards
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("pid")]
		public long ProcessId { get; set; }

		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("port")]
		public long Port { get; set; }

		[JsonProperty("attr")]
		public string Attribute { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

	}
}
