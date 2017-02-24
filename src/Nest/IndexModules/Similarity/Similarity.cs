using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[ContractJsonConverter(typeof(SimilarityJsonConverter))]
	public interface ISimilarity
	{
		[JsonProperty("type")]
		string Type { get; }
	}
}
