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
		public ValueMetric Cardinality(string key)
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

		public ScriptedValueMetric ScriptedMetric(string key)
		{
			var valueMetric = this.TryGet<ValueMetric>(key);

			if (valueMetric != null)
			{
				return new ScriptedValueMetric { _Value = valueMetric.Value };
			}

			return this.TryGet<ScriptedValueMetric>(key);
		}

		public StatsMetric Stats(string key)
		{
			return this.TryGet<StatsMetric>(key);
		}

		public ExtendedStatsMetric ExtendedStats(string key)
		{
			return this.TryGet<ExtendedStatsMetric>(key);
		}

		public GeoBoundsMetric GeoBounds(string key)
		{
			return this.TryGet<GeoBoundsMetric>(key);
		}

		public PercentilesMetric Percentiles(string key)
		{
			return this.TryGet<PercentilesMetric>(key);
		}

		public PercentilesMetric PercentilesRank(string key)
		{
			return this.TryGet<PercentilesMetric>(key);
		}

		public TopHitsMetric TopHitsMetric(string key)
		{
			return this.TryGet<TopHitsMetric>(key);
		}

		public FiltersBucket Filters(string key)
		{
			var named = this.TryGet<FiltersBucket>(key);
			if (named != null)
				return named;

			var anonymous = this.TryGet<Bucket>(key);
			if (anonymous != null)
				return new FiltersBucket(anonymous.Items);

			return null;
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

		public SingleBucket ReverseNested(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public SingleBucket Children(string key)
		{
			return this.TryGet<SingleBucket>(key);
		}

		public BucketWithDocCount<SignificantTermItem> SignificantTerms(string key)
		{
			var bucket = this.TryGet<BucketWithDocCount>(key);
			if (bucket == null)
				return null;
			var b = new BucketWithDocCount<SignificantTermItem>();
			b.DocCount = bucket.DocCount;
			b.Items = bucket.Items.OfType<SignificantTermItem>().ToList();
			return b;
		}

		public Bucket<KeyItem> Terms(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<KeyItem>();
			b.Items = bucket.Items.OfType<KeyItem>().ToList();
			return b;
		}

		public Bucket<HistogramItem> Histogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<HistogramItem>();
			b.Items = bucket.Items.OfType<HistogramItem>()
				.Concat<HistogramItem>(bucket.Items.OfType<KeyItem>()
				.Select(x =>
					new HistogramItem
						{
							Key = long.Parse(x.Key),
							KeyAsString = x.Key,
							DocCount = x.DocCount,
							Aggregations = x.Aggregations
						}
					)
				)
				.ToList();
			return b;
		}

		public Bucket<KeyItem> GeoHash(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<KeyItem>();
			b.Items = bucket.Items.OfType<KeyItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> Range(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> DateRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> IpRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<RangeItem> GeoDistance(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<RangeItem>();
			b.Items = bucket.Items.OfType<RangeItem>().ToList();
			return b;
		}

		public Bucket<HistogramItem> DateHistogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			if (bucket == null)
				return null;
			var b = new Bucket<HistogramItem>();
			b.Items = bucket.Items.OfType<HistogramItem>().ToList();
			return b;
		}
	}
}