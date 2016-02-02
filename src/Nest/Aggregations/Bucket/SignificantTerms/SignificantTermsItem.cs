using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsItem : BucketItemBase, IBucketItem
	{
		public SignificantTermsItem() { }
		public SignificantTermsItem(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public long BgCount { get; set; }
		public long DocCount { get; set; }
		public double Score { get; set; }
	}
}