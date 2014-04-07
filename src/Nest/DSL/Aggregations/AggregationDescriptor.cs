using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public class AggregationDescriptor<T>
		where T : class
	{
		internal readonly IDictionary<string, AggregationDescriptor<T>> _Aggregations =
			new Dictionary<string, AggregationDescriptor<T>>();

		[JsonProperty("aggs", Order = 100)] 
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, AggregationDescriptor<T>> _NestedAggregations;


		[JsonProperty("avg")]
		internal AverageAggregationDescriptor<T> _Average { get; set; }
		public AggregationDescriptor<T> Average(string name, Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Average = d);
		}

		[JsonProperty("date_histogram")]
		internal DateHistogramAggregationDescriptor<T> _DateHistogram { get; set; }
		public AggregationDescriptor<T> DateHistogram(string name,
			Func<DateHistogramAggregationDescriptor<T>, DateHistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._DateHistogram = d);
		}
		
		[JsonProperty("percentiles")]
		internal PercentilesAggregationDescriptor<T> _Percentiles { get; set; }
		public AggregationDescriptor<T> Percentiles(string name,
			Func<PercentilesAggregationDescriptor<T>, PercentilesAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Percentiles = d);
		}

		[JsonProperty("date_range")]
		internal DateRangeAggregationDescriptor<T> _DateRange { get; set; }
		public AggregationDescriptor<T> DateRange(string name,
			Func<DateRangeAggregationDescriptor<T>, DateRangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._DateRange = d);
		}

		[JsonProperty("extended_stats")]
		internal ExtendedStatsAggregationDescriptor<T> _ExtendedStats { get; set; }
		public AggregationDescriptor<T> ExtendedStats(string name,
			Func<ExtendedStatsAggregationDescriptor<T>, ExtendedStatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._ExtendedStats = d);
		}
	
		[JsonProperty("filter")]
		internal FilterAggregationDescriptor<T> _Filter { get; set; }
		public AggregationDescriptor<T> Filter(string name,
			Func<FilterAggregationDescriptor<T>, FilterAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Filter = d);
		}
		
		[JsonProperty("geo_distance")]
		internal GeoDistanceAggregationDescriptor<T> _GeoDistance { get; set; }
		public AggregationDescriptor<T> GeoDistance(string name,
			Func<GeoDistanceAggregationDescriptor<T>, GeoDistanceAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._GeoDistance = d);
		}

		[JsonProperty("geohash_grid")]
		internal GeoHashAggregationDescriptor<T> _GeoHash { get; set; }
		public AggregationDescriptor<T> GeoHash(string name,
			Func<GeoHashAggregationDescriptor<T>, GeoHashAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._GeoHash = d);
		}

		[JsonProperty("histogram")]
		internal HistogramAggregationDescriptor<T> _Histogram { get; set; }
		public AggregationDescriptor<T> Histogram(string name,
			Func<HistogramAggregationDescriptor<T>, HistogramAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Histogram = d);
		}
		
		[JsonProperty("global")]
		internal GlobalAggregationDescriptor<T> _Global { get; set; }
		public AggregationDescriptor<T> Global(string name,
			Func<GlobalAggregationDescriptor<T>, GlobalAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Global = d);
		}

		[JsonProperty("ip_range")]
		internal Ip4RangeAggregationDescriptor<T> _IpRange { get; set; }
		public AggregationDescriptor<T> IpRange(string name,
			Func<Ip4RangeAggregationDescriptor<T>, Ip4RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._IpRange = d);
		}

		[JsonProperty("max")]
		internal MaxAggregationDescriptor<T> _Max { get; set; }
		public AggregationDescriptor<T> Max(string name, Func<MaxAggregationDescriptor<T>, MaxAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Max = d);
		}

		[JsonProperty("min")]
		internal MinAggregationDescriptor<T> _Min { get; set; }
		public AggregationDescriptor<T> Min(string name, Func<MinAggregationDescriptor<T>, MinAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Min = d);
		}
	
		[JsonProperty("missing")]
		internal MissingAggregationDescriptor<T> _Missing { get; set; }
		public AggregationDescriptor<T> Missing(string name, Func<MissingAggregationDescriptor<T>, MissingAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Missing = d);
		}

		[JsonProperty("nested")]
		internal NestedAggregationDescriptor<T> _Nested { get; set; }
		public AggregationDescriptor<T> Nested(string name, Func<NestedAggregationDescriptor<T>, NestedAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Nested = d);
		}

		[JsonProperty("range")]
		internal RangeAggregationDescriptor<T> _Range { get; set; }
		public AggregationDescriptor<T> Range(string name, Func<RangeAggregationDescriptor<T>, RangeAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Range = d);
		}

		[JsonProperty("stats")]
		internal StatsAggregationDescriptor<T> _Stats { get; set; }
		public AggregationDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, StatsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Stats = d);
		}

		[JsonProperty("sum")]
		internal SumAggregationDescriptor<T> _Sum { get; set; }
		public AggregationDescriptor<T> Sum(string name, Func<SumAggregationDescriptor<T>, SumAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Sum = d);
		}

		[JsonProperty("terms")]
		internal TermsAggregationDescriptor<T> _Terms { get; set; }
		public AggregationDescriptor<T> Terms(string name, Func<TermsAggregationDescriptor<T>, TermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._Terms = d);
		}
		
		[JsonProperty("significant_terms")]
		internal SignificantTermsAggregationDescriptor<T> _SignificantTerms { get; set; }
		public AggregationDescriptor<T> SignificantTerms(string name, Func<SignificantTermsAggregationDescriptor<T>, SignificantTermsAggregationDescriptor<T>> selector)
		{
			return _SetInnerAggregation(name, selector, (a, d) => a._SignificantTerms = d);
		}

		[JsonProperty("value_count")]
		internal ValueCountAggregationDescriptor<T> _ValueCount { get; set; }
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
			var bucket = innerDescriptor as IBucketAggregationDescriptor<T>;
			if (bucket != null && bucket.NestedAggregations.HasAny())
			{
				descriptor._NestedAggregations = bucket.NestedAggregations;
			}
			this._Aggregations[key] = descriptor;
			return this;

		}


	}
}