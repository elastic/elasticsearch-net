using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class AggregationsHelper
	{
		public IDictionary<string, IAggregate> Aggregations { get; protected internal set; }

		public AggregationsHelper() { }

		public AggregationsHelper(IDictionary<string, IAggregate> aggregations)
		{
			this.Aggregations = aggregations;
		}

		public ValueAggregate Min(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate Max(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate Sum(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate Cardinality(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate Average(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate ValueCount(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate AverageBucket(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate Derivative(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate SumBucket(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate MovingAverage(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate CumulativeSum(string key) => this.TryGet<ValueAggregate>(key);

		public ValueAggregate BucketScript(string key) => this.TryGet<ValueAggregate>(key);

		public KeyedValueAggregate MaxBucket(string key) => this.TryGet<KeyedValueAggregate>(key);

		public KeyedValueAggregate MinBucket(string key) => this.TryGet<KeyedValueAggregate>(key);

		public ScriptedMetricAggregate ScriptedMetric(string key)
		{
			var valueMetric = this.TryGet<ValueAggregate>(key);

			return valueMetric != null
				? new ScriptedMetricAggregate { _Value = valueMetric.Value, Meta = valueMetric.Meta }
				: this.TryGet<ScriptedMetricAggregate>(key);
		}

		public StatsAggregate Stats(string key) => this.TryGet<StatsAggregate>(key);

		public StatsAggregate StatsBucket(string key) => this.TryGet<StatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStats(string key) => this.TryGet<ExtendedStatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStatsBucket(string key) => this.TryGet<ExtendedStatsAggregate>(key);

		public GeoBoundsAggregate GeoBounds(string key) => this.TryGet<GeoBoundsAggregate>(key);

		public PercentilesAggregate Percentiles(string key) => this.TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentilesBucket(string key) => this.TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentileRanks(string key) => this.TryGet<PercentilesAggregate>(key);

		public TopHitsAggregate TopHits(string key) => this.TryGet<TopHitsAggregate>(key);

		public FiltersAggregate Filters(string key)
		{
			var named = this.TryGet<FiltersAggregate>(key);
			if (named != null)
				return named;

			var anonymous = this.TryGet<BucketAggregate>(key);
			return anonymous != null
				? new FiltersAggregate { Buckets = anonymous.Items.OfType<FiltersBucketItem>().ToList(), Meta = anonymous.Meta }
				: null;
		}

		public SingleBucketAggregate Global(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Filter(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Missing(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Nested(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate ReverseNested(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Children(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Sampler(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SignificantTermsAggregate SignificantTerms(string key)
		{
			var bucket = this.TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new SignificantTermsAggregate
				{
					DocCount = bucket.DocCount,
					Buckets = bucket.Items.OfType<SignificantTermsBucket>().ToList(),
					Meta = bucket.Meta
				};
		}

		public TermsAggregate Terms(string key)
		{
			var bucket = this.TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new TermsAggregate
				{
					DocCountErrorUpperBound = bucket.DocCountErrorUpperBound,
					SumOtherDocCount = bucket.SumOtherDocCount,
					Buckets = bucket.Items.OfType<KeyedBucket>().ToList(),
					Meta = bucket.Meta
				};
		}

		public MultiBucketAggregate<HistogramBucket> Histogram(string key)
		{
			var bucket = this.TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new MultiBucketAggregate<HistogramBucket>
				{
					Buckets = bucket.Items.OfType<HistogramBucket>()
						.Concat(bucket.Items.OfType<KeyedBucket>()
							.Select(x =>
								new HistogramBucket
								{
									Key = long.Parse(x.Key),
									KeyAsString = x.Key,
									DocCount = x.DocCount.GetValueOrDefault(0),
									Aggregations = x.Aggregations
								}
							)
						)
						.ToList(),
					Meta = bucket.Meta
				};
		}

		public MultiBucketAggregate<KeyedBucket> GeoHash(string key) => GetBucket<KeyedBucket>(key);

		public MultiBucketAggregate<RangeBucket> Range(string key) => GetBucket<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> DateRange(string key) => GetBucket<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> IpRange(string key) => GetBucket<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> GeoDistance(string key) => GetBucket<RangeBucket>(key);

		public MultiBucketAggregate<DateHistogramBucket> DateHistogram(string key) => GetBucket<DateHistogramBucket>(key);

		private TAggregation TryGet<TAggregation>(string key)
			where TAggregation : class, IAggregate
		{
			IAggregate agg;
			return this.Aggregations.TryGetValue(key, out agg) ? agg as TAggregation : null;
		}

		private MultiBucketAggregate<TBucket> GetBucket<TBucket>(string key)
			where TBucket : IBucket
		{
			var bucket = this.TryGet<BucketAggregate>(key);
			if (bucket == null) return null;
			return new MultiBucketAggregate<TBucket>
			{
				Buckets = bucket.Items.OfType<TBucket>().ToList(),
				Meta = bucket.Meta
			};
		}
	}
}
