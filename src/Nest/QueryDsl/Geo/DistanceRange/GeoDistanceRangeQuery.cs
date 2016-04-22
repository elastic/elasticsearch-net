using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoDistanceRangeQuery, IGeoDistanceRangeQuery>))]
	public interface IGeoDistanceRangeQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("gte")]
		Distance GreaterThanOrEqualTo { get; set; }
		
		[JsonProperty("lte")]
		Distance LessThanOrEqualTo { get; set; }
		
		[JsonProperty("gt")]
		Distance GreaterThan { get; set; }

		[JsonProperty("lt")]
		Distance LessThan { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }
		
		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		
		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
	
		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	
	}

	public class GeoDistanceRangeQuery : FieldNameQueryBase, IGeoDistanceRangeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public GeoLocation Location { get; set; }
		public Distance GreaterThan { get; set; }
		public Distance LessThan { get; set; }
		public Distance GreaterThanOrEqualTo { get; set; }
		public Distance LessThanOrEqualTo { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? Coerce { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoDistanceRange = this;

		internal static bool IsConditionless(IGeoDistanceRangeQuery q) => 
			q.Field == null || q.Location == null 
			|| (q.LessThan == null && q.LessThanOrEqualTo == null && q.GreaterThanOrEqualTo == null && q.GreaterThan == null);
	}

	public class GeoDistanceRangeQueryDescriptor<T> : FieldNameQueryDescriptorBase<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery, T> 
		, IGeoDistanceRangeQuery where T : class
	{
		protected override bool Conditionless => GeoDistanceRangeQuery.IsConditionless(this);
		GeoLocation IGeoDistanceRangeQuery.Location { get; set; }
		Distance IGeoDistanceRangeQuery.GreaterThanOrEqualTo { get; set; }
		Distance IGeoDistanceRangeQuery.LessThanOrEqualTo { get; set; }
		Distance IGeoDistanceRangeQuery.GreaterThan { get; set; }
		Distance IGeoDistanceRangeQuery.LessThan { get; set; }
		GeoDistanceType? IGeoDistanceRangeQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		bool? IGeoDistanceRangeQuery.Coerce { get; set; }
		bool? IGeoDistanceRangeQuery.IgnoreMalformed { get; set; }
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


		public GeoDistanceRangeQueryDescriptor<T> Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		public GeoDistanceRangeQueryDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);

		public GeoDistanceRangeQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
