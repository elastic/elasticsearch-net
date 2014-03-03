using System;

namespace Nest
{
	public class DateHistogramItem : BucketAggregationBase, IBucketItem
	{
		public long Key { get; set; }
		public string KeyAsString { get; set; }

		public DateTime Date
		{
			get
			{
				return new DateTime(1970, 1, 1).AddMilliseconds(0 + this.Key);
			}
		}

		public long DocCount { get; set; }
	}
}