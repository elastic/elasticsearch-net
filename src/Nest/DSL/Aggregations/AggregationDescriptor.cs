using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	public class AggregationDescriptor<T>
		where T : class
	{
		private readonly IDictionary<string, IAggregationDescriptor> _aggregations =
			new Dictionary<string, IAggregationDescriptor>();

		public AverageAggregationDescriptor<T> _Average { get; set; }
		public AggregationDescriptor<T> Average(Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			this._Average = selector(new AverageAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("date_histogram")]
		public DateHistogramAggregationDescriptor<T> _DateHistogram { get; set; }
		public AggregationDescriptor<T> DateHistogram(
			Func<DateHistogramAggregationDescriptor<T>, DateHistogramAggregationDescriptor<T>> selector)
		{
			this._DateHistogram = selector(new DateHistogramAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("date_range")]
		public DateRangeAggregationDescriptor<T> _DateRange { get; set; }
		public AggregationDescriptor<T> DateRange(
			Func<DateRangeAggregationDescriptor<T>, DateRangeAggregationDescriptor<T>> selector)
		{
			this._DateRange = selector(new DateRangeAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("extended_stats")]
		public ExtendedStatsAggregationDescriptor<T> _ExtendedStats { get; set; }
		public AggregationDescriptor<T> ExtendedStats(
			Func<ExtendedStatsAggregationDescriptor<T>, ExtendedStatsAggregationDescriptor<T>> selector)
		{
			this._ExtendedStats = selector(new ExtendedStatsAggregationDescriptor<T>());
			return this;
		}
	
		[JsonProperty("filter")]
		public FilterAggregationDescriptor<T> _Filter { get; set; }
		public AggregationDescriptor<T> Filter(
			Func<FilterAggregationDescriptor<T>, FilterAggregationDescriptor<T>> selector)
		{
			this._Filter = selector(new FilterAggregationDescriptor<T>());
			return this;
		}
		
		[JsonProperty("geo_distance")]
		public GeoDistanceAggregationDescriptor<T> _GeoDistance { get; set; }
		public AggregationDescriptor<T> GeoDistance(
			Func<GeoDistanceAggregationDescriptor<T>, GeoDistanceAggregationDescriptor<T>> selector)
		{
			this._GeoDistance = selector(new GeoDistanceAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("geo_hash")]
		public GeoHashAggregationDescriptor<T> _GeoHash { get; set; }
		public AggregationDescriptor<T> GeoHash(
			Func<GeoHashAggregationDescriptor<T>, GeoHashAggregationDescriptor<T>> selector)
		{
			this._GeoHash = selector(new GeoHashAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("histogram")]
		public HistogramAggregationDescriptor<T> _Histogram { get; set; }
		public AggregationDescriptor<T> Histogram(
			Func<HistogramAggregationDescriptor<T>, HistogramAggregationDescriptor<T>> selector)
		{
			this._Histogram = selector(new HistogramAggregationDescriptor<T>());
			return this;
		}
		
		[JsonProperty("global")]
		public GlobalAggregationDescriptor<T> _Global { get; set; }
		public AggregationDescriptor<T> Global(
			Func<GlobalAggregationDescriptor<T>, GlobalAggregationDescriptor<T>> selector)
		{
			this._Global = selector(new GlobalAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("ip4_range")]
		public Ip4RangeAggregationDescriptor<T> _Ip4Range { get; set; }
		public AggregationDescriptor<T> Ip4Range(
			Func<Ip4RangeAggregationDescriptor<T>, Ip4RangeAggregationDescriptor<T>> selector)
		{
			this._Ip4Range = selector(new Ip4RangeAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("max")]
		public MaxAggregationDescriptor<T> _Max { get; set; }
		public AggregationDescriptor<T> Max(Func<MaxAggregationDescriptor<T>, MaxAggregationDescriptor<T>> selector)
		{
			this._Max = selector(new MaxAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("min")]
		public MinAggregationDescriptor<T> _Min { get; set; }
		public AggregationDescriptor<T> Min(Func<MinAggregationDescriptor<T>, MinAggregationDescriptor<T>> selector)
		{
			this._Min = selector(new MinAggregationDescriptor<T>());
			return this;
		}
	
		[JsonProperty("missing")]
		public MissingAggregationDescriptor<T> _Missing { get; set; }
		public AggregationDescriptor<T> Missing(Func<MissingAggregationDescriptor<T>, MissingAggregationDescriptor<T>> selector)
		{
			this._Missing = selector(new MissingAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("nested")]
		public NestedAggregationDescriptor<T> _Nested { get; set; }
		public AggregationDescriptor<T> Nested(Func<NestedAggregationDescriptor<T>, NestedAggregationDescriptor<T>> selector)
		{
			this._Nested = selector(new NestedAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("range")]
		public RangeAggregationDescriptor<T> _Range { get; set; }
		public AggregationDescriptor<T> Range(Func<RangeAggregationDescriptor<T>, RangeAggregationDescriptor<T>> selector)
		{
			this._Range = selector(new RangeAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("stats")]
		public StatsAggregationDescriptor<T> _Stats { get; set; }
		public AggregationDescriptor<T> Stats(Func<StatsAggregationDescriptor<T>, StatsAggregationDescriptor<T>> selector)
		{
			this._Stats = selector(new StatsAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("sum")]
		public SumAggregationDescriptor<T> _Sum { get; set; }
		public AggregationDescriptor<T> Sum(Func<SumAggregationDescriptor<T>, SumAggregationDescriptor<T>> selector)
		{
			this._Sum = selector(new SumAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("terms")]
		public TermsAggregationDescriptor<T> _Terms { get; set; }
		public AggregationDescriptor<T> Terms(Func<TermsAggregationDescriptor<T>, TermsAggregationDescriptor<T>> selector)
		{
			this._Terms = selector(new TermsAggregationDescriptor<T>());
			return this;
		}

		[JsonProperty("value_count")]
		public ValueCountAggregationDescriptor<T> _ValueCount { get; set; }
		public AggregationDescriptor<T> ValueCount(
			Func<ValueCountAggregationDescriptor<T>, ValueCountAggregationDescriptor<T>> selector)
		{
			this._ValueCount = selector(new ValueCountAggregationDescriptor<T>());
			return this;
		}

	}
}