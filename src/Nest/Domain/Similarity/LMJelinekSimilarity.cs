using Newtonsoft.Json;

namespace Nest
{
	public class LMJelinekSimilarity : SimilarityBase
	{
		public LMJelinekSimilarity()
		{
			this.Type = "LMJelinekMercer";
		}

		[JsonProperty("lambda")]
		public double Lambda { get; set; }
	}
}
