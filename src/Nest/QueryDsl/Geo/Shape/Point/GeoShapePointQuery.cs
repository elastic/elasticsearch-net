using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointQuery : FieldNameQueryBase, IGeoShapePointQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IPointGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapePointQuery q) => q.Field.IsConditionless() || q.Shape?.Coordinates == null;
	}

	public class GeoShapePointQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery, T>
		, IGeoShapePointQuery where T : class
	{
		protected override bool Conditionless => GeoShapePointQuery.IsConditionless(this);
		IPointGeoShape IGeoShapePointQuery.Shape { get; set; }

		public GeoShapePointQueryDescriptor<T> Coordinates(GeoCoordinate coordinates) =>
			Assign(a => a.Shape = new PointGeoShape { Coordinates = coordinates });

		public GeoShapePointQueryDescriptor<T> Coordinates(double longitude, double latitude) =>
			Assign(a => a.Shape = new PointGeoShape { Coordinates = new GeoCoordinate(latitude, longitude) });
	}
}
