using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardPath
	{
		[JsonProperty("state_path")]
		public string StatePath { get; internal set; }

		[JsonProperty("data_path")]
		public string DataPath { get; internal set; }

		[JsonProperty("is_custom_data_path")]
		public bool IsCustomDataPath { get; internal set; }
	}
}
