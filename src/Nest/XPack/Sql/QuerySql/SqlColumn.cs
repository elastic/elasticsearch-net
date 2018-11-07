using Newtonsoft.Json;

namespace Nest
{
	public class SqlColumn
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
