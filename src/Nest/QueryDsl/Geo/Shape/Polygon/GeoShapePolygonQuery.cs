using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapePolygonQuery : GeoShapeQueryBase, IGeoShapePolygonQuery
	{
		private IPolygonGeoShape _shape;

		public IPolygonGeoShape Shape
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

		internal static bool IsConditionless(IGeoShapePolygonQuery q) =>
			q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapePolygonQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery, T>
			, IGeoShapePolygonQuery where T : class
	{
		protected override bool Conditionless => GeoShapePolygonQuery.IsConditionless(this);
		IPolygonGeoShape IGeoShapePolygonQuery.Shape { get; set; }

		public GeoShapePolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<GeoCoordinate>> coordinates, bool? ignoreUnmapped = null) =>
			Assign(a =>
			{
				a.Shape = a.Shape ?? new PolygonGeoShape();
				a.Shape.Coordinates = coordinates;
				a.IgnoreUnmapped = ignoreUnmapped;
			});
	}
}
