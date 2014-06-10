using System;

namespace Nest
{
	public class HistogramItem : BucketAggregationBase, IBucketItem
	{
		public long Key { get; set; }
		public string KeyAsString { get; set; }

		/// <summary>
		/// Get a DateTime form of the returned key, only make sense on date_histogram aggregations.
		/// </summary>
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