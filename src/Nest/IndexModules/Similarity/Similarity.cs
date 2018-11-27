using System.Runtime.Serialization;

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
		[DataMember(Name ="type")]
		string Type { get; }
	}
}
