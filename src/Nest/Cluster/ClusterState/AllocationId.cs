using Newtonsoft.Json;

namespace Nest
{
	public class AllocationId
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }
	}
}
