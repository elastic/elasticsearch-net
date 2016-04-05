using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
	public class AggregationDictionary : IsADictionaryBase<string, IAggregationContainer>
	{
		public AggregationDictionary() : base() { }
		public AggregationDictionary(IDictionary<string, IAggregationContainer> container) : base(container) { }
		public AggregationDictionary(Dictionary<string, AggregationContainer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => (IAggregationContainer)kv.Value))
		{ }

		public static implicit operator AggregationDictionary(Dictionary<string, IAggregationContainer> container) =>
			new AggregationDictionary(container);

		public static implicit operator AggregationDictionary(Dictionary<string, AggregationContainer> container) =>
			new AggregationDictionary(container);

		public static implicit operator AggregationDictionary(AggregationBase aggregator)
		{
			IAggregation b;
			var combinator = aggregator as AggregationCombinator;
			if (combinator != null)
			{
				var dict = new Dictionary<string, AggregationContainer>();
				foreach (var agg in combinator.Aggregations)
				{
					b = agg;
					if (b.Name.IsNullOrEmpty())
						throw new ArgumentException($"{aggregator.GetType().Name} .Name is not set!");
					dict.Add(b.Name, agg);
				}
				return dict;
			}

			b = aggregator;
			if (b.Name.IsNullOrEmpty())
				throw new ArgumentException($"{aggregator.GetType().Name} .Name is not set!");
			return new Dictionary<string, AggregationContainer> { { b.Name, aggregator } };
		}
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AggregationContainer>))]
	public interface IAggregationContainer
	{
		[JsonProperty("meta")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, object> Meta { get; set; }

		[JsonProperty("avg")]
		IAverageAggregation Average { get; set; }

		[JsonProperty("date_histogram")]
		IDateHistogramAggregation DateHistogram { get; set; }

		[JsonProperty("percentiles")]
		IPercentilesAggregation Percentiles { get; set; }

		[JsonProperty("date_range")]
		IDateRangeAggregation DateRange { get; set; }

		[JsonProperty("extended_stats")]
		IExtendedStatsAggregation ExtendedStats { get; set; }

		[JsonProperty("filter")]
		IFilterAggregation Filter { get; set; }

		[JsonProperty("filters")]
		IFiltersAggregation Filters { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceAggregation GeoDistance { get; set; }

		[JsonProperty("geohash_grid")]
		IGeoHashGridAggregation GeoHash { get; set; }

		[JsonProperty("geo_bounds")]
		IGeoBoundsAggregation GeoBounds { get; set; }

		[JsonProperty("histogram")]
		IHistogramAggregation Histogram { get; set; }

		[JsonProperty("global")]
		IGlobalAggregation Global { get; set; }

		[JsonProperty("ip_range")]
		IIpRangeAggregation IpRange { get; set; }

		[JsonProperty("max")]
		IMaxAggregation Max { get; set; }

		[JsonProperty("min")]
		IMinAggregation Min { get; set; }

		[JsonProperty("cardinality")]
		ICardinalityAggregation Cardinality { get; set; }

		[JsonProperty("missing")]
		IMissingAggregation Missing { get; set; }

		[JsonProperty("nested")]
		INestedAggregation Nested { get; set; }

		[JsonProperty("reverse_nested")]
		IReverseNestedAggregation ReverseNested { get; set; }

		[JsonProperty("range")]
		IRangeAggregation Range { get; set; }

		[JsonProperty("stats")]
		IStatsAggregator Stats { get; set; }

		[JsonProperty("sum")]
		ISumAggregation Sum { get; set; }

		[JsonProperty("terms")]
		ITermsAggregation Terms { get; set; }

		[JsonProperty("significant_terms")]
		ISignificantTermsAggregation SignificantTerms { get; set; }

		[JsonProperty("value_count")]
		IValueCountAggregation ValueCount { get; set; }

		[JsonProperty("percentile_ranks")]
		IPercentileRanksAggregation PercentileRanks { get; set; }

		[JsonProperty("top_hits")]
		ITopHitsAggregation TopHits { get; set; }

		[JsonProperty("children")]
		IChildrenAggregation Children { get; set; }

		[JsonProperty("scripted_metric")]
		IScriptedMetricAggregation ScriptedMetric { get; set; }

		[JsonProperty("avg_bucket")]
		IAverageBucketAggregation AverageBucket { get; set; }

		[JsonProperty("derivative")]
		IDerivativeAggregation Derivative { get; set; }

		[JsonProperty("max_bucket")]
		IMaxBucketAggregation MaxBucket { get; set; }

		[JsonProperty("min_bucket")]
		IMinBucketAggregation MinBucket { get; set; }

		[JsonProperty("sum_bucket")]
		ISumBucketAggregation SumBucket { get; set; }

		[JsonProperty("stats_bucket")]
		IStatsBucketAggregation StatsBucket { get; set; }

		[JsonProperty("extended_stats_bucket")]
		IExtendedStatsBucketAggregation ExtendedStatsBucket { get; set; }

		[JsonProperty("percentiles_bucket")]
		IPercentilesBucketAggregation PercentilesBucket { get; set; }

		[JsonProperty("moving_avg")]
		IMovingAverageAggregation MovingAverage { get; set; }

		[JsonProperty("cumulative_sum")]
		ICumulativeSumAggregation CumulativeSum { get; set; }

		[JsonProperty("serial_diff")]
		ISerialDifferencingAggregation SerialDifferencing { get; set; }

		[JsonProperty("bucket_script")]
		IBucketScriptAggregation BucketScript { get; set; }

		[JsonProperty("bucket_selector")]
		IBucketSelectorAggregation BucketSelector { get; set; }

		[JsonProperty("sampler")]
		ISamplerAggregation Sampler { get; set; }

		[JsonProperty("aggs")]
		AggregationDictionary Aggregations { get; set; }

		void Accept(IAggregationVisitor visitor);
	}

	public class AggregationContainer : IAggregationContainer
	{
		public IDictionary<string, object> Meta { get; set; }
		public IAverageAggregation Average { get; set; }
		public IValueCountAggregation ValueCount { get; set; }
		public IMaxAggregation Max { get; set; }
		public IMinAggregation Min { get; set; }
		public IStatsAggregator Stats { get; set; }
		public ISumAggregation Sum { get; set; }
		public IExtendedStatsAggregation ExtendedStats { get; set; }
		public IDateHistogramAggregation DateHistogram { get; set; }

		public IPercentilesAggregation Percentiles { get; set; }

		public IDateRangeAggregation DateRange { get; set; }

		public IFilterAggregation Filter { get; set; }

		public IFiltersAggregation Filters { get; set; }

		public IGeoDistanceAggregation GeoDistance { get; set; }

		public IGeoHashGridAggregation GeoHash { get; set; }

		public IGeoBoundsAggregation GeoBounds { get; set; }

		public IHistogramAggregation Histogram { get; set; }

		public IGlobalAggregation Global { get; set; }

		public IIpRangeAggregation IpRange { get; set; }

		public ICardinalityAggregation Cardinality { get; set; }

		public IMissingAggregation Missing { get; set; }

		public INestedAggregation Nested { get; set; }

		public IReverseNestedAggregation ReverseNested { get; set; }

		public IRangeAggregation Range { get; set; }

		public ITermsAggregation Terms { get; set; }

		public ISignificantTermsAggregation SignificantTerms { get; set; }

		public IPercentileRanksAggregation PercentileRanks { get; set; }

		public ITopHitsAggregation TopHits { get; set; }

		public IChildrenAggregation Children { get; set; }

		public IScriptedMetricAggregation ScriptedMetric { get; set; }

		public IAverageBucketAggregation AverageBucket { get; set; }

		public IDerivativeAggregation Derivative { get; set; }

		public IMaxBucketAggregation MaxBucket { get; set; }

		public IMinBucketAggregation MinBucket { get; set; }

		public ISumBucketAggregation SumBucket { get; set; }

		public IStatsBucketAggregation StatsBucket { get; set; }

		public IExtendedStatsBucketAggregation ExtendedStatsBucket { get; set; }

		public IPercentilesBucketAggregation PercentilesBucket { get; set; }

		public IMovingAverageAggregation MovingAverage { get; set; }

		public ICumulativeSumAggregation CumulativeSum { get; set; }

		public ISerialDifferencingAggregation SerialDifferencing { get; set; }

		public IBucketScriptAggregation BucketScript { get; set; }

		public IBucketSelectorAggregation BucketSelector { get; set; }

		public ISamplerAggregation Sampler { get; set; }

		public AggregationDictionary Aggregations { get; set; }

		public static implicit operator AggregationContainer(AggregationBase aggregator)
		{
			if (aggregator == null) return null;
			var container = new AggregationContainer();
			aggregator.WrapInContainer(container);
			var bucket = aggregator as BucketAggregationBase;
			container.Aggregations = bucket?.Aggregations;
			container.Meta = aggregator.Meta;
			return container;
		}

		public void Accept(IAggregationVisitor visitor)
		{
			if (visitor.Scope == AggregationVisitorScope.Unknown) visitor.Scope = AggregationVisitorScope.Aggregation;
			new AggregationWalker().Walk(this, visitor);
		}
	}

	public class AggregationContainerDescriptor<T> : DescriptorBase<AggregationContainerDescriptor<T>, IAggregationContainer>, IAggregationContainer
		where T : class
	{
		IDictionary<string, object> IAggregationContainer.Meta { get; set; }

		AggregationDictionary IAggregationContainer.Aggregations { get; set; }

		IAverageAggregation IAggregationContainer.Average { get; set; }

		IDateHistogramAggregation IAggregationContainer.DateHistogram { get; set; }

		IPercentilesAggregation IAggregationContainer.Percentiles { get; set; }

		IDateRangeAggregation IAggregationContainer.DateRange { get; set; }

		IExtendedStatsAggregation IAggregationContainer.ExtendedStats { get; set; }

		IFilterAggregation IAggregationContainer.Filter { get; set; }

		IFiltersAggregation IAggregationContainer.Filters { get; set; }

		IGeoDistanceAggregation IAggregationContainer.GeoDistance { get; set; }

		IGeoHashGridAggregation IAggregationContainer.GeoHash { get; set; }

		IGeoBoundsAggregation IAggregationContainer.GeoBounds { get; set; }

		IHistogramAggregation IAggregationContainer.Histogram { get; set; }

		IGlobalAggregation IAggregationContainer.Global { get; set; }

		IIpRangeAggregation IAggregationContainer.IpRange { get; set; }

		IMaxAggregation IAggregationContainer.Max { get; set; }

		IMinAggregation IAggregationContainer.Min { get; set; }

		ICardinalityAggregation IAggregationContainer.Cardinality { get; set; }

		IMissingAggregation IAggregationContainer.Missing { get; set; }

		INestedAggregation IAggregationContainer.Nested { get; set; }

		IReverseNestedAggregation IAggregationContainer.ReverseNested { get; set; }

		IRangeAggregation IAggregationContainer.Range { get; set; }

		IStatsAggregator IAggregationContainer.Stats { get; set; }

		ISumAggregation IAggregationContainer.Sum { get; set; }

		IValueCountAggregation IAggregationContainer.ValueCount { get; set; }

		ISignificantTermsAggregation IAggregationContainer.SignificantTerms { get; set; }

		IPercentileRanksAggregation IAggregationContainer.PercentileRanks { get; set; }

		ITermsAggregation IAggregationContainer.Terms { get; set; }

		ITopHitsAggregation IAggregationContainer.TopHits { get; set; }

		IChildrenAggregation IAggregationContainer.Children { get; set; }

		IScriptedMetricAggregation IAggregationContainer.ScriptedMetric { get; set; }

		IAverageBucketAggregation IAggregationContainer.AverageBucket { get; set; }

		IDerivativeAggregation IAggregationContainer.Derivative { get; set; }

		IMaxBucketAggregation IAggregationContainer.MaxBucket { get; set; }

		IMinBucketAggregation IAggregationContainer.MinBucket { get; set; }

		ISumBucketAggregation IAggregationContainer.SumBucket { get; set; }

		IStatsBucketAggregation IAggregationContainer.StatsBucket { get; set; }

		IExtendedStatsBucketAggregation IAggregationContainer.ExtendedStatsBucket { get; set; }

		IPercentilesBucketAggregation IAggregationContainer.PercentilesBucket { get; set; }

		IMovingAverageAggregation IAggregationContainer.MovingAverage { get; set; }

		ICumulativeSumAggregation IAggregationContainer.CumulativeSum { get; set; }

		ISerialDifferencingAggregation IAggregationContainer.SerialDifferencing { get; set; }

		IBucketScriptAggregation IAggregationContainer.BucketScript { get; set; }

		IBucketSelectorAggregation IAggregationContainer.BucketSelector { get; set; }

		ISamplerAggregation IAggregationContainer.Sampler { get; set; }

		public AggregationContainerDescriptor<T> Average(string name,
			Func<AverageAggregationDescriptor<T>, IAverageAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Average = d);

		public AggregationContainerDescriptor<T> DateHistogram(string name,
			Func<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.DateHistogram = d);

		public AggregationContainerDescriptor<T> Percentiles(string name,
			Func<PercentilesAggregationDescriptor<T>, IPercentilesAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Percentiles = d);

		public AggregationContainerDescriptor<T> PercentileRanks(string name,
			Func<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.PercentileRanks = d);

		public AggregationContainerDescriptor<T> DateRange(string name,
			Func<DateRangeAggregationDescriptor<T>, IDateRangeAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.DateRange = d);

		public AggregationContainerDescriptor<T> ExtendedStats(string name,
			Func<ExtendedStatsAggregationDescriptor<T>, IExtendedStatsAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ExtendedStats = d);

		public AggregationContainerDescriptor<T> Filter(string name,
			Func<FilterAggregationDescriptor<T>, IFilterAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Filter = d);

		public AggregationContainerDescriptor<T> Filters(string name,
			Func<FiltersAggregationDescriptor<T>, IFiltersAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Filters = d);

		public AggregationContainerDescriptor<T> GeoDistance(string name,
			Func<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoDistance = d);

		public AggregationContainerDescriptor<T> GeoHash(string name,
			Func<GeoHashGridAggregationDescriptor<T>, IGeoHashGridAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoHash = d);

		public AggregationContainerDescriptor<T> GeoBounds(string name,
			Func<GeoBoundsAggregationDescriptor<T>, IGeoBoundsAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoBounds = d);

		public AggregationContainerDescriptor<T> Histogram(string name,
			Func<HistogramAggregationDescriptor<T>, IHistogramAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Histogram = d);

		public AggregationContainerDescriptor<T> Global(string name,
			Func<GlobalAggregationDescriptor<T>, IGlobalAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Global = d);

		public AggregationContainerDescriptor<T> IpRange(string name,
			Func<IpRangeAggregationDescriptor<T>, IIpRangeAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.IpRange = d);

		public AggregationContainerDescriptor<T> Max(string name,
			Func<MaxAggregationDescriptor<T>, IMaxAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Max = d);

		public AggregationContainerDescriptor<T> Min(string name,
			Func<MinAggregationDescriptor<T>, IMinAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Min = d);

		public AggregationContainerDescriptor<T> Cardinality(string name,
			Func<CardinalityAggregationDescriptor<T>, ICardinalityAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Cardinality = d);

		public AggregationContainerDescriptor<T> Missing(string name,
			Func<MissingAggregationDescriptor<T>, IMissingAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Missing = d);

		public AggregationContainerDescriptor<T> Nested(string name,
			Func<NestedAggregationDescriptor<T>, INestedAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Nested = d);

		public AggregationContainerDescriptor<T> ReverseNested(string name,
			Func<ReverseNestedAggregationDescriptor<T>, IReverseNestedAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ReverseNested = d);

		public AggregationContainerDescriptor<T> Range(string name,
			Func<RangeAggregationDescriptor<T>, IRangeAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Range = d);

		public AggregationContainerDescriptor<T> Stats(string name,
			Func<StatsAggregationDescriptor<T>, IStatsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Stats = d);

		public AggregationContainerDescriptor<T> Sum(string name,
			Func<SumAggregationDescriptor<T>, ISumAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Sum = d);

		public AggregationContainerDescriptor<T> Terms(string name,
			Func<TermsAggregationDescriptor<T>, ITermsAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Terms = d);

		public AggregationContainerDescriptor<T> SignificantTerms(string name,
			Func<SignificantTermsAggregationDescriptor<T>, ISignificantTermsAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.SignificantTerms = d);

		public AggregationContainerDescriptor<T> ValueCount(string name,
			Func<ValueCountAggregationDescriptor<T>, IValueCountAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ValueCount = d);

		public AggregationContainerDescriptor<T> TopHits(string name,
			Func<TopHitsAggregationDescriptor<T>, ITopHitsAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.TopHits = d);

		public AggregationContainerDescriptor<T> Children<TChild>(string name,
			Func<ChildrenAggregationDescriptor<TChild>, IChildrenAggregation> selector) where TChild : class =>
			_SetInnerAggregation(name, selector, (a, d) => a.Children = d);

		public AggregationContainerDescriptor<T> ScriptedMetric(string name,
			Func<ScriptedMetricAggregationDescriptor<T>, IScriptedMetricAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ScriptedMetric = d);

		public AggregationContainerDescriptor<T> AverageBucket(string name,
			Func<AverageBucketAggregationDescriptor, IAverageBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.AverageBucket = d);

		public AggregationContainerDescriptor<T> Derivative(string name,
			Func<DerivativeAggregationDescriptor, IDerivativeAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Derivative = d);

		public AggregationContainerDescriptor<T> MaxBucket(string name,
			Func<MaxBucketAggregationDescriptor, IMaxBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.MaxBucket = d);

		public AggregationContainerDescriptor<T> MinBucket(string name,
			Func<MinBucketAggregationDescriptor, IMinBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.MinBucket = d);

		public AggregationContainerDescriptor<T> SumBucket(string name,
			Func<SumBucketAggregationDescriptor, ISumBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.SumBucket = d);

		public AggregationContainerDescriptor<T> StatsBucket(string name,
			Func<StatsBucketAggregationDescriptor, IStatsBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.StatsBucket = d);

		public AggregationContainerDescriptor<T> ExtendedStatsBucket(string name,
			Func<ExtendedStatsBucketAggregationDescriptor, IExtendedStatsBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ExtendedStatsBucket = d);

		public AggregationContainerDescriptor<T> PercentilesBucket(string name,
			Func<PercentilesBucketAggregationDescriptor, IPercentilesBucketAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.PercentilesBucket = d);

		public AggregationContainerDescriptor<T> MovingAverage(string name,
			Func<MovingAverageAggregationDescriptor, IMovingAverageAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.MovingAverage = d);

		public AggregationContainerDescriptor<T> CumulativeSum(string name,
			Func<CumulativeSumAggregationDescriptor, ICumulativeSumAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.CumulativeSum = d);

		public AggregationContainerDescriptor<T> SerialDifferencing(string name,
			Func<SerialDifferencingAggregationDescriptor, ISerialDifferencingAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.SerialDifferencing = d);

		public AggregationContainerDescriptor<T> BucketScript(string name,
			Func<BucketScriptAggregationDescriptor, IBucketScriptAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.BucketScript = d);

		public AggregationContainerDescriptor<T> BucketSelector(string name,
			Func<BucketSelectorAggregationDescriptor, IBucketSelectorAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.BucketSelector = d);

		public AggregationContainerDescriptor<T> Sampler(string name,
			Func<SamplerAggregationDescriptor<T>, ISamplerAggregation> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Sampler = d);

		/// <summary>
		/// Fluent methods do not assign to properties on `this` directly but on IAggregationContainers inside `this.Aggregations[string, IContainer]
		/// </summary>
		private AggregationContainerDescriptor<T> _SetInnerAggregation<TAggregator, TAggregatorInterface>(
			string key,
			Func<TAggregator, TAggregatorInterface> selector
			, Action<IAggregationContainer, TAggregatorInterface> assignToProperty
		)
			where TAggregator : IAggregation, TAggregatorInterface, new()
			where TAggregatorInterface : IAggregation
		{
			var aggregator = selector(new TAggregator());

			//create new isolated container for new aggregator and assign to the right property
			var container = new AggregationContainer() { Meta = aggregator.Meta };

			assignToProperty(container, aggregator);

			//create aggregations dictionary on `this` if it does not exist already
			IAggregationContainer self = this;
			if (self.Aggregations == null) self.Aggregations = new Dictionary<string, IAggregationContainer>();

			//if the aggregator is a bucket aggregator (meaning it contains nested aggregations);
			var bucket = aggregator as IBucketAggregation;
			if (bucket != null && bucket.Aggregations.HasAny())
			{
				//make sure we copy those aggregations to the isolated container's
				//own .Aggregations container (the one that gets serialized to "aggs")
				IAggregationContainer d = container;
				d.Aggregations = bucket.Aggregations;
			}
			//assign the aggregations container under Aggregations ("aggs" in the json)
			self.Aggregations[key] = container;
			return this;
		}

		public void Accept(IAggregationVisitor visitor)
		{
			if (visitor.Scope == AggregationVisitorScope.Unknown) visitor.Scope = AggregationVisitorScope.Aggregation;
			new AggregationWalker().Walk(this, visitor);
		}
	}
}
