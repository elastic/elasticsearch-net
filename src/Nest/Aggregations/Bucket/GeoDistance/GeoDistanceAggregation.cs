using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GeoDistanceAggregation>))]
	public interface IGeoDistanceAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("origin")]
		string Origin { get; set; }

		[JsonProperty("unit")]
		GeoUnit? Unit { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregation : BucketAggregationBase, IGeoDistanceAggregation
	{
		public Field Field { get; set; }

		public string Origin { get; set; }

		public GeoUnit? Unit { get; set; }

		public GeoDistanceType? DistanceType { get; set; }

		public IEnumerable<Range<double>> Ranges { get; set; }

		internal GeoDistanceAggregation() { }

		public GeoDistanceAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoDistance = this;
	}

	public class GeoDistanceAggregationDescriptor<T> :
		BucketAggregationDescriptorBase<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregation, T>
			, IGeoDistanceAggregation
		where T : class
	{
		Field IGeoDistanceAggregation.Field { get; set; }

		string IGeoDistanceAggregation.Origin { get; set; }

		GeoUnit? IGeoDistanceAggregation.Unit { get; set; }

		GeoDistanceType? IGeoDistanceAggregation.DistanceType { get; set; }

		IEnumerable<Range<double>> IGeoDistanceAggregation.Ranges { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Origin(double Lat, double Lon) =>
			Assign(a => a.Origin = "{0}, {1}".F(
				Lat.ToString(CultureInfo.InvariantCulture), Lon.ToString(CultureInfo.InvariantCulture)
			));

		public GeoDistanceAggregationDescriptor<T> Origin(string geoHash) => Assign(a => a.Origin = geoHash);

		public GeoDistanceAggregationDescriptor<T> Unit(GeoUnit unit) => Assign(a => a.Unit = unit);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType? geoDistance) => Assign(a => a.DistanceType = geoDistance);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Range<double>() select range(r)).ToListOrNullIfEmpty());

	}
}