using System;
using System.Collections.Generic;

namespace Nest
{
	public class HistogramItem : BucketAggregationBase, IBucketItem
	{
		public HistogramItem() { }
		public HistogramItem(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public long Key { get; set; }
		public string KeyAsString { get; set; }

		/// <summary>
		/// Get a DateTime form of the returned key, only make sense on date_histogram aggregations.
		/// </summary>
		public DateTime Date => new DateTime(1970, 1, 1).AddMilliseconds(0 + this.Key);

		public long DocCount { get; set; }
	}

	public class ExtendedBounds<T>
	{
		public T Minimum { get; set; }

		public T Maximum { get; set; }
	}
}