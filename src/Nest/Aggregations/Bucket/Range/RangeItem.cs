using System.Collections.Generic;

namespace Nest
{
	public class RangeItem : BucketBase, IBucketItem
	{
		public RangeItem() { }
		public RangeItem(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
		public long DocCount { get; set; }
	}
}