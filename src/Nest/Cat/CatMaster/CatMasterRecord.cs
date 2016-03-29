using Newtonsoft.Json;

namespace Nest
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