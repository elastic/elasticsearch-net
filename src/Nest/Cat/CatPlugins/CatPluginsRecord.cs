using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatPluginsRecord : ICatRecord
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name  { get; set; }

		[JsonProperty("component")]
		public string Component { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("isolation")]
		public string Isolation { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}