using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class PluginStats
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("classname")]
		public string ClassName { get; set; }

		[JsonProperty("jvm")]
		public bool Jvm { get; set; }

		[JsonProperty("isolated")]
		public bool Isolated { get; set; }

		[JsonProperty("site")]
		public bool Site { get; set; }
	}
}
