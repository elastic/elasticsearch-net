using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class StoreStats
	{
		[JsonProperty(PropertyName = "size")]
		public string Size { get; set; }
		[JsonProperty(PropertyName = "size_in_bytes")]
		public double SizeInBytes { get; set; }
	}

}
