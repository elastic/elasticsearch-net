using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class CatMasterRecord : ICatRecord
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }
	}
}