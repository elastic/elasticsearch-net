using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoDistanceQuery, IGeoDistanceQuery>))]
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }
		
		[JsonProperty("distance")]
		Distance Distance { get; set; }
		
		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
	
		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	
	}

	public class GeoDistanceQuery : FieldNameQueryBase, IGeoDistanceQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public GeoLocation Location { get; set; }
		public Distance Distance { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public bool? Coerce { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }


		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) => 
			q.Location == null || q.Distance == null || q.Field.IsConditionless();
	}

	public class GeoDistanceQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery, T> 
		, IGeoDistanceQuery where T : class
	{
		protected override bool Conditionless => GeoDistanceQuery.IsConditionless(this);
		GeoLocation IGeoDistanceQuery.Location { get; set; }
		Distance IGeoDistanceQuery.Distance { get; set; }
		GeoDistanceType? IGeoDistanceQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceQuery.OptimizeBoundingBox { get; set; }
		bool? IGeoDistanceQuery.Coerce { get; set; }
		bool? IGeoDistanceQuery.IgnoreMalformed { get; set; }
		GeoValidationMethod? IGeoDistanceQuery.ValidationMethod { get; set; }

		public GeoDistanceQueryDescriptor<T> Location(GeoLocation location) => Assign(a => a.Location = location);

		public GeoDistanceQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoDistanceQueryDescriptor<T> Distance(Distance distance) => Assign(a => a.Distance = distance);

		public GeoDistanceQueryDescriptor<T> Distance(double distance, DistanceUnit unit) => Assign(a => a.Distance = new Distance(distance, unit));

		public GeoDistanceQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistanceType type) => Assign(a => a.DistanceType = type);

		public GeoDistanceQueryDescriptor<T> Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		public GeoDistanceQueryDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);

		public GeoDistanceQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
