using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class AggregationsHelper
	{
		public IDictionary<string, IAggregation> Aggregations { get; internal protected set; }

		public AggregationsHelper()
		{
			
		}

		public AggregationsHelper(IDictionary<string, IAggregation> aggregations)
		{
			this.Aggregations = aggregations;
		}

		private TAggregation TryGet<TAggregation>(string key)
			where TAggregation : class, IAggregation 
		{
			IAggregation agg;
			if (this.Aggregations.TryGetValue(key, out agg))
				return agg as TAggregation;
			return null;
		
		}

		public ValueMetric Min(string key)
		{
			return this.TryGet<ValueMetric>(key);
		}

		public ValueMetric Max(string key)
		{
			return this.TryGet<ValueMetric>(key);
		}

		public ValueMetric Sum(string key)
		{
			return this.TryGet<ValueMetric>(key);
		}

		public ValueMetric Average(string key)
		{
			return this.TryGet<ValueMetric>(key);
		}

		public ValueMetric ValueCount(string key)
		{
			return this.TryGet<ValueMetric>(key);
		}

		public StatsMetric Stats(string key)
		{
			return this.TryGet<StatsMetric>(key);
		}

		public ExtendedStatsMetric ExtendedStats(string key)
		{
			return this.TryGet<ExtendedStatsMetric>(key);
		}

		public SingleBucket Global(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public SingleBucket Filter(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public SingleBucket Missing(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public SingleBucket Nested(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public Bucket<KeyItem> Terms(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<KeyItem>();
			b.Items = bucket.Items.OfType<KeyItem>().ToList();
			return b;
		}

		public Bucket<KeyItem> Histogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<KeyItem>();
			b.Items = bucket.Items.OfType<KeyItem>().ToList();
			return b;
		}

		public Bucket<KeyItem> GeoHash(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<KeyItem>();
			b.Items = bucket.Items.OfType<KeyItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> Range(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> DateRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> IpRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> GeoDistance(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<DateHistogramItem> DateHistogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			var b = new Bucket<DateHistogramItem>();
			b.Items = bucket.Items.OfType<DateHistogramItem>().ToList();
			return b;
		}

		


	}
}