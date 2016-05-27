using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(VariableFieldNameQueryJsonConverter<GeoBoundingBoxQuery, IGeoBoundingBoxQuery>))]
	public interface IGeoBoundingBoxQuery : IFieldNameQuery
	{
		[VariableField]
		IBoundingBox BoundingBox { get; set; }

		[JsonProperty("type")]
		GeoExecution? Type { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	}


	public class GeoBoundingBoxQuery : FieldNameQueryBase, IGeoBoundingBoxQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IBoundingBox BoundingBox { get; set; }
		public GeoExecution? Type { get; set; }
		public bool? Coerce { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoBoundingBox = this;

		internal static bool IsConditionless(IGeoBoundingBoxQuery q) =>
			q.Field.IsConditionless() || q.BoundingBox?.BottomRight == null || q.BoundingBox?.TopLeft == null;
	}

	public class GeoBoundingBoxQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoBoundingBoxQueryDescriptor<T>, IGeoBoundingBoxQuery, T>
		, IGeoBoundingBoxQuery where T : class
	{
		protected override bool Conditionless => GeoBoundingBoxQuery.IsConditionless(this);
		IBoundingBox IGeoBoundingBoxQuery.BoundingBox { get; set; }
		GeoExecution? IGeoBoundingBoxQuery.Type { get; set; }
		bool? IGeoBoundingBoxQuery.Coerce { get; set; }
		bool? IGeoBoundingBoxQuery.IgnoreMalformed { get; set; }
		GeoValidationMethod? IGeoBoundingBoxQuery.ValidationMethod { get; set; }

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(double topLeftLat, double topLeftLon, double bottomRightLat, double bottomRightLon) => 
			BoundingBox(f=>f.TopLeft(topLeftLat, topLeftLon).BottomRight(bottomRightLat, bottomRightLon));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(GeoLocation topLeft, GeoLocation bottomRight) => 
			BoundingBox(f=>f.TopLeft(topLeft).BottomRight(bottomRight));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(Func<BoundingBoxDescriptor, IBoundingBox> boundingBoxSelector) => 
			Assign(a => a.BoundingBox = boundingBoxSelector?.Invoke(new BoundingBoxDescriptor()));

		public GeoBoundingBoxQueryDescriptor<T> Type(GeoExecution type) => Assign(a => a.Type = type);

		public GeoBoundingBoxQueryDescriptor<T> Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		public GeoBoundingBoxQueryDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);

		public GeoBoundingBoxQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
