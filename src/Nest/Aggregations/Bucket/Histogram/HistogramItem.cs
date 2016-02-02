using System;
using System.Collections.Generic;

namespace Nest
{
	public class HistogramItem : BucketItemBase, IBucketItem
	{
		public HistogramItem() { }
		public HistogramItem(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long Key { get; set; }
		public string KeyAsString { get; set; }
		public long DocCount { get; set; }
	}
}