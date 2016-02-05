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

		public ScriptedMetricAggregate ScriptedMetric(string key) => this.TryGet<ScriptedMetricAggregate>(key);

		public StatsAggregate Stats(string key) => this.TryGet<StatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStats(string key) => this.TryGet<ExtendedStatsAggregate>(key);

		public GeoBoundsAggregate GeoBounds(string key) => this.TryGet<GeoBoundsAggregate>(key);

		public PercentilesAggregate Percentiles(string key) => this.TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentileRanks(string key) => this.TryGet<PercentilesAggregate>(key);

		public TopHitsAggregate TopHits(string key) => this.TryGet<TopHitsAggregate>(key);

		public FiltersAggregate Filters(string key) => this.TryGet<FiltersAggregate>(key);

		public SingleBucketAggregate Global(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Filter(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Missing(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Nested(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate ReverseNested(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Children(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Sampler(string key) => this.TryGet<SingleBucketAggregate>(key);

		public SignificantTermsAggregate SignificantTerms(string key) => this.TryGet<SignificantTermsAggregate>(key);

		public TermsAggregate Terms(string key) => this.TryGet<TermsAggregate>(key);

		public MultiBucketAggregate<HistogramBucket> Histogram(string key) => this.TryGet<MultiBucketAggregate<HistogramBucket>>(key);

		public MultiBucketAggregate<KeyedBucket> GeoHash(string key) => this.TryGet<MultiBucketAggregate<KeyedBucket>>(key);

		public MultiBucketAggregate<RangeBucket> Range(string key) => this.TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> DateRange(string key) => this.TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> IpRange(string key) => this.TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> GeoDistance(string key) => this.TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<DateHistogramBucket> DateHistogram(string key) => this.TryGet<MultiBucketAggregate<DateHistogramBucket>>(key);

		private TAggregation TryGet<TAggregation>(string key)
			where TAggregation : class, IAggregate
		{
			IAggregate agg;
			return this.Aggregations.TryGetValue(key, out agg) ? agg as TAggregation : null;
		}
	}
}