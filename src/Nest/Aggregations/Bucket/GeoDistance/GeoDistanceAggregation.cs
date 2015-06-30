using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

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
		GeoDistance? DistanceType { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregator : BucketAggregator, IGeoDistanceAggregator
	{
		public PropertyPathMarker Field { get; set; }

		public string Origin { get; set; }

		public GeoUnit? Unit { get; set; }

		public GeoDistance? DistanceType { get; set; }

		public IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregationDescriptor<T> :
		BucketAggregatorBaseDescriptor<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregator, T>
			, IGeoDistanceAggregator
		where T : class
	{
		PropertyPathMarker IGeoDistanceAggregator.Field { get; set; }

		string IGeoDistanceAggregator.Origin { get; set; }

		GeoUnit? IGeoDistanceAggregator.Unit { get; set; }

		GeoDistance? IGeoDistanceAggregator.DistanceType { get; set; }

		IEnumerable<Range<double>> IGeoDistanceAggregator.Ranges { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Origin(double Lat, double Lon) =>
			Assign(a => a.Origin = "{0}, {1}".F(
				Lat.ToString(CultureInfo.InvariantCulture), Lon.ToString(CultureInfo.InvariantCulture)
			));

		public GeoDistanceAggregationDescriptor<T> Origin(string geoHash) => Assign(a => a.Origin = geoHash);

		public GeoDistanceAggregationDescriptor<T> Unit(GeoUnit unit) => Assign(a => a.Unit = unit);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistance geoDistance) => Assign(a => a.DistanceType = geoDistance);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Range<double>() select range(r)).ToListOrNullIfEmpty());

	}
}