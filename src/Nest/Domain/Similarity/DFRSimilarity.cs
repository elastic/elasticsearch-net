using Newtonsoft.Json;

namespace Nest
{
	public class DFRSimilarity : SimilarityBase
	{
		public DFRSimilarity()
		{
			this.Type = "DFR";
		}

		[JsonProperty("basic_model")]
		public string BasicModel { get; set; }

		[JsonProperty("after_effect")]
		public string AfterEffect { get; set; }
	}
}