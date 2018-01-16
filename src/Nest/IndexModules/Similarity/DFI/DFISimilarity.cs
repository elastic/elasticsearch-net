using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Similarity that implements the divergence from independence model
	/// </summary>
	public interface IDFISimilarity : ISimilarity
	{
		/// <summary>
		/// The independence measure
		/// </summary>
		[JsonProperty("independence_measure")]
		DFIIndependenceMeasure? IndependenceMeasure { get; set; }
	}

	/// <inheritdoc/>
	public class DFISimilarity : IDFISimilarity
	{
		public string Type => "DFI";

		/// <inheritdoc/>
		public DFIIndependenceMeasure? IndependenceMeasure { get; set; }
	}

	/// <inheritdoc/>
	public class DFISimilarityDescriptor
		: DescriptorBase<DFISimilarityDescriptor, IDFISimilarity>, IDFISimilarity
	{
		string ISimilarity.Type => "DFI";
		DFIIndependenceMeasure? IDFISimilarity.IndependenceMeasure { get; set; }

		/// <inheritdoc/>
		public DFISimilarityDescriptor IndependenceMeasure(DFIIndependenceMeasure? independenceMeasure) =>
			Assign(a => a.IndependenceMeasure = independenceMeasure);
	}
}
