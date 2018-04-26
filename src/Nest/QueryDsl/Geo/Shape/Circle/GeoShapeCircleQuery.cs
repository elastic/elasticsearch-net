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
		protected override bool Conditionless => IsConditionless(this);

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
			a.IgnoreUnmapped = ignoreUnmapped;
		});

		public GeoShapeCircleQueryDescriptor<T> Coordinates(double longitude, double latitude, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new CircleGeoShape();
			a.Shape.Coordinates = new GeoCoordinate(latitude, longitude);
			a.IgnoreUnmapped = ignoreUnmapped;
		});

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius, bool? ignoreUnmapped = null) => Assign(a =>
		{
			a.Shape = a.Shape ?? new CircleGeoShape();
			a.Shape.Radius = radius;
			a.IgnoreUnmapped = ignoreUnmapped;
		});
	}
}
