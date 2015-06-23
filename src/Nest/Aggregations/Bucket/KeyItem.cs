using System.Collections.Generic;

namespace Nest
{
	public class KeyItem : BucketAggregationBase, IBucketItem
	{
		public KeyItem() { }
		public KeyItem(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public string KeyAsString { get; set; }
		public long DocCount { get; set; }
	}
}