using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoDistanceAggregation))]
	public interface IGeoDistanceAggregation : IBucketAggregation
	{
		[DataMember(Name ="distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="origin")]
		GeoLocation Origin { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IAggregationRange> Ranges { get; set; }

		[DataMember(Name ="unit")]
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

		public GeoDistanceAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Origin(double lat, double lon) => Assign(a => a.Origin = new GeoLocation(lat, lon));

		public GeoDistanceAggregationDescriptor<T> Origin(GeoLocation geoLocation) => Assign(a => a.Origin = geoLocation);

		public GeoDistanceAggregationDescriptor<T> Unit(DistanceUnit? unit) => Assign(a => a.Unit = unit);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType? geoDistance) => Assign(a => a.DistanceType = geoDistance);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<AggregationRangeDescriptor, IAggregationRange>[] ranges) =>
			Assign(a => a.Ranges = ranges?.Select(r => r(new AggregationRangeDescriptor())));
	}
}
