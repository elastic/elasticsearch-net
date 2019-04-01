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
		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("origin")]
		GeoLocation Origin { get; set; }

		[JsonProperty("ranges")]
		IEnumerable<IAggregationRange> Ranges { get; set; }

		[JsonProperty("unit")]
		DistanceUnit? Unit { get; set; }
	}

	public class GeoDistanceAggregation : BucketAggregationBase, IGeoDistanceAggregation
	{
		internal GeoDistanceAggregation() { }

		public GeoDistanceAggregation(string name) : base(name) { }

		public GeoDistanceType? DistanceType { get; set; }
		public Field Field { get; set; }

		public GeoLocation Origin { get; set; }

		public IEnumerable<IAggregationRange> Ranges { get; set; }

		public DistanceUnit? Unit { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoDistance = this;
	}

	public class GeoDistanceAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregation, T>
			, IGeoDistanceAggregation
		where T : class
	{
		GeoDistanceType? IGeoDistanceAggregation.DistanceType { get; set; }
		Field IGeoDistanceAggregation.Field { get; set; }

		GeoLocation IGeoDistanceAggregation.Origin { get; set; }

		IEnumerable<IAggregationRange> IGeoDistanceAggregation.Ranges { get; set; }

		DistanceUnit? IGeoDistanceAggregation.Unit { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(field, (a, v) => a.Field = v);

		public GeoDistanceAggregationDescriptor<T> Origin(double lat, double lon) => Assign(new GeoLocation(lat, lon), (a, v) => a.Origin = v);

		public GeoDistanceAggregationDescriptor<T> Origin(GeoLocation geoLocation) => Assign(geoLocation, (a, v) => a.Origin = v);

		public GeoDistanceAggregationDescriptor<T> Unit(DistanceUnit? unit) => Assign(unit, (a, v) => a.Unit = v);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType? geoDistance) => Assign(geoDistance, (a, v) => a.DistanceType = v);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<AggregationRangeDescriptor, IAggregationRange>[] ranges) =>
			Assign(ranges?.Select(r => r(new AggregationRangeDescriptor())), (a, v) => a.Ranges = v);
	}
}
