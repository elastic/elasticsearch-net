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
		private ICircleGeoShape _shape;

		public ICircleGeoShape Shape
		{
			get => _shape;
			set
			{
#pragma warning disable 618
				if (value?.IgnoreUnmapped != null)
				{
					IgnoreUnmapped = value.IgnoreUnmapped;
					value.IgnoreUnmapped = null;
				}
#pragma warning restore 618
				_shape = value;
			}
		}

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoShape = this;

		internal static bool IsConditionless(IGeoShapeCircleQuery q) => q.Field.IsConditionless() || q.Shape?.Coordinates == null;
	}

	public class GeoShapeCircleQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery, T>
			, IGeoShapeCircleQuery where T : class
	{
		protected override bool Conditionless => GeoShapeCircleQuery.IsConditionless(this);
		ICircleGeoShape IGeoShapeCircleQuery.Shape { get; set; }

		public GeoShapeCircleQueryDescriptor<T> Coordinates(GeoCoordinate coordinates, bool? ignoreUnmapped = null) =>
			Assign(coordinates, (a, v) =>
			{
				a.Shape = a.Shape ?? new CircleGeoShape();
				a.Shape.Coordinates = v;
			})
			.Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);

		public GeoShapeCircleQueryDescriptor<T> Coordinates(double longitude, double latitude, bool? ignoreUnmapped = null) =>
			Assign(new GeoCoordinate(latitude, longitude), (a, v) =>
			{
				a.Shape = a.Shape ?? new CircleGeoShape();
				a.Shape.Coordinates = v;
			})
			.Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius, bool? ignoreUnmapped = null) =>
			Assign(radius,(a, v) =>
			{
				a.Shape = a.Shape ?? new CircleGeoShape();
				a.Shape.Radius = v;
			})
			.Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);
	}
}
