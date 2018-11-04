using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(VariableFieldNameQueryJsonConverter<GeoDistanceRangeQuery, IGeoDistanceRangeQuery>))]
	[Obsolete("Scheduled to be removed in 6.0")]
	public interface IGeoDistanceRangeQuery : IFieldNameQuery
	{
		[Obsolete("Deprecated. Use ValidationMethod")]
		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty("gt")]
		Distance GreaterThan { get; set; }

		[JsonProperty("gte")]
		Distance GreaterThanOrEqualTo { get; set; }

		[Obsolete("Deprecated. Use ValidationMethod")]
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("lt")]
		Distance LessThan { get; set; }

		[JsonProperty("lte")]
		Distance LessThanOrEqualTo { get; set; }

		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }

		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class GeoDistanceRangeQuery : FieldNameQueryBase, IGeoDistanceRangeQuery
	{
		[Obsolete("Deprecated. Use ValidationMethod")]
		public bool? Coerce { get; set; }

		public GeoDistanceType? DistanceType { get; set; }
		public Distance GreaterThan { get; set; }
		public Distance GreaterThanOrEqualTo { get; set; }

		[Obsolete("Deprecated. Use ValidationMethod")]
		public bool? IgnoreMalformed { get; set; }

		public Distance LessThan { get; set; }
		public Distance LessThanOrEqualTo { get; set; }
		public GeoLocation Location { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoDistanceRange = this;

		internal static bool IsConditionless(IGeoDistanceRangeQuery q) =>
			q.Field == null || q.Location == null
			|| q.LessThan == null && q.LessThanOrEqualTo == null && q.GreaterThanOrEqualTo == null && q.GreaterThan == null;
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class GeoDistanceRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery, T>
			, IGeoDistanceRangeQuery where T : class
	{
		protected override bool Conditionless => GeoDistanceRangeQuery.IsConditionless(this);
		bool? IGeoDistanceRangeQuery.Coerce { get; set; }
		GeoDistanceType? IGeoDistanceRangeQuery.DistanceType { get; set; }
		Distance IGeoDistanceRangeQuery.GreaterThan { get; set; }
		Distance IGeoDistanceRangeQuery.GreaterThanOrEqualTo { get; set; }
		bool? IGeoDistanceRangeQuery.IgnoreMalformed { get; set; }
		Distance IGeoDistanceRangeQuery.LessThan { get; set; }
		Distance IGeoDistanceRangeQuery.LessThanOrEqualTo { get; set; }
		GeoLocation IGeoDistanceRangeQuery.Location { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		GeoValidationMethod? IGeoDistanceRangeQuery.ValidationMethod { get; set; }

		public GeoDistanceRangeQueryDescriptor<T> Location(GeoLocation geoLocation) => Assign(a => a.Location = geoLocation);

		public GeoDistanceRangeQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoDistanceRangeQueryDescriptor<T> GreaterThan(Distance from) => Assign(a => a.GreaterThan = from);

		public GeoDistanceRangeQueryDescriptor<T> GreaterThan(double distance, DistanceUnit unit) =>
			Assign(a => a.GreaterThan = new Distance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> GreaterThanOrEqualTo(Distance from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public GeoDistanceRangeQueryDescriptor<T> GreaterThanOrEqualTo(double distance, DistanceUnit unit) =>
			Assign(a => a.GreaterThanOrEqualTo = new Distance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> LessThanOrEqualTo(Distance to) => Assign(a => a.LessThanOrEqualTo = to);

		public GeoDistanceRangeQueryDescriptor<T> LessThanOrEqualTo(double distance, DistanceUnit unit) =>
			Assign(a => a.LessThanOrEqualTo = new Distance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> LessThan(Distance to) => Assign(a => a.LessThan = to);

		public GeoDistanceRangeQueryDescriptor<T> LessThan(double distance, DistanceUnit unit) =>
			Assign(a => a.LessThan = new Distance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceRangeQueryDescriptor<T> DistanceType(GeoDistanceType geoDistance) => Assign(a => a.DistanceType = geoDistance);

		[Obsolete("Deprecated. Use ValidationMethod(GeoValidationMethod? validation)")]
		public GeoDistanceRangeQueryDescriptor<T> Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		[Obsolete("Deprecated. Use ValidationMethod(GeoValidationMethod? validation)")]
		public GeoDistanceRangeQueryDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);

		public GeoDistanceRangeQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
