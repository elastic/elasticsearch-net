using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
	public class AggregationDictionary : IsADictionary<string, IAggregationContainer>
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

		public static implicit operator AggregationDictionary(AggregatorBase aggregator)
		{
			IAggregatorBase b;
			var combinator = aggregator as AggregatorCombinator;
			if (combinator != null)
			{
				var dict = new Dictionary<string, AggregationContainer>();
				foreach (var agg in combinator.Aggregations)
				{
					b =  agg;
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
		[JsonProperty("avg")]
		IAverageAggregator Average { get; set; }

		[JsonProperty("date_histogram")]
		IDateHistogramAggregator DateHistogram { get; set; }

		[JsonProperty("percentiles")]
		IPercentilesAggregator Percentiles { get; set; }

		[JsonProperty("date_range")]
		IDateRangeAggregator DateRange { get; set; }

		[JsonProperty("extended_stats")]
		IExtendedStatsAggregator ExtendedStats { get; set; }

		[JsonProperty("filter")]
		IFilterAggregator Filter { get; set; }

		[JsonProperty("filters")]
		IFiltersAggregator Filters { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceAggregator GeoDistance { get; set; }

		[JsonProperty("geohash_grid")]
		IGeoHashAggregator GeoHash { get; set; }

		[JsonProperty("geo_bounds")]
		IGeoBoundsAggregator GeoBounds { get; set; }

		[JsonProperty("histogram")]
		IHistogramAggregator Histogram { get; set; }

		[JsonProperty("global")]
		IGlobalAggregator Global { get; set; }

		[JsonProperty("ip_range")]
		IIp4RangeAggregator IpRange { get; set; }

		[JsonProperty("max")]
		IMaxAggregator Max { get; set; }

		[JsonProperty("min")]
		IMinAggregator Min { get; set; }

		[JsonProperty("cardinality")]
		ICardinalityAggregator Cardinality { get; set; }

		[JsonProperty("missing")]
		IMissingAggregator Missing { get; set; }

		[JsonProperty("nested")]
		INestedAggregator Nested { get; set; }

		[JsonProperty("reverse_nested")]
		IReverseNestedAggregator ReverseNested { get; set; }

		[JsonProperty("range")]
		IRangeAggregator Range { get; set; }

		[JsonProperty("stats")]
		IStatsAggregator Stats { get; set; }

		[JsonProperty("sum")]
		ISumAggregator Sum { get; set; }

		[JsonProperty("terms")]
		ITermsAggregator Terms { get; set; }

		[JsonProperty("significant_terms")]
		ISignificantTermsAggregator SignificantTerms { get; set; }

		[JsonProperty("value_count")]
		IValueCountAggregator ValueCount { get; set; }

		[JsonProperty("percentile_ranks")]
		IPercentileRanksAggregator PercentileRanks { get; set; }

		[JsonProperty("top_hits")]
		ITopHitsAggregator TopHits { get; set; }

		[JsonProperty("children")]
		IChildrenAggregator Children { get; set; }

		[JsonProperty("scripted_metric")]
		IScriptedMetricAggregator ScriptedMetric { get; set; }

		[JsonProperty("aggs")]
		AggregationDictionary Aggregations { get; set; }

	}

	public class AggregationContainer : IAggregationContainer
	{
		public IAverageAggregator Average { get; set; }
		public IValueCountAggregator ValueCount { get; set; }
		public IMaxAggregator Max { get; set; }
		public IMinAggregator Min { get; set; }
		public IStatsAggregator Stats { get; set; }
		public ISumAggregator Sum { get; set; }
		public IExtendedStatsAggregator ExtendedStats { get; set; }
		public IDateHistogramAggregator DateHistogram { get; set; }

		public IPercentilesAggregator Percentiles { get; set; }

		public IDateRangeAggregator DateRange { get; set; }

		public IFilterAggregator Filter { get; set; }

		public IFiltersAggregator Filters { get; set; }

		public IGeoDistanceAggregator GeoDistance { get; set; }

		public IGeoHashAggregator GeoHash { get; set; }

		public IGeoBoundsAggregator GeoBounds { get; set; }

		public IHistogramAggregator Histogram { get; set; }

		public IGlobalAggregator Global { get; set; }

		public IIp4RangeAggregator IpRange { get; set; }

		public ICardinalityAggregator Cardinality { get; set; }

		public IMissingAggregator Missing { get; set; }

		public INestedAggregator Nested { get; set; }

		public IReverseNestedAggregator ReverseNested { get; set; }

		public IRangeAggregator Range { get; set; }

		public ITermsAggregator Terms { get; set; }

		public ISignificantTermsAggregator SignificantTerms { get; set; }

		public IPercentileRanksAggregator PercentileRanks { get; set; }

		public ITopHitsAggregator TopHits { get; set; }

		public IChildrenAggregator Children { get; set; }

		public IScriptedMetricAggregator ScriptedMetric { get; set; }

		public AggregationDictionary Aggregations { get; set; }

		public static implicit operator AggregationContainer(AggregatorBase aggregator)
		{
			if (aggregator == null) return null;
			var container = new AggregationContainer();
			aggregator.WrapInContainer(container);
			var bucket = aggregator as BucketAgg;
			container.Aggregations = bucket?.Aggregations;
			return container;
		}


	}

	public class AggregationContainerDescriptor<T> : IAggregationContainer
		where T : class
	{
		AggregationDictionary IAggregationContainer.Aggregations { get; set; }

		IAverageAggregator IAggregationContainer.Average { get; set; }

		IDateHistogramAggregator IAggregationContainer.DateHistogram { get; set; }

		IPercentilesAggregator IAggregationContainer.Percentiles { get; set; }

		IDateRangeAggregator IAggregationContainer.DateRange { get; set; }

		IExtendedStatsAggregator IAggregationContainer.ExtendedStats { get; set; }

		IFilterAggregator IAggregationContainer.Filter { get; set; }

		IFiltersAggregator IAggregationContainer.Filters { get; set; }

		IGeoDistanceAggregator IAggregationContainer.GeoDistance { get; set; }

		IGeoHashAggregator IAggregationContainer.GeoHash { get; set; }

		IGeoBoundsAggregator IAggregationContainer.GeoBounds { get; set; }

		IHistogramAggregator IAggregationContainer.Histogram { get; set; }

		IGlobalAggregator IAggregationContainer.Global { get; set; }

		IIp4RangeAggregator IAggregationContainer.IpRange { get; set; }

		IMaxAggregator IAggregationContainer.Max { get; set; }

		IMinAggregator IAggregationContainer.Min { get; set; }

		ICardinalityAggregator IAggregationContainer.Cardinality { get; set; }

		IMissingAggregator IAggregationContainer.Missing { get; set; }

		INestedAggregator IAggregationContainer.Nested { get; set; }

		IReverseNestedAggregator IAggregationContainer.ReverseNested { get; set; }

		IRangeAggregator IAggregationContainer.Range { get; set; }

		IStatsAggregator IAggregationContainer.Stats { get; set; }

		ISumAggregator IAggregationContainer.Sum { get; set; }

		IValueCountAggregator IAggregationContainer.ValueCount { get; set; }

		ISignificantTermsAggregator IAggregationContainer.SignificantTerms { get; set; }

		IPercentileRanksAggregator IAggregationContainer.PercentileRanks { get; set; }

		ITermsAggregator IAggregationContainer.Terms { get; set; }

		ITopHitsAggregator IAggregationContainer.TopHits { get; set; }

		IChildrenAggregator IAggregationContainer.Children { get; set; }

		IScriptedMetricAggregator IAggregationContainer.ScriptedMetric { get; set; }

		public AggregationContainerDescriptor<T> Average(string name,
			Func<AverageAggregatorDescriptor<T>, IAverageAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Average = d);

		public AggregationContainerDescriptor<T> DateHistogram(string name,
			Func<DateHistogramAggregatorDescriptor<T>, IDateHistogramAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.DateHistogram = d);

		public AggregationContainerDescriptor<T> Percentiles(string name,
			Func<PercentilesAggregatorDescriptor<T>, IPercentilesAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Percentiles = d);

		public AggregationContainerDescriptor<T> PercentileRanks(string name,
			Func<PercentileRanksAggregatorDescriptor<T>, IPercentileRanksAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.PercentileRanks = d);

		public AggregationContainerDescriptor<T> DateRange(string name,
			Func<DateRangeAggregatorDescriptor<T>, IDateRangeAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.DateRange = d);

		public AggregationContainerDescriptor<T> ExtendedStats(string name,
			Func<ExtendedStatsAggregatorDescriptor<T>, IExtendedStatsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ExtendedStats = d);

		public AggregationContainerDescriptor<T> Filter(string name,
			Func<FilterAggregatorDescriptor<T>, IFilterAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Filter = d);

		public AggregationContainerDescriptor<T> Filters(string name,
			Func<FiltersAggregatorDescriptor<T>, IFiltersAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Filters = d);

		public AggregationContainerDescriptor<T> GeoDistance(string name,
			Func<GeoDistanceAggregatorDescriptor<T>, IGeoDistanceAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoDistance = d);

		public AggregationContainerDescriptor<T> GeoHash(string name,
			Func<GeoHashAggregatorDescriptor<T>, IGeoHashAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoHash = d);

		public AggregationContainerDescriptor<T> GeoBounds(string name,
			Func<GeoBoundsAggregatorDescriptor<T>, IGeoBoundsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.GeoBounds = d);

		public AggregationContainerDescriptor<T> Histogram(string name,
			Func<HistogramAggregatorDescriptor<T>, IHistogramAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Histogram = d);

		public AggregationContainerDescriptor<T> Global(string name,
			Func<GlobalAggregatorDescriptor<T>, IGlobalAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Global = d);

		public AggregationContainerDescriptor<T> IpRange(string name,
			Func<Ip4RangeAggregatorDescriptor<T>, IIp4RangeAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.IpRange = d);

		public AggregationContainerDescriptor<T> Max(string name,
			Func<MaxAggregatorDescriptor<T>, IMaxAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Max = d);

		public AggregationContainerDescriptor<T> Min(string name,
			Func<MinAggregatorDescriptor<T>, IMinAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Min = d);

		public AggregationContainerDescriptor<T> Cardinality(string name,
			Func<CardinalityAggregatorDescriptor<T>, ICardinalityAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Cardinality = d);

		public AggregationContainerDescriptor<T> Missing(string name,
			Func<MissingAggregatorDescriptor<T>, IMissingAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Missing = d);

		public AggregationContainerDescriptor<T> Nested(string name,
			Func<NestedAggregatorDescriptor<T>, INestedAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Nested = d);

		public AggregationContainerDescriptor<T> ReverseNested(string name,
			Func<ReverseNestedAggregationDescriptor<T>, IReverseNestedAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ReverseNested = d);

		public AggregationContainerDescriptor<T> Range(string name,
			Func<RangeAggregatorDescriptor<T>, IRangeAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Range = d);

		public AggregationContainerDescriptor<T> Stats(string name,
			Func<StatsAggregatorDescriptor<T>, IStatsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Stats = d);

		public AggregationContainerDescriptor<T> Sum(string name,
			Func<SumAggregatorDescriptor<T>, ISumAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Sum = d);

		public AggregationContainerDescriptor<T> Terms(string name,
			Func<TermsAggregatorDescriptor<T>, ITermsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.Terms = d);

		public AggregationContainerDescriptor<T> SignificantTerms(string name,
			Func<SignificantTermsAggregatorDescriptor<T>, ISignificantTermsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.SignificantTerms = d);

		public AggregationContainerDescriptor<T> ValueCount(string name,
			Func<ValueCountAggregatorDescriptor<T>, IValueCountAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ValueCount = d);

		public AggregationContainerDescriptor<T> TopHits(string name,
			Func<TopHitsAggregatorDescriptor<T>, ITopHitsAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.TopHits = d);

		public AggregationContainerDescriptor<T> Children<TChild>(string name,
			Func<ChildrenAggregatorDescriptor<TChild>, IChildrenAggregator> selector) where TChild : class =>
			_SetInnerAggregation(name, selector, (a, d) => a.Children = d);

		public AggregationContainerDescriptor<T> ScriptedMetric(string name,
			Func<ScriptedMetricAggregatorDescriptor<T>, IScriptedMetricAggregator> selector) =>
			_SetInnerAggregation(name, selector, (a, d) => a.ScriptedMetric = d);

		/// <summary>
		/// Fluent methods do not assing to properties on `this` directly but on IAggregationContainers inside `this.Aggregations[string, IContainer]
		/// </summary>
		private AggregationContainerDescriptor<T> _SetInnerAggregation<TAggregator, TAggregatorInterface>(
			string key,
			Func<TAggregator, TAggregatorInterface> selector
			, Action<IAggregationContainer, TAggregatorInterface> assignToProperty
		)
			where TAggregator : IAggregator, TAggregatorInterface, new()
			where TAggregatorInterface : IAggregator
		{
			var aggregator = selector(new TAggregator());

			//create new isolated container for new aggregator and assign to the right property
			var container = new AggregationContainer();
			assignToProperty(container, aggregator);

			//create aggregations dictionary on `this` if it does not exist already
			IAggregationContainer self = this;
			if (self.Aggregations == null) self.Aggregations = new Dictionary<string, IAggregationContainer>();

			//if the aggregator is a bucket aggregator (meaning it contains nested aggregations);
			var bucket = aggregator as IBucketAggregator;
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
	}
}