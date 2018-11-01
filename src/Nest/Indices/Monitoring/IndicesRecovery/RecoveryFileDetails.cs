using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryFileDetails
	{
		[JsonProperty("length")]
		public long Length { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("recovered")]
		public long Recovered { get; internal set; }
	}
}
