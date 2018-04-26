using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointQuery : GeoShapeQueryBase, IGeoShapePointQuery
	{
		private IPointGeoShape _shape;
		protected override bool Conditionless => IsConditionless(this);

		public IPointGeoShape Shape
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
		internal static bool IsConditionless(IGeoShapePointQuery q) => q.Field.IsConditionless() || q.Shape?.Coordinates == null;
	}

	public class GeoShapePointQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery, T>
		, IGeoShapePointQuery where T : class
	{
		protected override bool Conditionless => GeoShapePointQuery.IsConditionless(this);
		IPointGeoShape IGeoShapePointQuery.Shape { get; set; }

		public GeoShapePointQueryDescriptor<T> Coordinates(GeoCoordinate coordinates, bool? ignoreUnmapped = null) =>
			Assign(a =>
			{
				a.Shape = a.Shape ?? new PointGeoShape();
				a.Shape.Coordinates = coordinates;
				a.IgnoreUnmapped = ignoreUnmapped;
			});

		public GeoShapePointQueryDescriptor<T> Coordinates(double longitude, double latitude, bool? ignoreUnmapped = null) =>
			Assign(a =>
			{
				a.Shape = a.Shape ?? new PointGeoShape();
				a.Shape.Coordinates = new GeoCoordinate(latitude, longitude);
				a.IgnoreUnmapped = ignoreUnmapped;
			});
	}
}
