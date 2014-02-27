using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class GeoDistanceAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GeoDistanceAggregationDescriptor<T>, T>
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public GeoDistanceAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("origin")]
		internal string _Origin { get; set; }
	
		public GeoDistanceAggregationDescriptor<T> Origin(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			this._Origin = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Origin(string geoHash)
		{
			this._Origin = geoHash;
			return this;
		}

		[JsonProperty("unit")]
		internal GeoUnit? _Unit { get; set; }

		public GeoDistanceAggregationDescriptor<T> Unit(GeoUnit unit)
		{
			this._Unit = unit;
			return this;
		}
		
		[JsonProperty("distance_type")]
		internal GeoDistanceType? _DistanceType { get; set; }

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType geoDistanceType)
		{
			this._DistanceType = geoDistanceType;
			return this;
		}

		[JsonProperty(PropertyName = "ranges")]
		internal IEnumerable<Range<double>> _Ranges { get; set; }
		
		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges)
		{
			var newRanges = from range in ranges let r = new Range<double>() select range(r);
			this._Ranges = newRanges;
			return this;
		}
	}
}