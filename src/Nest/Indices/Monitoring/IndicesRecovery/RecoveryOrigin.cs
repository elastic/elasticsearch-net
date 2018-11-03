using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryOrigin
	{
		[JsonProperty("hostname")]
		public string HostName { get; internal set; }

		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }
	}
}
