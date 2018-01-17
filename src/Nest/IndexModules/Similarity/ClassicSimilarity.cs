using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The classic similarity that is based on the TF/IDF model.
	/// </summary>
	public interface IClassicSimilarity : ISimilarity
	{
		/// <summary>
		/// Determines whether overlap tokens (tokens with 0 position increment) are ignored when computing norm.
		/// By default this is <c>true</c>, meaning overlap tokens do not count when computing norms.
		/// </summary>
		[JsonProperty("discount_overlaps")]
		bool? DiscountOverlaps { get; set; }
	}

	/// <inheritdoc />
	public class ClassicSimilarity : IClassicSimilarity
	{
		public string Type => "classic";

		/// <inheritdoc />
		public bool? DiscountOverlaps { get; set; }
	}

	/// <inheritdoc />
	public class ClassicSimilarityDescriptor
		: DescriptorBase<ClassicSimilarityDescriptor, IClassicSimilarity>, IClassicSimilarity
	{
		string ISimilarity.Type => "classic";
		bool? IClassicSimilarity.DiscountOverlaps { get; set; }

		/// <inheritdoc />
		public ClassicSimilarityDescriptor DiscountOverlaps(bool? discount = true) => Assign(a => a.DiscountOverlaps = discount);
	}
}
