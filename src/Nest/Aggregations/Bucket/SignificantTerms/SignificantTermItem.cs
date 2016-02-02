using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermItem : BucketItemBase, IBucketItem
	{
		public SignificantTermItem() { }
		public SignificantTermItem(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public long BgCount { get; set; }
		public long DocCount { get; set; }
		public double Score { get; set; }
	}
}