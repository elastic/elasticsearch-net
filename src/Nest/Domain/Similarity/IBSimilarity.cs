using Newtonsoft.Json;

namespace Nest
{
	public class IBSimilarity : SimilarityBase
	{
		public IBSimilarity()
		{
			this.Type = "IB";
		}

		[JsonProperty("distribution")]
		public string Distribution { get; set; }

		[JsonProperty("lambda")]
		public string Lambda { get; set; }
	}
}
