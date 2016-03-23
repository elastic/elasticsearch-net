using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<GeoDistanceAggregation>))]
	public interface IGeoDistanceAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("origin")]
		GeoLocation Origin { get; set; }

		[JsonProperty("unit")]
		DistanceUnit? Unit { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<IRange> Ranges { get; set; }
	}

	public class GeoDistanceAggregation : BucketAggregationBase, IGeoDistanceAggregation
	{
		public Field Field { get; set; }

		public GeoLocation Origin { get; set; }

		public DistanceUnit? Unit { get; set; }

		public GeoDistanceType? DistanceType { get; set; }

		public IEnumerable<IRange> Ranges { get; set; }

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

		GeoLocation IGeoDistanceAggregation.Origin { get; set; }

		DistanceUnit? IGeoDistanceAggregation.Unit { get; set; }

		GeoDistanceType? IGeoDistanceAggregation.DistanceType { get; set; }

		IEnumerable<IRange> IGeoDistanceAggregation.Ranges { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Origin(double lat, double lon) => Assign(a => a.Origin = new GeoLocation(lat, lon));

		public GeoDistanceAggregationDescriptor<T> Origin(GeoLocation geoLocation) => Assign(a => a.Origin = geoLocation);

		public GeoDistanceAggregationDescriptor<T> Unit(DistanceUnit unit) => Assign(a => a.Unit = unit);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType? geoDistance) => Assign(a => a.DistanceType = geoDistance);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<RangeDescriptor, IRange>[] ranges) =>
			Assign(a => a.Ranges = ranges?.Select(r => r(new RangeDescriptor())));

	}
}
