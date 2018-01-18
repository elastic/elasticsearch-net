using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeCircleQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleQuery : GeoShapeQueryBase, IGeoShapeCircleQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public ICircleGeoShape Shape { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;

		internal static bool IsConditionless(IGeoShapeCircleQuery q) => q.Field.IsConditionless() || q.Shape?.Coordinates == null;
	}

	public class GeoShapeCircleQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery, T>
		, IGeoShapeCircleQuery where T : class
	{
		protected override bool Conditionless => GeoShapeCircleQuery.IsConditionless(this);
		ICircleGeoShape IGeoShapeCircleQuery.Shape { get; set; }

		public GeoShapeCircleQueryDescriptor<T> Coordinates(GeoCoordinate coordinates, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new CircleGeoShape();
			a.Shape.Coordinates = coordinates;
			a.Shape.IgnoreUnmapped = ignoreUnmapped;
		});

		public GeoShapeCircleQueryDescriptor<T> Coordinates(double longitude, double latitude, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new CircleGeoShape();
			a.Shape.Coordinates = new GeoCoordinate(latitude, longitude);
			a.Shape.IgnoreUnmapped = ignoreUnmapped;
		});

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new CircleGeoShape();
			a.Shape.Radius = radius;
			a.Shape.IgnoreUnmapped = ignoreUnmapped;
		});
	}
}
