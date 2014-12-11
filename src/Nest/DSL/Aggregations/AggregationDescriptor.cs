using System;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<AggregationContainer>))]
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
		IPercentileRanksAggregaor PercentileRanks { get; set; }

		[JsonProperty("top_hits")]
		ITopHitsAggregator TopHits { get; set; }

		[JsonProperty("children")]
		IChildrenAggregator Children { get; set; }

		[JsonProperty("aggs")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }
	}


	public class AggregationContainer : IAggregationContainer
	{
		private IDateHistogramAggregator _dateHistogram;
		private IPercentilesAggregator _percentiles;
		private IDateRangeAggregator _dateRange;
		private IFilterAggregator _filter;
		private IGeoDistanceAggregator _geoDistance;
		private IGeoHashAggregator _geoHash;
		private IGeoBoundsAggregator _geoBounds;
		private IHistogramAggregator _histogram;
		private IGlobalAggregator _global;
		private IIp4RangeAggregator _ipRange;
		private ICardinalityAggregator _cardinality;
		private IMissingAggregator _missing;
		private INestedAggregator _nested;
		private IReverseNestedAggregator _reverseNested;
		private IRangeAggregator _range;
		private ITermsAggregator _terms;
		private ISignificantTermsAggregator _significantTerms;
		private IPercentileRanksAggregaor _percentileRanks;
	    private IFiltersAggregator _filters;
		private ITopHitsAggregator _topHits;
		private IChildrenAggregator _children;

		public IAverageAggregator Average { get; set; }
		public IValueCountAggregator ValueCount { get; set; }
		public IMaxAggregator Max { get; set; }
		public IMinAggregator Min { get; set; }
		public IStatsAggregator Stats { get; set; }
		public ISumAggregator Sum { get; set; }
		public IExtendedStatsAggregator ExtendedStats { get; set; }
		public IDateHistogramAggregator DateHistogram
		{
			get { return _dateHistogram; }
			set { _dateHistogram = value; }
		}

		public IPercentilesAggregator Percentiles
		{
			get { return _percentiles; }
			set { _percentiles = value; }
		}

		public IDateRangeAggregator DateRange
		{
			get { return _dateRange; }
			set { _dateRange = value; }
		}

		public IFilterAggregator Filter
		{
			get { return _filter; }
			set { _filter = value; }
		}

        public IFiltersAggregator Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }

		public IGeoDistanceAggregator GeoDistance
		{
			get { return _geoDistance; }
			set { _geoDistance = value; }
		}

		public IGeoHashAggregator GeoHash
		{
			get { return _geoHash; }
			set { _geoHash = value; }
		}

		public IGeoBoundsAggregator GeoBounds
		{
			get { return _geoBounds; }
			set { _geoBounds = value; }
		}

		public IHistogramAggregator Histogram
		{
			get { return _histogram; }
			set { _histogram = value; }
		}

		public IGlobalAggregator Global
		{
			get { return _global; }
			set { _global = value; }
		}

		public IIp4RangeAggregator IpRange
		{
			get { return _ipRange; }
			set { _ipRange = value; }
		}

		public ICardinalityAggregator Cardinality
		{
			get { return _cardinality; }
			set { _cardinality = value; }
		}

		public IMissingAggregator Missing
		{
			get { return _missing; }
			set { _missing = value; }
		}

		public INestedAggregator Nested
		{
			get { return _nested; }
			set { _nested = value; }
		}

		public IReverseNestedAggregator ReverseNested
		{
			get { return _reverseNested; }
			set { _reverseNested = value; }
		}

		public IRangeAggregator Range
		{
			get { return _range; }
			set { _range = value; }
		}

		public ITermsAggregator Terms
		{
			get { return _terms; }
			set { _terms = value; }
		}

		public ISignificantTermsAggregator SignificantTerms
		{
			get { return _significantTerms; }
			set { _significantTerms = value; }
		}
		
		public IPercentileRanksAggregaor PercentileRanks
		{
			get { return _percentileRanks; }
			set { _percentileRanks = value; }
		}

		public ITopHitsAggregator TopHits
		{
			get { return _topHits; }
			set { _topHits = value; }
		}

		public IChildrenAggregator Children
		{
			get { return _children; }
			set { _children = value; }
		}

		private void LiftAggregations(IBucketAggregator bucket)
		{
			if (bucket == null) return;
			this.Aggregations = bucket.Aggregations;
		}

		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }
	}

	public class AggregationDescriptor<T> : IAggregationContainer
		where T : class
	{
		IDictionary<string, IAggregationContainer> IAggregationContainer.Aggregations { get; set; }

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

		IPercentileRanksAggregaor IAggregationContainer.PercentileRanks { get;set; }
		
		ITermsAggregator IAggregationContainer.Terms { get; set; }

		ITopHitsAggregator IAggregationContainer.TopHits { get; set; }

		IChildrenAggregator IAggregationContainer.Children { get; set; }

		public AggregationDescriptor<T> Average(string name, Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Average = d);
		}

		public AggregationDescriptor<T> DateHistogram(string name,
			Func<DateHistogramAggregationDescriptor<T>, DateHistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.DateHistogram = d);
		}
		
		public AggregationDescriptor<T> Percentiles(string name,
			Func<PercentilesAggregationDescriptor<T>, PercentilesAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Percentiles = d);
		}

		public AggregationDescriptor<T> PercentileRanks(string name,
			Func<PercentileRanksAggregationDescriptor<T>, PercentileRanksAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.PercentileRanks = d);
		}

		public AggregationDescriptor<T> DateRange(string name,
			Func<DateRangeAggregationDescriptor<T>, DateRangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.DateRange = d);
		}

		public AggregationDescriptor<T> ExtendedStats(string name,
			Func<ExtendedStatsAggregationDescriptor<T>, ExtendedStatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.ExtendedStats = d);
		}
	
		public AggregationDescriptor<T> Filter(string name,
			Func<FilterAggregationDescriptor<T>, FilterAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Filter = d);
		}

        public AggregationDescriptor<T> Filters(string name,
            Func<FiltersAggregationDescriptor<T>, FiltersAggregationDescriptor<T>> selector)
        {
            return _SetInnerAggregation(name, selector, (a, d) => a.Filters = d);
        }
		
		public AggregationDescriptor<T> GeoDistance(string name,
			Func<GeoDistanceAggregationDescriptor<T>, GeoDistanceAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.GeoDistance = d);
		}

		public AggregationDescriptor<T> GeoHash(string name,
			Func<GeoHashAggregationDescriptor<T>, GeoHashAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.GeoHash = d);
		}

		public AggregationDescriptor<T> GeoBounds(string name,
			Func<GeoBoundsAggregationDescriptor<T>, GeoBoundsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.GeoBounds = d);
		}

		public AggregationDescriptor<T> Histogram(string name,
			Func<HistogramAggregationDescriptor<T>, HistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Histogram = d);
		}
		
		public AggregationDescriptor<T> Global(string name,
			Func<GlobalAggregationDescriptor<T>, GlobalAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Global = d);
		}

		public AggregationDescriptor<T> IpRange(string name,
			Func<Ip4RangeAggregationDescriptor<T>, Ip4RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.IpRange = d);
		}

		public AggregationDescriptor<T> Max(string name, Func<MaxAggregationDescriptor<T>, MaxAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Max = d);
		}

		public AggregationDescriptor<T> Min(string name, Func<MinAggregationDescriptor<T>, MinAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Min = d);
		}
		
		public AggregationDescriptor<T> Cardinality(string name, Func<CardinalityAggregationDescriptor<T>, CardinalityAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Cardinality = d);
		}

		public AggregationDescriptor<T> Missing(string name, Func<MissingAggregationDescriptor<T>, MissingAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Missing = d);
		}

		public AggregationDescriptor<T> Nested(string name, Func<NestedAggregationDescriptor<T>, NestedAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Nested = d);
		}

		public AggregationDescriptor<T> ReverseNested(string name, Func<ReverseNestedAggregationDescriptor<T>, ReverseNestedAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.ReverseNested = d);
		}

		public AggregationDescriptor<T> Range(string name, Func<RangeAggregationDescriptor<T>, RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Range = d);
		}

		public AggregationDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, StatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Stats = d);
		}

		public AggregationDescriptor<T> Sum(string name, Func<SumAggregationDescriptor<T>, SumAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Sum = d);
		}

		public AggregationDescriptor<T> Terms(string name, Func<TermsAggregationDescriptor<T>, TermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Terms = d);
		}
		
		public AggregationDescriptor<T> SignificantTerms(string name, Func<SignificantTermsAggregationDescriptor<T>, SignificantTermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.SignificantTerms = d);
		}

		public AggregationDescriptor<T> ValueCount(string name,
			Func<ValueCountAggregationDescriptor<T>, ValueCountAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.ValueCount = d);
		}

		public AggregationDescriptor<T> TopHits(string name,
			Func<TopHitsAggregationDescriptor<T>, TopHitsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.TopHits = d);
		}

		public AggregationDescriptor<T> Children(string name,
			Func<ChildrenAggregationDescriptor<T>, ChildrenAggregationDescriptor<T>> selector)
		{
			return this.Children<T>(name, selector);
		}

		public AggregationDescriptor<T> Children<K>(string name,
			Func<ChildrenAggregationDescriptor<K>, ChildrenAggregationDescriptor<K>> selector)
			where K : class
		{
			return _SetInnerAggregation(name, selector, (a, d) => a.Children = d);
		}

		private AggregationDescriptor<T> _SetInnerAggregation<TAggregation>(
			string key,
			Func<TAggregation, TAggregation> selector
			, Action<IAggregationContainer, TAggregation> setter 
			)
			where TAggregation : IAggregationDescriptor, new()

		{
			var innerDescriptor = selector(new TAggregation());
			var descriptor = new AggregationDescriptor<T>();
			setter(descriptor, innerDescriptor);
			var bucket = innerDescriptor as IBucketAggregator;
			IAggregationContainer self = this;
			if (self.Aggregations == null) self.Aggregations = new Dictionary<string, IAggregationContainer>();

			if (bucket != null && bucket.Aggregations.HasAny())
			{
				IAggregationContainer d = descriptor;
				d.Aggregations = bucket.Aggregations;
			}
			self.Aggregations[key] = descriptor;
			return this;
		}
	}
}