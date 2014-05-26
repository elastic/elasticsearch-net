using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<AggregationContainer>))]
	public interface IAggregationContainer 
	{
		[JsonProperty("avg")]
		IAverageAggregator _Average { get; set; }

		[JsonProperty("date_histogram")]
		IDateHistogramAggregator _DateHistogram { get; set; }

		[JsonProperty("percentiles")]
		IPercentilesAggregator _Percentiles { get; set; }

		[JsonProperty("date_range")]
		IDateRangeAggregator _DateRange { get; set; }

		[JsonProperty("extended_stats")]
		IExtendedStatsAggregator _ExtendedStats { get; set; }

		[JsonProperty("filter")]
		IFilterAggregator _Filter { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceAggregator _GeoDistance { get; set; }

		[JsonProperty("geohash_grid")]
		IGeoHashAggregator _GeoHash { get; set; }

		[JsonProperty("histogram")]
		IHistogramAggregator _Histogram { get; set; }

		[JsonProperty("global")]
		IGlobalAggregator _Global { get; set; }

		[JsonProperty("ip_range")]
		IIp4RangeAggregator _IpRange { get; set; }

		[JsonProperty("max")]
		IMaxAggregator _Max { get; set; }

		[JsonProperty("min")]
		IMinAggregator _Min { get; set; }

		[JsonProperty("cardinality")]
		ICardinalityAggregator _Cardinality { get; set; }

		[JsonProperty("missing")]
		IMissingAggregator _Missing { get; set; }

		[JsonProperty("nested")]
		INestedAggregator _Nested { get; set; }

		[JsonProperty("range")]
		IRangeAggregator _Range { get; set; }

		[JsonProperty("stats")]
		IStatsAggregator _Stats { get; set; }

		[JsonProperty("sum")]
		ISumAggregator _Sum { get; set; }

		[JsonProperty("terms")]
		ITermsAggregator _Terms { get; set; }

		[JsonProperty("significant_terms")]
		ISignificantTermsAggregator _SignificantTerms { get; set; }

		[JsonProperty("value_count")]
		IValueCountAggregator _ValueCount { get; set; }
	}


	public class AggregationContainer : IAggregationContainer
	{
		public IAverageAggregator _Average { get; set; }
		public IDateHistogramAggregator _DateHistogram { get; set; }
		public IPercentilesAggregator _Percentiles { get; set; }
		public IDateRangeAggregator _DateRange { get; set; }
		public IExtendedStatsAggregator _ExtendedStats { get; set; }
		public IFilterAggregator _Filter { get; set; }
		public IGeoDistanceAggregator _GeoDistance { get; set; }
		public IGeoHashAggregator _GeoHash { get; set; }
		public IHistogramAggregator _Histogram { get; set; }
		public IGlobalAggregator _Global { get; set; }
		public IIp4RangeAggregator _IpRange { get; set; }
		public IMaxAggregator _Max { get; set; }
		public IMinAggregator _Min { get; set; }
		public ICardinalityAggregator _Cardinality { get; set; }
		public IMissingAggregator _Missing { get; set; }
		public INestedAggregator _Nested { get; set; }
		public IRangeAggregator _Range { get; set; }
		public IStatsAggregator _Stats { get; set; }
		public ISumAggregator _Sum { get; set; }
		public ITermsAggregator _Terms { get; set; }
		public ISignificantTermsAggregator _SignificantTerms { get; set; }
		public IValueCountAggregator _ValueCount { get; set; }
	}

	public class AggregationDescriptor<T> : IAggregationContainer
		where T : class
	{
		internal readonly IDictionary<string, IAggregationContainer> _Aggregations = new Dictionary<string, IAggregationContainer>();

		[JsonProperty("aggs", Order = 100)] 
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, IAggregationContainer> _NestedAggregations;


		[JsonProperty("avg")]
		public IAverageAggregator _Average { get; set; }
		public AggregationDescriptor<T> Average(string name, Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Average = d);
		}

		[JsonProperty("date_histogram")]
		public IDateHistogramAggregator _DateHistogram { get; set; }
		public AggregationDescriptor<T> DateHistogram(string name,
			Func<DateHistogramAggregationDescriptor<T>, DateHistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._DateHistogram = d);
		}
		
		[JsonProperty("percentiles")]
		public IPercentilesAggregator _Percentiles { get; set; }
		public AggregationDescriptor<T> Percentiles(string name,
			Func<PercentilesAggregationDescriptor<T>, PercentilesAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Percentiles = d);
		}

		[JsonProperty("date_range")]
		public IDateRangeAggregator _DateRange { get; set; }
		public AggregationDescriptor<T> DateRange(string name,
			Func<DateRangeAggregationDescriptor<T>, DateRangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._DateRange = d);
		}

		[JsonProperty("extended_stats")]
		public IExtendedStatsAggregator _ExtendedStats { get; set; }
		public AggregationDescriptor<T> ExtendedStats(string name,
			Func<ExtendedStatsAggregationDescriptor<T>, ExtendedStatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._ExtendedStats = d);
		}
	
		[JsonProperty("filter")]
		public IFilterAggregator _Filter { get; set; }
		public AggregationDescriptor<T> Filter(string name,
			Func<FilterAggregationDescriptor<T>, FilterAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Filter = d);
		}
		
		[JsonProperty("geo_distance")]
		public IGeoDistanceAggregator _GeoDistance { get; set; }
		public AggregationDescriptor<T> GeoDistance(string name,
			Func<GeoDistanceAggregationDescriptor<T>, GeoDistanceAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._GeoDistance = d);
		}

		[JsonProperty("geohash_grid")]
		public IGeoHashAggregator _GeoHash { get; set; }
		public AggregationDescriptor<T> GeoHash(string name,
			Func<GeoHashAggregationDescriptor<T>, GeoHashAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._GeoHash = d);
		}

		[JsonProperty("histogram")]
		public IHistogramAggregator _Histogram { get; set; }
		public AggregationDescriptor<T> Histogram(string name,
			Func<HistogramAggregationDescriptor<T>, HistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Histogram = d);
		}
		
		[JsonProperty("global")]
		public IGlobalAggregator _Global { get; set; }
		public AggregationDescriptor<T> Global(string name,
			Func<GlobalAggregationDescriptor<T>, GlobalAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Global = d);
		}

		[JsonProperty("ip_range")]
		public IIp4RangeAggregator _IpRange { get; set; }
		public AggregationDescriptor<T> IpRange(string name,
			Func<Ip4RangeAggregationDescriptor<T>, Ip4RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._IpRange = d);
		}

		[JsonProperty("max")]
		public IMaxAggregator _Max { get; set; }
		public AggregationDescriptor<T> Max(string name, Func<MaxAggregationDescriptor<T>, MaxAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Max = d);
		}

		[JsonProperty("min")]
		public IMinAggregator _Min { get; set; }
		public AggregationDescriptor<T> Min(string name, Func<MinAggregationDescriptor<T>, MinAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Min = d);
		}
		
		[JsonProperty("cardinality")]
		public ICardinalityAggregator _Cardinality { get; set; }
		public AggregationDescriptor<T> Cardinality(string name, Func<CardinalityAggregationDescriptor<T>, CardinalityAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Cardinality = d);
		}

		[JsonProperty("missing")]
		public IMissingAggregator _Missing { get; set; }
		public AggregationDescriptor<T> Missing(string name, Func<MissingAggregationDescriptor<T>, MissingAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Missing = d);
		}

		[JsonProperty("nested")]
		public INestedAggregator _Nested { get; set; }
		public AggregationDescriptor<T> Nested(string name, Func<NestedAggregationDescriptor<T>, NestedAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Nested = d);
		}

		[JsonProperty("range")]
		public IRangeAggregator _Range { get; set; }
		public AggregationDescriptor<T> Range(string name, Func<RangeAggregationDescriptor<T>, RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Range = d);
		}

		[JsonProperty("stats")]
		public IStatsAggregator _Stats { get; set; }
		public AggregationDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, StatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Stats = d);
		}

		[JsonProperty("sum")]
		public ISumAggregator _Sum { get; set; }
		public AggregationDescriptor<T> Sum(string name, Func<SumAggregationDescriptor<T>, SumAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Sum = d);
		}

		[JsonProperty("terms")]
		public ITermsAggregator _Terms { get; set; }
		public AggregationDescriptor<T> Terms(string name, Func<TermsAggregationDescriptor<T>, TermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Terms = d);
		}
		
		[JsonProperty("significant_terms")]
		public ISignificantTermsAggregator _SignificantTerms { get; set; }
		public AggregationDescriptor<T> SignificantTerms(string name, Func<SignificantTermsAggregationDescriptor<T>, SignificantTermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._SignificantTerms = d);
		}

		[JsonProperty("value_count")]
		public IValueCountAggregator _ValueCount { get; set; }
		public AggregationDescriptor<T> ValueCount(string name,
			Func<ValueCountAggregationDescriptor<T>, ValueCountAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._ValueCount = d);
		}

		private AggregationDescriptor<T> _SetInnerAggregation<TAggregation>(
			string key,
			Func<TAggregation, TAggregation> selector
			, Action<AggregationDescriptor<T>, TAggregation> setter 
			)
			where TAggregation : IAggregationDescriptor, new()

		{
			var innerDescriptor = selector(new TAggregation());
			var descriptor = new AggregationDescriptor<T>();
			setter(descriptor, innerDescriptor);
			var bucket = innerDescriptor as IBucketAggregator;
			if (bucket != null && bucket.NestedAggregations.HasAny())
			{
				descriptor._NestedAggregations = bucket.NestedAggregations;
			}
			this._Aggregations[key] = descriptor;
			return this;
		}
	}
}