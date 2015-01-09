using Newtonsoft.Json;

namespace Nest
{
	public class DefaultSimilarity : SimilarityBase
	{
		public DefaultSimilarity()
		{
			this.Type = "default";
		}

		[JsonProperty("discount_overlaps")]
		public bool DiscountOverlaps { get; set; }
	}
}
