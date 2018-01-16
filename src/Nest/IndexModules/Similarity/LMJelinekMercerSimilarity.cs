using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A similarity that attempts to capture important patterns in the text,
	/// while leaving out noise.
	/// </summary>
	public interface ILMJelinekMercerSimilarity : ISimilarity
	{
		/// <summary>
		/// The lambda parameter
		/// </summary>
		[JsonProperty("lambda")]
		double? Lambda { get; set; }
	}

	/// <inheritdoc />
	public class LMJelinekMercerSimilarity : ILMJelinekMercerSimilarity
	{
		public string Type => "LMJelinekMercer";

		/// <inheritdoc />
		public double? Lambda { get; set; }
	}

	/// <inheritdoc />
	public class LMJelinekMercerSimilarityDescriptor
		: DescriptorBase<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity>, ILMJelinekMercerSimilarity
	{
		string ISimilarity.Type => "LMJelinekMercer";
		double? ILMJelinekMercerSimilarity.Lambda { get; set; }

		/// <inheritdoc />
		public LMJelinekMercerSimilarityDescriptor Lamdba(double? lamda) => Assign(a => a.Lambda = lamda);
	}
}
