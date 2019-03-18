using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// Contains aggregates that are returned by Elasticsearch. In NEST `Aggregation` always refers to an aggregation
	/// going to elasticsearch and an `Aggregate` describes an aggregation going out.
	/// </summary>
	[ContractJsonConverter(typeof(AggregateDictionaryConverter))]
	public class AggregateDictionary : IsAReadOnlyDictionaryBase<string, IAggregate>
	{
		internal static readonly char[] TypedKeysSeparator = { '#' };

		public AggregateDictionary(IReadOnlyDictionary<string, IAggregate> backingDictionary) : base(backingDictionary) { }

		public static AggregateDictionary Default { get; } = new AggregateDictionary(EmptyReadOnly<string, IAggregate>.Dictionary);

		protected override string Sanitize(string key)
		{
			//typed_keys = true on results in aggregation keys being returned as "<type>#<name>"
			var tokens = TypedKeyTokens(key);
			return tokens.Length > 1 ? tokens[1] : tokens[0];
		}

		internal static string[] TypedKeyTokens(string key)
		{
			var tokens = key.Split(TypedKeysSeparator, 2, StringSplitOptions.RemoveEmptyEntries);
			return tokens;
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

		public ValueAggregate SerialDifferencing(string key) => TryGet<ValueAggregate>(key);

		public ValueAggregate WeightedAverage(string key) => TryGet<ValueAggregate>(key);

		public KeyedValueAggregate MaxBucket(string key) => TryGet<KeyedValueAggregate>(key);

		public KeyedValueAggregate MinBucket(string key) => TryGet<KeyedValueAggregate>(key);

		public ScriptedMetricAggregate ScriptedMetric(string key)
		{
			var valueMetric = TryGet<ValueAggregate>(key);

			return valueMetric != null
				? new ScriptedMetricAggregate(valueMetric.Value) { Meta = valueMetric.Meta }
				: TryGet<ScriptedMetricAggregate>(key);
		}

		public StatsAggregate Stats(string key) => TryGet<StatsAggregate>(key);

		public StatsAggregate StatsBucket(string key) => TryGet<StatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStats(string key) => TryGet<ExtendedStatsAggregate>(key);

		public ExtendedStatsAggregate ExtendedStatsBucket(string key) => TryGet<ExtendedStatsAggregate>(key);

		public GeoBoundsAggregate GeoBounds(string key) => TryGet<GeoBoundsAggregate>(key);

		public PercentilesAggregate Percentiles(string key) => TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentilesBucket(string key) => TryGet<PercentilesAggregate>(key);

		public PercentilesAggregate PercentileRanks(string key) => TryGet<PercentilesAggregate>(key);

		public TopHitsAggregate TopHits(string key) => TryGet<TopHitsAggregate>(key);

		public FiltersAggregate Filters(string key)
		{
			var named = TryGet<FiltersAggregate>(key);
			if (named != null)
				return named;

			var anonymous = TryGet<BucketAggregate>(key);
			return anonymous != null
				? new FiltersAggregate { Buckets = anonymous.Items.OfType<FiltersBucketItem>().ToList(), Meta = anonymous.Meta }
				: null;
		}

		public SingleBucketAggregate Global(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Filter(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Missing(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Nested(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate ReverseNested(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Children(string key) => TryGet<SingleBucketAggregate>(key);

		public SingleBucketAggregate Sampler(string key) => TryGet<SingleBucketAggregate>(key);

		public GeoCentroidAggregate GeoCentroid(string key) => TryGet<GeoCentroidAggregate>(key);

		public SignificantTermsAggregate SignificantTerms(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new SignificantTermsAggregate
				{
					BgCount = bucket.BgCount,
					DocCount = bucket.DocCount,
					Buckets = bucket.Items.OfType<SignificantTermsBucket>().ToList(),
					Meta = bucket.Meta
				};
		}

		public SignificantTermsAggregate SignificantText(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new SignificantTermsAggregate
				{
					BgCount = bucket.BgCount,
					DocCount = bucket.DocCount,
					Buckets = bucket.Items.OfType<SignificantTermsBucket>().ToList(),
					Meta = bucket.Meta
				};
		}

		public TermsAggregate<TKey> Terms<TKey>(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			return bucket == null
				? null
				: new TermsAggregate<TKey>
				{
					DocCountErrorUpperBound = bucket.DocCountErrorUpperBound,
					SumOtherDocCount = bucket.SumOtherDocCount,
					Buckets = GetKeyedBuckets<TKey>(bucket.Items).ToList(),
					Meta = bucket.Meta
				};
		}

		public TermsAggregate<string> Terms(string key) => Terms<string>(key);

		public MultiBucketAggregate<KeyedBucket<double>> Histogram(string key) => GetMultiKeyedBucketAggregate<double>(key);

		public MultiBucketAggregate<KeyedBucket<string>> GeoHash(string key) => GetMultiKeyedBucketAggregate<string>(key);

		public MultiBucketAggregate<KeyedBucket<string>> AdjacencyMatrix(string key) => GetMultiKeyedBucketAggregate<string>(key);

		public MultiBucketAggregate<RangeBucket> Range(string key) => GetMultiBucketAggregate<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> DateRange(string key) => GetMultiBucketAggregate<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> IpRange(string key) => GetMultiBucketAggregate<RangeBucket>(key);

		public MultiBucketAggregate<RangeBucket> GeoDistance(string key) => GetMultiBucketAggregate<RangeBucket>(key);

		public MultiBucketAggregate<DateHistogramBucket> DateHistogram(string key) => GetMultiBucketAggregate<DateHistogramBucket>(key);

		public AutoDateHistogramAggregate AutoDateHistogram(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			if (bucket == null) return null;

			return new AutoDateHistogramAggregate
			{
				Buckets = bucket.Items.OfType<DateHistogramBucket>().ToList(),
				Meta = bucket.Meta,
				Interval = bucket.Interval
			};
		}


		public CompositeBucketAggregate Composite(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			if (bucket == null) return null;

			return new CompositeBucketAggregate
			{
				Buckets = bucket.Items.OfType<CompositeBucket>().ToList(),
				Meta = bucket.Meta,
				AfterKey = new CompositeKey(bucket.AfterKey)
			};
		}

		public MatrixStatsAggregate MatrixStats(string key) => TryGet<MatrixStatsAggregate>(key);

		private TAggregate TryGet<TAggregate>(string key)
			where TAggregate : class, IAggregate
		{
			IAggregate agg;
			return BackingDictionary.TryGetValue(key, out agg) ? agg as TAggregate : null;
		}

		private MultiBucketAggregate<TBucket> GetMultiBucketAggregate<TBucket>(string key)
			where TBucket : IBucket
		{
			var bucket = TryGet<BucketAggregate>(key);
			if (bucket == null) return null;

			return new MultiBucketAggregate<TBucket>
			{
				Buckets = bucket.Items.OfType<TBucket>().ToList(),
				Meta = bucket.Meta,
			};
		}

		private MultiBucketAggregate<KeyedBucket<TKey>> GetMultiKeyedBucketAggregate<TKey>(string key)
		{
			var bucket = TryGet<BucketAggregate>(key);
			if (bucket == null) return null;

			return new MultiBucketAggregate<KeyedBucket<TKey>>
			{
				Buckets = GetKeyedBuckets<TKey>(bucket.Items).ToList(),
				Meta = bucket.Meta,
			};
		}


		private IEnumerable<KeyedBucket<TKey>> GetKeyedBuckets<TKey>(IEnumerable<IBucket> items)
		{
			var buckets = items.Cast<KeyedBucket<object>>();

			foreach (var bucket in buckets)
				yield return new KeyedBucket<TKey>(bucket.BackingDictionary)
				{
					Key = (TKey)Convert.ChangeType(bucket.Key, typeof(TKey)),
					KeyAsString = bucket.KeyAsString,
					DocCount = bucket.DocCount,
					DocCountErrorUpperBound = bucket.DocCountErrorUpperBound
				};
		}
	}
}
