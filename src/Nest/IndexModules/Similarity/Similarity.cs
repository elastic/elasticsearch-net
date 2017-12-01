using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A similarity.
	/// </summary>
	[ContractJsonConverter(typeof(SimilarityJsonConverter))]
	public interface ISimilarity
	{
		/// <summary>
		/// The type of similarity.
		/// </summary>
		[JsonProperty("type")]
		string Type { get; }
	}
}
