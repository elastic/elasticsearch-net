using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPolygonQuery : GeoShapeQueryBase, IGeoShapeMultiPolygonQuery
	{
		private IMultiPolygonGeoShape _shape;
		protected override bool Conditionless => IsConditionless(this);

		public IMultiPolygonGeoShape Shape
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
		internal static bool IsConditionless(IGeoShapeMultiPolygonQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiPolygonQueryDescriptor<T>
		: GeoShapeQueryDescriptorBase<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery, T>
		, IGeoShapeMultiPolygonQuery where T : class
	{
		protected override bool Conditionless => GeoShapeMultiPolygonQuery.IsConditionless(this);
		IMultiPolygonGeoShape IGeoShapeMultiPolygonQuery.Shape { get; set; }

		public GeoShapeMultiPolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates, bool? ignoreUnmapped = null) =>
			Assign(a =>
			{
				a.Shape = a.Shape ?? new MultiPolygonGeoShape();
				a.Shape.Coordinates = coordinates;
				a.IgnoreUnmapped = ignoreUnmapped;
			});


	}
}
