using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardPath
	{
		[JsonProperty("state_path")]
		public string StatePath { get; set; }

		[JsonProperty("data_path")]
		public string DataPath { get; set; }

		[JsonProperty("is_custom_data_path")]
		public bool IsCustomDataPath { get; set; }
	}
}