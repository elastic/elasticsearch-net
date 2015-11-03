using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class AggregationsHelper
	{
		public IDictionary<string, IAggregation> Aggregations { get; internal protected set; }

		public AggregationsHelper() { }

		public AggregationsHelper(IDictionary<string, IAggregation> aggregations)
		{
			this.Aggregations = aggregations;
		}

		private TAggregation TryGet<TAggregation>(string key)
			where TAggregation : class, IAggregation
		{
			IAggregation agg;
			return this.Aggregations.TryGetValue(key, out agg) 
				? agg as TAggregation 
				: null;
		}

		public ValueMetric Min(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric Max(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric Sum(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric Cardinality(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric Average(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric ValueCount(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric AverageBucket(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric Derivative(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric SumBucket(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric MovingAverage(string key) => this.TryGet<ValueMetric>(key);

		public ValueMetric CumulativeSum(string key) => this.TryGet<ValueMetric>(key);

		public KeyedValueMetric MaxBucket(string key) => this.TryGet<KeyedValueMetric>(key);

		public KeyedValueMetric MinBucket(string key) => this.TryGet<KeyedValueMetric>(key);

		public ScriptedValueMetric ScriptedMetric(string key)
		{
			var valueMetric = this.TryGet<ValueMetric>(key);

			return valueMetric != null 
				? new ScriptedValueMetric { _Value = valueMetric.Value } 
				: this.TryGet<ScriptedValueMetric>(key);
		}

		public StatsMetric Stats(string key) => this.TryGet<StatsMetric>(key);

		public ExtendedStatsMetric ExtendedStats(string key) => this.TryGet<ExtendedStatsMetric>(key);

		public GeoBoundsMetric GeoBounds(string key) => this.TryGet<GeoBoundsMetric>(key);

		public PercentilesMetric Percentiles(string key) => this.TryGet<PercentilesMetric>(key);

		public PercentilesMetric PercentilesRank(string key) => this.TryGet<PercentilesMetric>(key);

		public TopHitsMetric TopHits(string key) => this.TryGet<TopHitsMetric>(key);

		public FiltersBucket Filters(string key)
		{
			var named = this.TryGet<FiltersBucket>(key);
			if (named != null)
				return named;

			var anonymous = this.TryGet<Bucket>(key);
			return anonymous != null ? new FiltersBucket(anonymous.Items) : null;
		}

		public SingleBucket Global(string key) => this.TryGet<SingleBucket>(key);

		public SingleBucket Filter(string key) => this.TryGet<SingleBucket>(key);

		public SingleBucket Missing(string key) => this.TryGet<SingleBucket>(key);

		public SingleBucket Nested(string key) => this.TryGet<SingleBucket>(key);

		public SingleBucket ReverseNested(string key) => this.TryGet<SingleBucket>(key);

		public SingleBucket Children(string key) => this.TryGet<SingleBucket>(key);

		public DocCountBucket<SignificantTermItem> SignificantTerms(string key)
		{
			var bucket = this.TryGet<DocCountBucket>(key);
			return bucket == null
				? null
				: new DocCountBucket<SignificantTermItem>
				{
					DocCount = bucket.DocCount,
					Items = bucket.Items.OfType<SignificantTermItem>().ToList()
				};
		}

		public Bucket<KeyedBucket> Terms(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<KeyedBucket> {Items = bucket.Items.OfType<KeyedBucket>().ToList()};
		}

		public Bucket<HistogramItem> Histogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null
				? null
				: new Bucket<HistogramItem>
				{
					Items = bucket.Items.OfType<HistogramItem>()
						.Concat<HistogramItem>(bucket.Items.OfType<KeyedBucket>()
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
						.ToList()
				};
		}

		public Bucket<KeyedBucket> GeoHash(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<KeyedBucket> {Items = bucket.Items.OfType<KeyedBucket>().ToList()};
		}

		public Bucket<RangeItem> Range(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<RangeItem> {Items = bucket.Items.OfType<RangeItem>().ToList()};
		}

		public Bucket<RangeItem> DateRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<RangeItem> {Items = bucket.Items.OfType<RangeItem>().ToList()};
		}

		public Bucket<RangeItem> IpRange(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<RangeItem> {Items = bucket.Items.OfType<RangeItem>().ToList()};
		}

		public Bucket<RangeItem> GeoDistance(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<RangeItem> {Items = bucket.Items.OfType<RangeItem>().ToList()};
		}

		public Bucket<HistogramItem> DateHistogram(string key)
		{
			var bucket = this.TryGet<Bucket>(key);
			return bucket == null 
				? null 
				: new Bucket<HistogramItem> {Items = bucket.Items.OfType<HistogramItem>().ToList()};
		}
	}
}