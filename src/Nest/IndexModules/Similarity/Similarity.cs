using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A similarity.
	/// </summary>
	[JsonFormatter(typeof(SimilarityFormatter))]
	public interface ISimilarity
	{
		/// <summary>
		/// The type of similarity.
		/// </summary>
		[DataMember(Name ="type")]
		string Type { get; }
	}
}
