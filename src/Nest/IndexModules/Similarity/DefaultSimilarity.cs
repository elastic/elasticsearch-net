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
		public string Type => "default";

		public bool? DiscountOverlaps { get; set; }
	}

	[Obsolete("Use ClassicSimilarityDescriptor for TF/IDF model. Removed in NEST 6.x")]
	public class DefaultSimilarityDescriptor
		: DescriptorBase<DefaultSimilarityDescriptor, IDefaultSimilarity>, IDefaultSimilarity
	{
		string ISimilarity.Type => "default";
		bool? IDefaultSimilarity.DiscountOverlaps { get; set; }

		public DefaultSimilarityDescriptor DiscountOverlaps(bool? discount = true) => Assign(a => a.DiscountOverlaps = discount);
	}

}
