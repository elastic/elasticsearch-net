using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(VariableFieldNameQueryJsonConverter<GeoDistanceQuery, IGeoDistanceQuery>))]
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		[JsonProperty("distance")]
		Distance Distance { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	}

	public class GeoDistanceQuery : FieldNameQueryBase, IGeoDistanceQuery
	{
		public Distance Distance { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public GeoLocation Location { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) =>
			q.Location == null || q.Distance == null || q.Field.IsConditionless();
	}

	public class GeoDistanceQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery, T>
			, IGeoDistanceQuery where T : class
	{
		protected override bool Conditionless => GeoDistanceQuery.IsConditionless(this);
		Distance IGeoDistanceQuery.Distance { get; set; }
		GeoDistanceType? IGeoDistanceQuery.DistanceType { get; set; }
		GeoLocation IGeoDistanceQuery.Location { get; set; }
		GeoValidationMethod? IGeoDistanceQuery.ValidationMethod { get; set; }

		public GeoDistanceQueryDescriptor<T> Location(GeoLocation location) => Assign(location, (a, v) => a.Location = v);

		public GeoDistanceQueryDescriptor<T> Location(double lat, double lon) => Assign(new GeoLocation(lat, lon), (a, v) => a.Location = v);

		public GeoDistanceQueryDescriptor<T> Distance(Distance distance) => Assign(distance, (a, v) => a.Distance = v);

		public GeoDistanceQueryDescriptor<T> Distance(double distance, DistanceUnit unit) => Assign(new Distance(distance, unit), (a, v) => a.Distance = v);

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistanceType? type) => Assign(type, (a, v) => a.DistanceType = v);

		public GeoDistanceQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(validation, (a, v) => a.ValidationMethod = v);
	}
}
