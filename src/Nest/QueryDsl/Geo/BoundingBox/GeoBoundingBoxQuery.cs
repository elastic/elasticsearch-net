using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(VariableFieldNameQueryJsonConverter<GeoBoundingBoxQuery, IGeoBoundingBoxQuery>))]
	public interface IGeoBoundingBoxQuery : IFieldNameQuery
	{
		[VariableField]
		IBoundingBox BoundingBox { get; set; }

		[DataMember(Name ="type")]
		GeoExecution? Type { get; set; }

		[DataMember(Name ="validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	}


	public class GeoBoundingBoxQuery : FieldNameQueryBase, IGeoBoundingBoxQuery
	{
		public IBoundingBox BoundingBox { get; set; }
		public GeoExecution? Type { get; set; }

		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoBoundingBox = this;

		internal static bool IsConditionless(IGeoBoundingBoxQuery q) =>
			q.Field.IsConditionless() || q.BoundingBox?.BottomRight == null && q.BoundingBox?.TopLeft == null && q.BoundingBox?.WellKnownText == null;
	}

	public class GeoBoundingBoxQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoBoundingBoxQueryDescriptor<T>, IGeoBoundingBoxQuery, T>
			, IGeoBoundingBoxQuery where T : class
	{
		protected override bool Conditionless => GeoBoundingBoxQuery.IsConditionless(this);
		IBoundingBox IGeoBoundingBoxQuery.BoundingBox { get; set; }
		GeoExecution? IGeoBoundingBoxQuery.Type { get; set; }
		GeoValidationMethod? IGeoBoundingBoxQuery.ValidationMethod { get; set; }

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(double topLeftLat, double topLeftLon, double bottomRightLat, double bottomRightLon) =>
			BoundingBox(f => f.TopLeft(topLeftLat, topLeftLon).BottomRight(bottomRightLat, bottomRightLon));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(GeoLocation topLeft, GeoLocation bottomRight) =>
			BoundingBox(f => f.TopLeft(topLeft).BottomRight(bottomRight));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(string wkt) =>
			BoundingBox(f => f.WellKnownText(wkt));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(Func<BoundingBoxDescriptor, IBoundingBox> boundingBoxSelector) =>
			Assign(a => a.BoundingBox = boundingBoxSelector?.Invoke(new BoundingBoxDescriptor()));

		public GeoBoundingBoxQueryDescriptor<T> Type(GeoExecution? type) => Assign(a => a.Type = type);

		public GeoBoundingBoxQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
