using System;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Use IClassicSimilarity for TF/IDF model. Removed in NEST 6.x")]
	public interface IDefaultSimilarity : ISimilarity
	{
		[JsonProperty("discount_overlaps")]
		bool? DiscountOverlaps { get; set; }
	}

	[Obsolete("Use ClassicSimilarity for TF/IDF model. Removed in NEST 6.x")]
	public class DefaultSimilarity : IDefaultSimilarity
	{
		public bool? DiscountOverlaps { get; set; }
		public string Type => "default";
	}

	[Obsolete("Use ClassicSimilarityDescriptor for TF/IDF model. Removed in NEST 6.x")]
	public class DefaultSimilarityDescriptor
		: DescriptorBase<DefaultSimilarityDescriptor, IDefaultSimilarity>, IDefaultSimilarity
	{
		bool? IDefaultSimilarity.DiscountOverlaps { get; set; }
		string ISimilarity.Type => "default";

		public DefaultSimilarityDescriptor DiscountOverlaps(bool? discount = true) => Assign(a => a.DiscountOverlaps = discount);
	}
}
