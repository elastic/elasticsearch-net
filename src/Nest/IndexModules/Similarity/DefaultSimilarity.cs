using Newtonsoft.Json;

namespace Nest
{
	public interface IDefaultSimilarity : ISimilarity
	{
		[JsonProperty("discount_overlaps")]
		bool? DiscountOverlaps { get; set; }
	}

	public class DefaultSimilarity : IDefaultSimilarity
	{
		public string Type => "default";

		public bool? DiscountOverlaps { get; set; }
	}

	public class DefaultSimilarityDescriptor 
		: DescriptorBase<DefaultSimilarityDescriptor, IDefaultSimilarity>, IDefaultSimilarity
	{
		string ISimilarity.Type => "default";
		bool? IDefaultSimilarity.DiscountOverlaps { get; set; }

		public DefaultSimilarityDescriptor DiscountOverlaps(bool? discount = true) => Assign(a => a.DiscountOverlaps = discount);
	}

}
