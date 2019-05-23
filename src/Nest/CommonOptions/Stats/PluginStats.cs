using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class PluginStats
	{
		[JsonProperty("classname")]
		public string ClassName { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("elasticsearch_version")]
		public string ElasticsearchVersion { get; set; }

		[JsonProperty("isolated")]
		public bool Isolated { get; set; }

		[JsonProperty("jvm")]
		public bool Jvm { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("site")]
		public bool Site { get; set; }

		[JsonProperty("java_version")]
		public string JavaVersion { get; set; }

		[JsonProperty("has_native_controller")]
		public bool HasNativeController { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }
	}
}
