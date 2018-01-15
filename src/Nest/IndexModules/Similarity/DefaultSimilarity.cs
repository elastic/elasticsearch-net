using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The default similarity that is based on the TF/IDF model.
	/// </summary>
	public interface IDefaultSimilarity : ISimilarity
	{
		/// <summary>
		/// Determines whether overlap tokens (tokens with 0 position increment) are ignored when computing norm.
		/// By default this is <c>true</c>, meaning overlap tokens do not count when computing norms.
		/// </summary>
		[JsonProperty("discount_overlaps")]
		bool? DiscountOverlaps { get; set; }
	}

	/// <inheritdoc />
	public class DefaultSimilarity : IDefaultSimilarity
	{
		public string Type => "default";

		/// <inheritdoc />
		public bool? DiscountOverlaps { get; set; }
	}

	/// <inheritdoc />
	public class DefaultSimilarityDescriptor
		: DescriptorBase<DefaultSimilarityDescriptor, IDefaultSimilarity>, IDefaultSimilarity
	{
		string ISimilarity.Type => "default";
		bool? IDefaultSimilarity.DiscountOverlaps { get; set; }

		/// <inheritdoc />
		public DefaultSimilarityDescriptor DiscountOverlaps(bool? discount = true) => Assign(a => a.DiscountOverlaps = discount);
	}
}
