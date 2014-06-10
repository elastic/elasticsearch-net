using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<GeoDistanceAggregator>))]
	public interface IGeoDistanceAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("origin")]
		string Origin { get; set; }

		[JsonProperty("unit")]
		GeoUnit? Unit { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }
		
		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregator : BucketAggregator, IGeoDistanceAggregator
	{
		public PropertyPathMarker Field { get; set; }

		public string Origin { get; set; }

		public GeoUnit? Unit { get; set; }

		public GeoDistanceType? DistanceType { get; set; }

		public IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GeoDistanceAggregationDescriptor<T>, T>, IGeoDistanceAggregator where T : class
	{
		private IGeoDistanceAggregator Self { get { return this; } }

		PropertyPathMarker IGeoDistanceAggregator.Field { get; set; }
		
		string IGeoDistanceAggregator.Origin { get; set; }
	
		GeoUnit? IGeoDistanceAggregator.Unit { get; set; }

		GeoDistanceType? IGeoDistanceAggregator.DistanceType { get; set; }

		IEnumerable<Range<double>> IGeoDistanceAggregator.Ranges { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Origin(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Self.Origin = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Origin(string geoHash)
		{
			Self.Origin = geoHash;
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Unit(GeoUnit unit)
		{
			Self.Unit = unit;
			return this;
		}
		
		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType geoDistanceType)
		{
			Self.DistanceType = geoDistanceType;
			return this;
		}

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges)
		{
			var newRanges = from range in ranges let r = new Range<double>() select range(r);
			Self.Ranges = newRanges;
			return this;
		}
	}
}