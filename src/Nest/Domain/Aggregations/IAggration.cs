using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IAggregation
	{
	}

	public interface IMetricAggregation : IAggregation
	{
	}

	public interface IBucketAggregation : IAggregation
	{
		IDictionary<string, IAggregation> Aggregations { get; }
		//AggregationsHelper Aggs { get; }
	}
	public abstract class BucketAggregationBase : AggregationsHelper , IBucketAggregation
	{
		//public IDictionary<string, IAggregation> Aggregations { get; internal protected set; }
		//private AggregationsHelper _agg = null;

		//public AggregationsHelper Aggs
		//{
		//	get { return _agg ?? (_agg = new AggregationsHelper(this.Aggregations)); }
		//}
	}

	public class ValueMetric : IMetricAggregation
	{
		public double Value { get; set; }
	}

	public class StatsMetric : IMetricAggregation
	{
		public long Count { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double Average { get; set; }
		public double Sum { get; set; }
	}

	public class ExtendedStatsMetric : IMetricAggregation
	{
		public long Count { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double Average { get; set; }
		public double Sum { get; set; }
		public double SumOfSquares { get; set; }
		public double Variance { get; set; }
		public double StdDeviation { get; set; }
	}


	public class SingleBucket : BucketAggregationBase
	{
		public long DocCount { get; set; }
	}

	public class NestedBucket : BucketAggregationBase
	{
	}

	public class Bucket<TBucketItem> : BucketAggregationBase
		where TBucketItem : IBucketItem
	{
		public IList<TBucketItem> Items { get; set; }
	}

	public class Bucket : IAggregation
	{
		public IEnumerable<IAggregation> Items { get; set; }
	}

	public interface IBucketItem : IAggregation
	{
	}

	public class KeyItem : BucketAggregationBase, IBucketItem
	{
		public string Key { get; set; }
		public long DocCount { get; set; }
	}

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

	public class RangeItem : BucketAggregationBase, IBucketItem
	{
		public string Key { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
		public long DocCount { get; set; }
	}

}
