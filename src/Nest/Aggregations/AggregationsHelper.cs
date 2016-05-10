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

		public ValueAggregate Min(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate Max(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate Sum(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate Cardinality(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate Average(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate ValueCount(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate AverageBucket(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate Derivative(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate SumBucket(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate MovingAverage(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate CumulativeSum(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate BucketScript(string key) => TryGet<ValueAggregate>(key);

		public KeyedValueAggregate MaxBucket(string key) => TryGet<KeyedValueAggregate>(key);

		public KeyedValueAggregate MinBucket(string key) => TryGet<KeyedValueAggregate>(key);

		public ScriptedMetricAggregate ScriptedMetric(string key) => TryGet<ScriptedMetricAggregate>(key);

		public StatsAggregate Stats(string key) => TryGet<StatsAggregate>(key);

		public StatsAggregate StatsBucket(string key) => TryGet<StatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStats(string key) => TryGet<ExtendedStatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStatsBucket(string key) => TryGet<ExtendedStatsAggregate>(key);

		public GeoBoundsAggregate GeoBounds(string key) => TryGet<GeoBoundsAggregate>(key);

		public PercentilesAggregate Percentiles(string key) => TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentilesBucket(string key) => TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentileRanks(string key) => TryGet<PercentilesAggregate>(key);

		public TopHitsAggregate TopHits(string key) => TryGet<TopHitsAggregate>(key);

		public NamedFiltersAggregate NamedFilters(string key) => TryGet<NamedFiltersAggregate>(key);

		public AnonymousFiltersAggregate AnonymousFilters(string key) => TryGet<AnonymousFiltersAggregate>(key);

		public SingleBucketAggregate Global(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Filter(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Missing(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Nested(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate ReverseNested(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Children(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Sampler(string key) => TryGet<SingleBucketAggregate>(key);

		public SignificantTermsAggregate SignificantTerms(string key) => TryGet<SignificantTermsAggregate>(key);

		public TermsAggregate Terms(string key) => TryGet<TermsAggregate>(key);

		public MultiBucketAggregate<HistogramBucket> Histogram(string key) => TryGet<MultiBucketAggregate<HistogramBucket>>(key);

		public MultiBucketAggregate<KeyedBucket> GeoHash(string key) => TryGet<MultiBucketAggregate<KeyedBucket>>(key);

		public MultiBucketAggregate<RangeBucket> Range(string key) => TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> DateRange(string key) => TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> IpRange(string key) => TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<RangeBucket> GeoDistance(string key) => TryGet<MultiBucketAggregate<RangeBucket>>(key);

		public MultiBucketAggregate<DateHistogramBucket> DateHistogram(string key) => TryGet<MultiBucketAggregate<DateHistogramBucket>>(key);

		private TAggregation TryGet<TAggregation>(string key)
			where TAggregation : class, IAggregate
		{
			IAggregate agg;
			return this.Aggregations.TryGetValue(key, out agg) ? agg as TAggregation : null;
		}
	}
}
