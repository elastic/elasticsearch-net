using Newtonsoft.Json;

namespace Nest
{
	public class BlockState
	{
		[JsonProperty("read_only")]
		public bool ReadOnly { get; set; }
	}
}
