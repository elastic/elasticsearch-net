using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(SimilarityJsonConverter))]
	public interface ISimilarity
	{
		[JsonProperty("type")]
		string Type { get; }
	}
}
